using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfighterTurretShoot : MonoBehaviour
{
    [Header("Turret Settings")]
    public float reloadTime;
    public bool canShoot;

    [Header("Object References")]
    public GameObject firePoint;
    public GameObject projectilePrefab;
    public AudioSource shootSound;
    public LineGuider guider;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameManager.gamePaused)
        {
            if (Input.GetKey(KeyCode.RightShift) && canShoot && !guider.outOfGame)
            {
                StartCoroutine(Fire());
            }
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
