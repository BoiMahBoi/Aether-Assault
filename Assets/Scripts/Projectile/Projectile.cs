using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public Collider2D col2D;
    public Rigidbody2D rb;

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
}
