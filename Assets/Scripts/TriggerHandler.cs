using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    public void HandleDeathTrigger()
    {
        Manager.Instance.ResetPlayer();
    }

    public void HandleCoinTrigger()
    {
        CoinManager.Instance.CoinCollected();
    }
}
