using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 moveDirection;

    public CharacterController controller;

    public Transform cameraLookAtPoint;
    
    public float speed = 6f;

    private float gravity = Physics.gravity.y;

    private void Update()
    {
        if (controller.isGrounded)
        {
            if (velocity.y < 0)
            {
                velocity.y = -2f;
                velocity.x = 0;
                velocity.z = 0;
            }
            
            MoveForward();
        }

        
        if (!controller.isGrounded)
        {
            //Apply gravity
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    private void MoveForward()
    {
        controller.Move(moveDirection * speed * Time.deltaTime);
        moveDirection = new Vector3(transform.forward.x, 0f, transform.forward.z);
    }

    public void AdjustMoveDirection(Vector3 newDirection)
    {
        moveDirection += newDirection;
        moveDirection.Normalize();
    }
    public Vector3 GetMoveDirection()
    {
        return moveDirection;
    }

    public void Jump(float jumpHeight)
    {
        Jump(jumpHeight, Vector3.up);
    }
    public void Jump(float jumpHeight, Vector3 direction)
    {
        if (controller.isGrounded)
        {
            AddForce(jumpHeight, direction);
        }
            
    }

    public void AddForce(float height, Vector3 direction)
    {
        velocity.y = Mathf.Sqrt(height * -2f * gravity);
        velocity.x = direction.x;
        velocity.z = direction.z;
    }
}
