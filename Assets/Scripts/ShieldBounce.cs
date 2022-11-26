using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBounce : MonoBehaviour
{

    [Range(1, 100)]public float power;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Shield") || collision.gameObject.CompareTag("RescueZone"))
        {
            Vector2 direction = transform.position - collision.transform.position;
            direction.Normalize();
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            gameObject.GetComponent<Rigidbody2D>().AddForce(direction * power);
            collision.gameObject.GetComponent<ShieldScript>().HitShield();
        }
    }
}
