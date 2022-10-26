using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public GameObject cannonManager;

    void Update()
    {
        if (!cannonManager.GetComponent<MothershipCannonManager>().isShooting)
        {
            Inputs();
        }
    }

    void Inputs()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Movement(direction);
    }
    
    void Movement(Vector2 direction)
    {
        direction.Normalize();
        rb.AddForce(direction * moveSpeed);
    }
}
