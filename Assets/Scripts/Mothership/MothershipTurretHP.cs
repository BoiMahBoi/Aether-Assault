using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipTurretHP : MonoBehaviour
{
    public float maxHP;
    private float currentHP;
    public float repairTime;
    private GameObject cannon;
    public GameObject cannonPrefab;

    void Start()
    {
        currentHP = maxHP;
        cannon = transform.GetChild(0).gameObject;
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
        cannon.SetActive(false);
        StartCoroutine(TurretRepair());
    }

    IEnumerator TurretRepair()
    {
        yield return new WaitForSeconds(repairTime);
        currentHP = maxHP;
        // reset rotation?
        cannon.SetActive(true);
    }
}
