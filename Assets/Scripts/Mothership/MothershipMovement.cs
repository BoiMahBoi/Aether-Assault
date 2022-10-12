using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipMovement : MonoBehaviour
{
    // attributes
    public float moveSpeed;
    public int shipHP;
    public GameObject forceField;
    public GameObject hullCollider;
    public Rigidbody2D rb;

    // builtin methods
    void Start()
    {
        // rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Inputs();
    }

    // custom methods
    void Inputs()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        direction.Normalize();
        Movement(direction);
    }
    
    void Movement(Vector2 direction)
    {
        rb.AddForce(direction * moveSpeed);
    }
}
