using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        TripLogic player = other.GetComponent<TripLogic>();
        if (player)
        {
            player.thirdPersonMovement.AddForce(4, player.transform.position - transform.position);
        }
    }
}
