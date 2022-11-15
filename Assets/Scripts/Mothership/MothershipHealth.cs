using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipHealth : MonoBehaviour
{

    public GameObject[] cannons = new GameObject[3];

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
        }
    }
}
