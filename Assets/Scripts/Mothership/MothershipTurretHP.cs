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
    public bool isDestroyed;
    private GameObject cannon;
    public GameObject cannonPrefab;

    void Start()
    {
        //Assigning value to max health
        isDestroyed = false;
        currentHP = maxHP;
        cannon = transform.GetChild(0).gameObject;
    }

    public void UpdateHealthBar()
    {
        //Updating the HealthBar, so that is relates to current health
        healthBar.SetHealth(currentHP);
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        UpdateHealthBar();

        if (currentHP <= 0)
        {
            TurretDestroy();
        }
    }

    void TurretDestroy()
    {
        cannon.SetActive(false);
        isDestroyed = false;
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
