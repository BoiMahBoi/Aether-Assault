using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretShoot : MonoBehaviour
{
    [Header("Turret Settings")]
    public float reloadTime;
    public bool canShoot;

    [Header("Object References")]
    public GameObject firePoint;
    public GameObject projectilePrefab;

    void Update()
    {
        Inputs();
    }

    void Inputs()
    {
        if(canShoot)
        {
            StartCoroutine(Fire());
        }
    }

    IEnumerator Fire()
    {
        canShoot = false;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.transform.position, transform.rotation);
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
    }
}
