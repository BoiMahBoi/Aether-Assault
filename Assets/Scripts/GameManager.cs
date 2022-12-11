using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    [HideInInspector]public bool gamePaused = false;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public TextMeshProUGUI gameOverText;

    public GameObject Mothership;
    public GameObject Starfighter;
    public GameObject Planet;
    public GameObject BeamOfDeath;

    public GameObject MothershipExplosion;
    public GameObject StarfighterExplosion;

    [Header("Animations")]
    public GameObject planetExplosion;
    public Animator cameraAnim;

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
        if(!gamePaused)
        {
            gamePaused = true;
            Time.timeScale = 0;
            gameOverText.SetText("The " + Winner + " has won!");
            gameOverMenu.SetActive(true);
        }
    }

    public void PauseGame()
    {
        if (!gamePaused)
        {
            gamePaused = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        } else
        {
            gamePaused = false;
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


    public void endGameBuffer(string deadPlayer)
    {
        StartCoroutine(EndGameWithABang(deadPlayer));
    }

    public void endGameWithBoomieHaha()
    {
        StartCoroutine(EndGameWithABiggerBang());
    }

    public IEnumerator EndGameWithABang(string deadPlayer)
    {
        string tmp;
        Debug.Log("Gameover");
        
        if(deadPlayer == "Starfighter")
        {
            tmp = "Mothership";
            Instantiate(StarfighterExplosion, Starfighter.transform.position, Starfighter.transform.localRotation);
            Starfighter.SetActive(false);
        } else
        {
            tmp = "Starfighter";
            Instantiate(MothershipExplosion, Mothership.transform.position, Mothership.transform.localRotation);
            Mothership.SetActive(false);
        }
        yield return new WaitForSeconds(1);
        GameOver(tmp);
    }

    public IEnumerator EndGameWithABiggerBang()
    {
        cameraAnim.SetTrigger("PlanetBoom");
        yield return new WaitForSeconds(1);
        Planet.SetActive(false);
        Instantiate(planetExplosion, transform.position, transform.localRotation);

        yield return new WaitForSeconds(2);
        GameOver("Mothership");
    }


}
