using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SubMenu;

    public void ClickPlay()
    {
        MainMenu.SetActive(false);
        SubMenu.SetActive(true);
    }
    public void ClickQuit()
    {
        Application.Quit();
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
}
