using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipTurretShoot : MonoBehaviour
{
    public bool canShoot;
    public float reloadTime;
    public int cannonNumber;
    public MothershipCannonManager cannonManager;
    public GameObject firePoint;
    public GameObject projectilePrefab;
    public AudioSource shootingSound;

    void Update()
    {
        if (cannonManager.isShooting && cannonNumber == cannonManager.activeCannon)
        {
            if (Input.GetKey(KeyCode.M) && canShoot)
            {
                StartCoroutine(Fire());
            }
        }
    }

    IEnumerator Fire()
    {
        canShoot = false;
        shootingSound.Play();
        GameObject projectile = Instantiate(projectilePrefab, firePoint.transform.position, transform.rotation);
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
    }
}
