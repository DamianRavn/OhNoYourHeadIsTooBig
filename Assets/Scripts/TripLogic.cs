using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripLogic : MonoBehaviour
{
    public ThirdPersonMovement thirdPersonMovement;

    private int HalfScreenSize = Screen.width/2;
    public float maxTripRotationZ = 50f;

    private int dir = 1;

    public float baseJumpHeight = 2f;
    public float balanceInterval = 2f;
    public float balanceIntervalMultiplyer = 2.3f;
    private float turnSmoothVelocity;
    private float turnSmoothVelocityY;
    public float turnYSpeed = 2;
    public float turnSmoothTime = 0.1f;

    private float extraJumpPowerMultBase = 1f;
    private float extraJumpPowerMult = 1f;
    public float extraJumpOnClick = 5f;

    private Transform model;
    private RaycastHit hit;

    [Range(-1, 1)]
    public float tripScale;

    private void Start()
    {
        //InvokeRepeating("UpdateTriping", 1f, invokeRepeatingTimer);
        model = transform.GetChild(0);
    }

    private void Update()
    {
        if (tripScale == 0) tripScale = 0.1f * dir;

        RayCastDown();
        RotatePlayer();
        UpdateTriping();
        if (Input.GetMouseButton(0))
        {
            Balance(Input.mousePosition);
        }
        tripScale = Mathf.Clamp(tripScale, -1, 1);
    }

    private void UpdateTriping()
    {
        float tripNumber = Mathf.Sign(tripScale) * balanceInterval * Time.deltaTime;
        tripScale += tripNumber;
        
        float jumpStart = 0.6f;
        if ((tripScale > jumpStart || tripScale < -jumpStart) && thirdPersonMovement.controller.isGrounded)
        {
            thirdPersonMovement.Jump(Mathf.Abs(baseJumpHeight * tripScale), model.up * extraJumpPowerMult);
            extraJumpPowerMult = extraJumpPowerMultBase;
        }
    }
    private void RotatePlayer()
    {
        //Rotation
        Vector3 rot = transform.eulerAngles;
        float curve = Mathf.Sin(tripScale * Mathf.PI * 0.5f);

        rot.z = Mathf.LerpUnclamped(0, maxTripRotationZ, curve);
        float yRot = rot.y += -Mathf.Sign(tripScale) * turnYSpeed *  Time.deltaTime;
        float angleZ = Mathf.SmoothDampAngle(transform.eulerAngles.z, rot.z, ref turnSmoothVelocity, turnSmoothTime);
        float angleY = Mathf.SmoothDampAngle(rot.y, yRot, ref turnSmoothVelocityY, turnSmoothTime);

        transform.rotation = Quaternion.Euler(rot.x, angleY, angleZ);

        //Trip direction
        //Vector3 currentMoveDir = thirdPersonMovement.GetMoveDirection();
        //Vector3 crossP = Vector3.Cross(Vector3.up, currentMoveDir).normalized;
        //Vector3 tripDirection = Vector3.SlerpUnclamped(currentMoveDir, crossP, tripScale);

        //Debug.DrawRay(transform.position, currentMoveDir * 50f, Color.yellow);
        //Debug.DrawRay(transform.position, crossP * 50f, Color.red);
        //Debug.DrawRay(transform.position, tripDirection * 50f, Color.green);

        //thirdPersonMovement.AdjustMoveDirection(tripDirection * Time.deltaTime);
    }

    private void Balance(Vector3 towards)
    {
        if (tripScale > 0.999f || tripScale < -0.999f)
        {
            extraJumpPowerMult = extraJumpPowerMultBase + extraJumpOnClick;
        }

        float func = balanceInterval * balanceIntervalMultiplyer * Time.deltaTime;

        if (towards.x < HalfScreenSize)
        {
            //Left
            
            tripScale += func;
            dir = 1;
        }
        else
        {
            //Right
            tripScale -= func;
            dir = -1;
        }
    }

    private void RayCastDown()
    {
        if (Physics.Raycast(transform.position - new Vector3(0, thirdPersonMovement.controller.height / 2, 0), Vector3.down, out hit, 0.5f))
        {
            if (hit.collider.tag == "Respawn") Manager.Instance.ResetPlayer();
            Debug.DrawRay(transform.position - new Vector3(0, thirdPersonMovement.controller.height / 2, 0), Vector3.down * 0.5f, Color.yellow);
        }
    }
}
