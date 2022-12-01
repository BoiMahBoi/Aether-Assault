using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class MothershipTurretHP : MonoBehaviour
{
    private bool isFading = false;

    //Reference to the HealthBar
    [Header("Object References")]
    public HealthBar healthBar;
    public GameObject cannonPrefab;
    public SpriteRenderer cannonSprite;
    public int cannonNumber;
    public GameObject hitSoundObject;
    public GameObject explosionParticles;

    [Header("Repair bar slider refference")]
    public Slider repairSlider;

    [Header("Turret Settings")]
    public float maxHP;
    public float currentHP;
    public float repairTime;
    private float repairTimer;

    [Header("Turret State")]
    public bool isDestroyed;
    private GameObject cannon;

    void Start()
    {
        //Assigning value to max health
        isDestroyed = false;
        currentHP = maxHP;
        cannon = transform.GetChild(0).gameObject;
        healthBar.SetMaxHealth(maxHP);
        //Setting repairSlider to current repair time spent
        repairSlider.value = repairTimer;
        //Setting repairSliders max value to total repair time
        repairSlider.maxValue = repairTime;
    }

    public void UpdateHealthBar()
    {
        //Updating the HealthBar, so that is relates to current health
        healthBar.SetHealth(currentHP);
    }

    public void TakeDamage(float damage)
    {
        Instantiate(hitSoundObject, transform.position, transform.rotation);
        currentHP -= damage;
        UpdateHealthBar();

        if (currentHP <= 0)
        {
            //Instantiating explosion
            Instantiate(explosionParticles, transform.position, transform.localRotation);
            TurretDestroy();
        }

        if (isFading)
        {
            StopAllCoroutines();
            Color tmp = cannonSprite.color;
            tmp.r = 0.75f;
            tmp.g = 0.75f;
            tmp.b = 0.75f;
            cannonSprite.color = tmp;
        }
        StartCoroutine(BlinkWhite(0.75f, 0.5f));
    }

    void TurretDestroy()
    {
        cannon.SetActive(false);
        isDestroyed = true;

        // remove cannon[cannonNumber] from functionalCannons in MothershipCannonManager
    }

    void Update()
    {
        if (isDestroyed)
        {
            if (repairTimer < repairTime)
            {
                repairTimer += Time.deltaTime;
                repairSlider.value = repairTimer;
            }
            else
            {
                currentHP = maxHP;
                cannon.SetActive(true);
                isDestroyed = false;
                cannon.gameObject.GetComponent<MothershipTurretShoot>().canShoot = true;
                // add cannon[cannonNumber] from cannon to functionalCannons in MothershipCannonManager
                UpdateHealthBar();

                repairTimer = 0;
                repairSlider.value = repairTimer;
            }
        }
    }

    IEnumerator BlinkWhite(float endValue, float duration)
    {
        isFading = true;
        Color tmp = cannonSprite.color;
        tmp.r = 1.0f;
        tmp.g = 1.0f;
        tmp.b = 1.0f;
        cannonSprite.color = tmp;
        float elapsedTime = 0;
        float startRedValue = cannonSprite.color.r;
        float startGreenValue = cannonSprite.color.g;
        float startBlueValue = cannonSprite.color.b;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newRed = Mathf.Lerp(startRedValue, endValue, elapsedTime / duration);
            float newGreen = Mathf.Lerp(startGreenValue, endValue, elapsedTime / duration);
            float newBlue = Mathf.Lerp(startBlueValue, endValue, elapsedTime / duration);
            cannonSprite.color = new Color(newRed, newGreen, newBlue, cannonSprite.color.a);
            yield return null;
        }
        isFading = false;
    }
}
