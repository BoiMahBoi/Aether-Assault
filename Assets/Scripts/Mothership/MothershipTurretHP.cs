using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipTurretHP : MonoBehaviour
{
    public float maxHP;
    public float currentHP;
    public float repairTime;
    public bool isFunctional;
    public GameObject cannon;
    public GameObject cannonPrefab;

    void Start()
    {
        currentHP = maxHP;
        cannon = Instantiate(cannonPrefab, transform);
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        if(currentHP < 0)
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
