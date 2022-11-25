using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starfighterHealth : MonoBehaviour
{
    private bool isFading = false;
    private GameManager gameManager;
    public AudioSource hitSound;

    [Header("Health Settings")]
    //Public var for the maximum health
    public float maxHealth;
    //Public var for the current health
    public float currentHealth;

    //Referrence to the HealthBar
    [Header("Object References")]
    public HealthBar healthBar;
    public SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //Assigning value to the maximum health
        maxHealth = 100;
        //Setting the current health to the maximum health at start of game
        currentHealth = maxHealth;
        //Calling the function UpdateHealthBar
        UpdateHealthBar();
    }
    
    public void UpdateHealthBar()
    {
        //Updating the HealthBar, so that is relates to current health
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            //Spawn explosion here
            Debug.Log("Mothership wins!");
            gameManager.GameOver("Mothership");
        }
    }

    //The function for taking damage, substracting the damage taken from current health
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        hitSound.Play();
        UpdateHealthBar();

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
