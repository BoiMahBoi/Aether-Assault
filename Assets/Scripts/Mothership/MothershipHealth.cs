using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipHealth : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject[] cannons = new GameObject[3];
    public GameObject[] healthSections = new GameObject[6];
    public SpriteRenderer playerSprite;

    private bool isFading = false;

    //Referrence to the HealthBar
    [Tooltip("Assign the Mothership's Healthbar here")]
    public HealthBar healthBar;

    [Header("Health Settings")]
    public int currentHP = 5;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public bool hasWorkingTurrets()
    {
        foreach (var t in cannons)
        {
            if(t.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    public bool hasHealthLeft()
    {
        foreach (var t in healthSections)
        {
            if (t.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    public void TakeDamage(float damage)
    {
        healthSections[currentHP].SetActive(false);
        currentHP++;
        bool isAlive = hasHealthLeft();

        if (!isAlive)
        {
            //Spawn explosion here
            Debug.Log("The Starfighter won!");
            gameManager.GameOver("Starfighter");
        }
        if (isFading)
        {
            StopAllCoroutines();
            Color tmp = playerSprite.color;
            tmp.r = 0.75f;
            tmp.g = 0.75f;
            tmp.b = 0.75f;
            playerSprite.color = tmp;
        }
        StartCoroutine(BlinkWhite(0.75f, 0.5f));
    }

    public IEnumerator BlinkWhite(float endValue, float duration)
    {
        isFading = true;
        Color tmp = playerSprite.color;
        tmp.r = 1.0f;
        tmp.g = 1.0f;
        tmp.b = 1.0f;
        playerSprite.color = tmp;
        float elapsedTime = 0;
        float startRedValue = playerSprite.color.r;
        float startGreenValue = playerSprite.color.g;
        float startBlueValue = playerSprite.color.b;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newRed = Mathf.Lerp(startRedValue, endValue, elapsedTime / duration);
            float newGreen = Mathf.Lerp(startGreenValue, endValue, elapsedTime / duration);
            float newBlue = Mathf.Lerp(startBlueValue, endValue, elapsedTime / duration);
            playerSprite.color = new Color(newRed, newGreen, newBlue, playerSprite.color.a);
            yield return null;
        }
        isFading = false;
    }
}
