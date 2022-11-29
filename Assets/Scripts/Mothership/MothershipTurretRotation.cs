using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipTurretRotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed;
    public float currentRotation;
    public float maxRotation;
    public int cannonNumber;
    private float cannonRotation;

    [Header("CannonManager Reference")]
    public MothershipCannonManager cannonManager;

    void Update()
    {
        if (cannonManager.isShooting && cannonNumber == cannonManager.activeCannon)
        {
            if(Input.GetKey(KeyCode.E))
            {
                cannonRotation = 1;
            }
            if(Input.GetKey(KeyCode.Q)) {
                cannonRotation = -1;
            }
            if(Input.GetKeyUp(KeyCode.E))
            {
                cannonRotation = 0;
            }
            if(Input.GetKeyUp(KeyCode.Q))
            {
                cannonRotation = 0;
            }

            TurretRotation(cannonRotation);
        }
    }

    void TurretRotation(float cannonRotation)
    {
        currentRotation -= cannonRotation * rotationSpeed * Time.deltaTime;
        currentRotation = Mathf.Clamp(currentRotation, -maxRotation, maxRotation);
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, currentRotation);
    }
}
