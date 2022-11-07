using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starfighterHealth : MonoBehaviour
{
    //Public var for the maximum health
    public float maxHealth;
    //Public var for the current health
    public float currentHealth;
    //Referrence to the HealthBar
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
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
        }
    }

    //The function for taking damage, substracting the damage taken from current health
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        UpdateHealthBar();
    }
}
