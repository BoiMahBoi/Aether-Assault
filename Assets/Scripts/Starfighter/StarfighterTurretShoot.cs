using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfighterTurretShoot : MonoBehaviour
{
    public float reloadTime;
    public bool canShoot;
    public GameObject firePoint;
    public GameObject projectilePrefab;

    // Update is called once per frame
    void Update()
    {
        Inputs();
    }

    void Inputs() 
    {
        if (Input.GetKey(KeyCode.Return) && canShoot)
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
