using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipForcefieldHP : MonoBehaviour
{
    public SpriteRenderer shieldSprite;
    private bool shieldActive;
    private bool isFading;

    // builtin methods
    void Start()
    {
        isFading = false;
        shieldActive = true;
        Color tmp = shieldSprite.color;
        tmp.a = 0f;
        shieldSprite.color = tmp;
    }

    // custom methods
    /*void Update()
    {
        // For each turret that gets destroyed, turn forcefield slightly more red and transparent until fully gone
    }*/

    public void hitShield() {
        Debug.Log("Shield was hit");
        if(isFading){
            StopAllCoroutines();
            Color tmp = shieldSprite.color;
            tmp.a = 0f;
            shieldSprite.color = tmp;
        }
        StartCoroutine(ShieldFade(0.1f, 1));
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
