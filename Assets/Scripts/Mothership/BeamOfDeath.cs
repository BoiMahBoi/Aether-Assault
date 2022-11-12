using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeamOfDeath : MonoBehaviour
{
    [Header("Beam charging")]
    public int count = 0;
    public int maxCharge = 10;

    [Header("Beam State")]
    public bool beamCharged;
    public float fireTime;
    private bool isFiring = false;

    [Header("Slider")]
    public Slider slider;

    private void Start()
    {
        slider.maxValue = maxCharge;
        slider.value = count;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Uran"))
        {
            other.gameObject.SetActive(false);

            if (count >= maxCharge)
            {
                beamCharged = true;
            }
            else
            {
                count = count + 1;
                if (count >= maxCharge)
                {
                    beamCharged = true;
                }
            }
            slider.value = count;

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && beamCharged)
        {
            StartCoroutine(FireBeam());
        }
        if (Input.GetKeyUp(KeyCode.KeypadEnter) && isFiring)
        {
            Debug.Log("Stopped charging the beam...");
            StopAllCoroutines();
            isFiring = false;
        }
    }

    public IEnumerator FireBeam()
    {
        isFiring = true;
        Debug.Log("Preparing the beam!");
        yield return new WaitForSeconds(fireTime);
        Debug.Log("The Mothership destroyed the planet!");
    }
}
