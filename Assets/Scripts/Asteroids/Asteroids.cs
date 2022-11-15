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

    // Start is called before the first frame update
    void Start()
    {
        hp = 5;
        sprite = GetComponent<SpriteRenderer>();
        isFading = false;
    }

    public void AsteroidGotShot(){
        hp--;
        //Spawn dust particle
        if(hp <= 0) {

            Instantiate(explosionParticles, transform.position, transform.localRotation);

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
            Debug.Log("It's getting white in here!");
            yield return null;
        }
        isFading = false;
    }

}
