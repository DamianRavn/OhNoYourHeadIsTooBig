using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private static Manager _instance;

    public static Manager Instance { get { return _instance; } }

    public Transform startPoint;
    public TripLogic playerPrefab;
    public CinemachineFreeLook cinemachineCameraPrefab;
    public Camera mainCameraPrefab;
    public GameObject winScreen;

    private TripLogic spawnedPlayer;
    private int maxCoinAmount;

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

    private void Start()
    {
        maxCoinAmount = GameObject.FindGameObjectsWithTag("Coin").Length;
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        spawnedPlayer = Instantiate(playerPrefab, startPoint.position, startPoint.rotation);
        Instantiate(mainCameraPrefab);
        CinemachineFreeLook cinemaCam = Instantiate(cinemachineCameraPrefab, spawnedPlayer.transform.position - spawnedPlayer.transform.forward, Quaternion.identity);
        cinemaCam.Follow = spawnedPlayer.transform;
        cinemaCam.LookAt = spawnedPlayer.thirdPersonMovement.cameraLookAtPoint;
    }
    public void ResetPlayer()
    {
        spawnedPlayer.thirdPersonMovement.controller.enabled = false;

        spawnedPlayer.transform.position = startPoint.position;
        spawnedPlayer.transform.rotation = startPoint.rotation;
        spawnedPlayer.tripScale = 0f;

        spawnedPlayer.thirdPersonMovement.controller.enabled = true;
    }

    public void WinGame()
    {
        Time.timeScale = 0;
        TMP_Text text = winScreen.transform.GetChild(0).GetComponent<TMP_Text>();
        text.text = string.Format("Congratulations! \n You collected {0} out of {1} Coins!", CoinManager.Instance.CoinAmount, maxCoinAmount);
        winScreen.SetActive(true);
    }
    public void LoadLevel(string name)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
}
