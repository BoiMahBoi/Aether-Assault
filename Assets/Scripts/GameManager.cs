using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    [HideInInspector]public bool MenuOpened = false;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public TextMeshProUGUI gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }


    public void GameOver(string Winner)
    {
        if(!MenuOpened)
        {
            MenuOpened = true;
            Time.timeScale = 0;
            gameOverText.SetText("The " + Winner + " has won!");
            gameOverMenu.SetActive(true);
        }
    }

    public void PauseGame()
    {
        if (!MenuOpened)
        {
            MenuOpened = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        } else
        {
            MenuOpened = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    public void Rematch()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
