using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeamOfDeath : MonoBehaviour
{

    private GameManager gameManager;

    [Header("Beam Settings")]
    public int count = 0;
    public int maxCharge = 10;
    public float fireTime;
    //public animater planetexplosion

    [Header("Beam State")]
    public bool beamCharged;
    [HideInInspector]public bool isFiring = false;

    [Header("Slider")]
    [Tooltip("Assign the slider here")]
    public Slider slider;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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

        if (Input.GetKeyDown(KeyCode.Return) && beamCharged && !gameManager.gamePaused)
        {
            StartCoroutine(FireBeam());
        }
        if (Input.GetKeyUp(KeyCode.Return) && isFiring && !gameManager.gamePaused)
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
        //Put effects here. before game ends input explosion.
        isFiring = false;
        gameManager.GameOver("Mothership");
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GameOver("Mothership");
    }
}
