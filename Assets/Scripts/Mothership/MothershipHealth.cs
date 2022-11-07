using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipHealth : MonoBehaviour
{

    public GameObject[] cannons = new GameObject[3];

    //Referrence to the HealthBar
    //public HealthBar healthBar;
    public float maxHP;
    public float currentHP;

    void Start()
    {
        //Assigning value to max health
        maxHP = 100;
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

    /*public void UpdateHealthBar()
    {
        //Updating the HealthBar, so that is relates to current health
        healthBar.SetHealth(currentHP);
    }*/

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        //UpdateHealthBar();

        if (currentHP <= 0)
        {
            //Spawn explosion here
            Debug.Log("The Starfighter won!");
        }
    }
}
