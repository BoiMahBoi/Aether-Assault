using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("SoundObjects")]
    public GameObject MotherSound;
    public GameObject StarSound;

    [Header("Projectile Settings")]
    public float speed;
    public int damage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;

        if(gameObject.name == "StarfighterProjectile")
        {
            Instantiate(StarSound, transform.position, transform.rotation);
        } else
        {
            Instantiate(MotherSound, transform.position, transform.rotation);
        }
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
                if(collider.gameObject.GetComponent<MothershipHealth>().hasWorkingTurrets())
                {
                    Debug.Log("Mothership still has working turrets!");
                    collider.gameObject.GetComponentInChildren<ShieldScript>().HitShield();

                } else
                {
                    Debug.Log("You damaged the mothership!");
                    collider.gameObject.GetComponent<MothershipHealth>().TakeDamage(damage);
                }
                
                Destroy(gameObject);
            } 
            else if (collider.gameObject.name == "Starfighter" && gameObject.name != "StarfighterProjectile")
            {
                Debug.Log("You damaged the Starfighter!");
                collider.gameObject.GetComponent<starfighterHealth>().TakeDamage(damage);
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
            collider.gameObject.GetComponent<ShieldScript>().HitShield();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Uran"))
        {
            Destroy(gameObject);
        }
    }

}
