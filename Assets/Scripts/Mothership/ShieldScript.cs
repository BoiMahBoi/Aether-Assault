using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript: MonoBehaviour
{
    public bool hasShield;
    public float startAlpha;

    [Header("Object References")]
    public SpriteRenderer shieldSprite;
    public AudioSource shieldSound;
    private bool isFading;

    // builtin methods
    void Start()
    {
        isFading = false;
        Color tmp = shieldSprite.color;
        tmp.a = startAlpha;
        shieldSprite.color = tmp;
    }

    public void HitShield() {
        if(isFading){
            StopAllCoroutines();
            Color tmp = shieldSprite.color;
            tmp.a = 0.0f;
            shieldSprite.color = tmp;
        }
        if (hasShield)
        {
            shieldSound.Play();
        }

        StartCoroutine(ShieldFade(0.0f, 1));
    }

    public IEnumerator ShieldFade(float endValue, float duration)
    {
        isFading = true;
        Color tmp = shieldSprite.color;
        tmp.a = 1f;
        shieldSprite.color = tmp;
        float elapsedTime = 0;
        float startValue = shieldSprite.color.a;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            shieldSprite.color = new Color(shieldSprite.color.r, shieldSprite.color.g, shieldSprite.color.b, newAlpha);
            yield return null;
        }
        isFading = false;
    }

}
