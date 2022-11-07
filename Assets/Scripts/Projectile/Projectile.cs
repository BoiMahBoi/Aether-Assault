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
        if (collider.gameObject.CompareTag("Projectile"))
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
        else if (collider.gameObject.CompareTag("Enemy"))
        {
            if (collider.gameObject.name == "Mothership") 
            {
                Debug.Log("The Mothership was hit!");
                collider.gameObject.GetComponent<ShieldScript>().hitShield();
                Destroy(gameObject);
            } 
            else if (collider.gameObject.name == "Starfighter")
            {
                Debug.Log("The Starfighter was hit!");
                Destroy(gameObject);
            }
            else if (collider.gameObject.name == "MothershipCannon")
            {
                Debug.Log("A Mothership cannon was hit!");
                collider.transform.parent.gameObject.GetComponent<MothershipTurretHP>().TakeDamage(damage);
                Destroy(gameObject);
            }
        } 
        else if (collider.gameObject.CompareTag("Asteroid"))
        {
            Debug.Log("An asteroid was hit!");
            collider.gameObject.GetComponent<Asteroids>().AsteroidGotShot();
            Destroy(gameObject);
        }
        else if (collider.gameObject.CompareTag("RescueZone")) {
            Debug.Log("The Planet was hit!");
            collider.gameObject.GetComponent<ShieldScript>().hitShield();
            Destroy(gameObject);
        }
    }
}
