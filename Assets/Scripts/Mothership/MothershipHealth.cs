using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipHealth : MonoBehaviour
{

    public GameObject[] cannons = new GameObject[3];
    public SpriteRenderer playerSprite;

    private bool isFading = false;

    //Referrence to the HealthBar
    [Tooltip("Assign the Mothership's Healthbar here")]
    public HealthBar healthBar;

    [Header("Health Settings")]
    public float maxHP;
    public float currentHP;
    private bool isDead = false;

    void Start()
    {
        healthBar.SetMaxHealth(maxHP);
        currentHP = maxHP;
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

    public void UpdateHealthBar()
    {
        //Updating the HealthBar, so that is relates to current health
        healthBar.SetHealth(currentHP);
    }

    public void TakeDamage(float damage)
    {
        if(!isDead)
        {
            currentHP -= damage;
            healthBar.SetHealth(currentHP);
            //UpdateHealthBar();

            if (currentHP <= 0)
            {
                //Spawn explosion here
                isDead = true;
                Debug.Log("The Starfighter won!");
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
