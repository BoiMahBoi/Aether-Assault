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
    public bool isFunctional;
    public GameObject cannon;
    public GameObject cannonPrefab;

    void Start()
    {
        //Assigning value to max health
        maxHP = 100;
        currentHP = maxHP;
        cannon = Instantiate(cannonPrefab, transform);
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

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        healthBar.SetHealth(currentHP);

        if (currentHP < 0)
        {
            TurretDestroy();
        }
    }

    void TurretDestroy()
    {
        Destroy(cannon);
        StartCoroutine(TurretRepair());
    }

    IEnumerator TurretRepair()
    {
        yield return new WaitForSeconds(repairTime);
        currentHP = maxHP;
        cannon = Instantiate(cannonPrefab, transform);
    }
}
