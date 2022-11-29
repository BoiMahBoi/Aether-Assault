using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    [Header("Asteroid Settings")]
    public int hp;
    public float minSpeed, maxSpeed;
    public GameObject explosionParticles;
    private SpriteRenderer sprite;
    private bool isFading;
    private Transform planetTransform;
    private float driftSpeed;
    private Rigidbody2D rb;
    public GameObject crystal;
    public bool isCrystalAsteroid;
    public GameObject medkit;
    public bool canDropMedkits;

    // Start is called before the first frame update
    void Start()
    {
        float randomPos = Random.Range(-20, 20);
        planetTransform = GameObject.FindGameObjectWithTag("Planet").GetComponent<Transform>();
        Vector3 driftTarget = new Vector2(planetTransform.position.x + randomPos, (planetTransform.position.y + randomPos));
        driftSpeed = Random.Range(2, 7);
        rb = GetComponent<Rigidbody2D>();
        hp = 5;
        sprite = GetComponent<SpriteRenderer>();
        rb.AddForce((driftTarget - transform.position) * driftSpeed);
        isFading = false;
    }

    public void AsteroidGotShot(){
        hp--;
        //Spawn dust particle
        if(hp <= 0) {

            Instantiate(explosionParticles, transform.position, transform.localRotation);
            if(isCrystalAsteroid)
            {
                Instantiate(crystal, transform.position, transform.localRotation);
            }
            else if(Random.Range(0, 4) == 2 && canDropMedkits) 
            {
                Instantiate(medkit, transform.position, transform.localRotation);
            }

            //Spawn asteroid explosion particle
            Destroy(gameObject);
        }
        if (isFading)
        {
            StopAllCoroutines();
            Color tmp = sprite.color;
            tmp.r = 0.75f;
            tmp.g = 0.75f;
            tmp.b = 0.75f;
            sprite.color = tmp;
        }
        StartCoroutine(BlinkWhite(0.75f, 1));
    }

    
    public IEnumerator BlinkWhite(float endValue, float duration)
    {
        isFading = true;
        Color tmp = sprite.color;
        tmp.r = 1.0f;
        tmp.g = 1.0f;
        tmp.b = 1.0f;
        sprite.color = tmp;
        float elapsedTime = 0;
        float startRedValue = sprite.color.r;
        float startGreenValue = sprite.color.g;
        float startBlueValue = sprite.color.b;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newRed = Mathf.Lerp(startRedValue, endValue, elapsedTime / duration);
            float newGreen = Mathf.Lerp(startGreenValue, endValue, elapsedTime / duration);
            float newBlue = Mathf.Lerp(startBlueValue, endValue, elapsedTime / duration);
            sprite.color = new Color(newRed, newGreen, newBlue, sprite.color.a);
            yield return null;
        }
        isFading = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Levelborders"))
        {
            Destroy(gameObject);
        }
    }
}
