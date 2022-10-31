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
        //Setting the healthbars health to the max health
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //*TESTING* When space is pressed, the function for taking damage is called
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    //The function for taking damage, substracting the damage taken from current health
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}
