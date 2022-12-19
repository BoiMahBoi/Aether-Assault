using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Menu References")]
    public GameObject mainMenu;
    public GameObject howToMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;

    private void Start()
    {
        Screen.fullScreen = true;
    }

    public void playGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void switchMenu(GameObject menu)
    {
        mainMenu.SetActive(false);
        menu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        if(optionsMenu.activeSelf)
        {
            optionsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        else if (howToMenu.activeSelf)
        {
            howToMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        else if (creditsMenu.activeSelf)
        {
            creditsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        if(!Screen.fullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}
