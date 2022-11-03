using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float damage;

    void Start()
    {
        rb.velocity = transform.up * speed;
        Invoke("DeleteProjectile", 5.0f);
    }

    void DeleteProjectile()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {        
        if (!collider.gameObject.CompareTag("RescueZone"))
        {
            Destroy(gameObject);
        }

        // interface or super class named isDestroyable?
        // set the HP of projectiles to 1, so all projectiles only look for an HP class, and projectiles will deal enough damage to destroy bullets on impact

        if (collider.gameObject.CompareTag("Projectile"))
        {
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.CompareTag("Enemy"))
        {
            collider.transform.parent.gameObject.GetComponent<MothershipTurretHP>().TakeDamage(damage);
        }
    }
}
