using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("SoundObjects")]
    public GameObject MotherSound;
    public GameObject StarSound;

    [Header("Projectile Particles")]
    public GameObject projectileExplosion;

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
            Instantiate(projectileExplosion, transform.position, transform.rotation);
        }
        else if (collider.gameObject.CompareTag("Enemy"))
        {
            if (collider.gameObject.name == "Mothership") 
            {
                if(collider.gameObject.GetComponent<MothershipHealth>().hasWorkingTurrets())
                {
                    collider.gameObject.GetComponentInChildren<ShieldScript>().HitShield();

                } else
                {
                    collider.gameObject.GetComponent<MothershipHealth>().TakeDamage(damage);
                }
                
                Destroy(gameObject);
            } 
            else if (collider.gameObject.name == "Starfighter" && gameObject.name != "StarfighterProjectile")
            {
                collider.gameObject.GetComponent<starfighterHealth>().TakeDamage(damage);
                Destroy(gameObject);
            }
            else if (collider.gameObject.name == "MothershipCannon")
            {
                collider.transform.parent.gameObject.GetComponent<MothershipTurretHP>().TakeDamage(damage);
                Destroy(gameObject);
            }
        } 
        else if (collider.gameObject.CompareTag("Asteroid"))
        {
            collider.gameObject.GetComponent<Asteroids>().AsteroidGotShot();
            Destroy(gameObject);
        }
        else if (collider.gameObject.CompareTag("RescueZone")) {
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
        else
        {
            Destroy(gameObject);
        }
    }
}
