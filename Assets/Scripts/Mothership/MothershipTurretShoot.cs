using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipTurretShoot : MonoBehaviour
{
    public float reloadTime;
    public bool canShoot;
    public GameObject firePoint;
    public GameObject projectilePrefab;

    void Update()
    {
        if (transform.parent.parent.gameObject.GetComponent<MothershipManager>().isShooting)
        {
            Inputs();
        }
    }

    void Inputs()
    {
        if(Input.GetKey(KeyCode.Space) && canShoot)
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
