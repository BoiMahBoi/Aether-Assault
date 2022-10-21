using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipTurretRotation : MonoBehaviour
{
    public float rotationSpeed;
    public float currentRotation;
    public float maxRotation;

    void Update()
    {
        if (transform.parent.parent.gameObject.GetComponent<MothershipManager>().isShooting)
        {
            Inputs();
        }
    }

    void Inputs()
    {
        float cannonRotation = Input.GetAxis("Horizontal");
        TurretRotation(cannonRotation);
    }

    void TurretRotation(float cannonRotation)
    {
        currentRotation -= cannonRotation * rotationSpeed * Time.deltaTime;
        currentRotation = Mathf.Clamp(currentRotation, -maxRotation, maxRotation);
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, currentRotation);
    }
}
