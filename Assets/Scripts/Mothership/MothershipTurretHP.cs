using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MothershipTurretHP : MonoBehaviour
{
    //Referrence to the HealthBar
    public HealthBar healthBar;
    public float maxHP;
    public float currentHP;
    public float repairTime;
    private GameObject cannon;
    public GameObject cannonPrefab;

    void Start()
    {
        //Assigning value to max health
        maxHP = 100;
        currentHP = maxHP;
        cannon = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        /*//*TESTING* When space is pressed, the function for taking damage is called
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }*/
    }

    public void UpdateHealthBar()
    {
        //Updating the HealthBar, so that is relates to current health

        //healthBar.SetHealth(currentHP); // re insert this line when mothership cannons has healthbars
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        UpdateHealthBar();

        if (currentHP < 0)
        {
            TurretDestroy();
        }
    }

    void TurretDestroy()
    {
        cannon.SetActive(false);
        StartCoroutine(TurretRepair());
    }

    IEnumerator TurretRepair()
    {
        yield return new WaitForSeconds(repairTime);
        currentHP = maxHP;
        UpdateHealthBar();
        // reset rotation?
        cannon.SetActive(true);
    }
}
