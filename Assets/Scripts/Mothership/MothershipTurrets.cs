using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipTurrets : MonoBehaviour
{
    // attributes
    public float cannonRotationSpeed;
    public float currentRotation;
    public float maxRotation;
    public float fireRate;
    public bool canShoot;
    public int cannon1HP;
    public int cannon2HP;
    public int cannon3HP;
    public GameObject[] cannons;
    public GameObject[] cannonFirePoints;
    public GameObject projectilePrefab;

    // builtin methods
    public void Start()
    {
        // store current rotation of turrets to use in turretrotation instead of using CannonAnchors?
        canShoot = true;
    }

    public void Update()
    {
        Inputs();
    }

    // onEnable()

    // onDisable()

    // custom methods
    public void Inputs()
    {
        // Toggle between movement and shooting?
        // Always shoot all guns?
        // wasd movement, cv rotate guns, always shoot <-
        
        float cannonRotation = Input.GetAxis("Mothership Cannon Rotation");
        TurretRotation(cannonRotation);

        if(Input.GetKey(KeyCode.Space) && canShoot)
        {
            StartCoroutine(Fire());
        }
    }

    public void TurretRotation(float cannonRotation)
    {
        // rotate turrets within clamps

        currentRotation -= cannonRotation * cannonRotationSpeed * Time.deltaTime;
        currentRotation = Mathf.Clamp(currentRotation, -maxRotation, maxRotation);

        for (int i = 0; i < cannons.Length; i++)
        {
            cannons[i].transform.localRotation = Quaternion.Euler(0.0f, 0.0f, currentRotation);
        }
    }

    public IEnumerator Fire()
    {
        // instantiate projectilePrefab

        canShoot = false;

        for (int i = 0; i < cannons.Length; i++)
        {
            if (cannons[i].activeSelf)
            {
                GameObject projectile = Instantiate(projectilePrefab, cannonFirePoints[i].transform.position, cannons[i].transform.rotation);
            }
        }
        
        yield return new WaitForSeconds(fireRate);

        canShoot = true;
    }
}
