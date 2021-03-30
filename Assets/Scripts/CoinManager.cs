using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private static CoinManager _instance;

    public static CoinManager Instance { get { return _instance; } }

    public int CoinAmount { get => coinAmount; set => coinAmount = value; }

    public TextMeshProUGUI coinText;
    private int coinAmount = 0;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    public void CoinCollected()
    {
        coinAmount++;
        coinText.text = coinAmount.ToString();
    }
}
