using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfighterTurretShoot : MonoBehaviour
{
    public float reloadTime;
    public bool canShoot;
    public GameObject firePoint;
    public GameObject projectilePrefab;
    public AudioSource shootSound;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && canShoot)
        {
            StartCoroutine(Fire());
        }
    }

    IEnumerator Fire()
    {
        canShoot = false;
        shootSound.Play();
        GameObject projectile = Instantiate(projectilePrefab, firePoint.transform.position, transform.rotation);
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
    }
}
