using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    void Update()
    {
        if (!transform.gameObject.GetComponent<MothershipManager>().isShooting)
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
