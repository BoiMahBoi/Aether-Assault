using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject howToMenu;
    public GameObject optionsMenu;

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
            Debug.Log("Going back to the Main Menu from the Options Menu");
            optionsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        else if (howToMenu.activeSelf)
        {
            Debug.Log("Going back to the Main Menu from the How To Play Menu");
            howToMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }

}
