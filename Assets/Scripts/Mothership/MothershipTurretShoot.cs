using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipTurretShoot : MonoBehaviour
{
    [Header("Turret Settings")]
    public bool canShoot;
    public bool indicatorActive = false;
    public float reloadTime;
    public int cannonNumber;

    [Header("Object References")]
    public MothershipCannonManager cannonManager;
    public GameObject firePoint;
    public GameObject projectilePrefab;
    public AudioSource shootingSound;
    public GameObject indicatorSprite;
    private GameManager gameManager;


    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (cannonManager.isShooting && cannonNumber == cannonManager.activeCannon)
        {
            if(!indicatorActive)
            {
                indicatorActive = true;
                indicatorSprite.SetActive(true);
            }
            if (/*SLET IKKE DETTE!!! (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.RightAlt)) && */ canShoot)
            {
                StartCoroutine(Fire());
            }
        }

        if (!cannonManager.isShooting || cannonNumber != cannonManager.activeCannon)
        {
            indicatorActive = false;
            indicatorSprite.SetActive(false);
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
