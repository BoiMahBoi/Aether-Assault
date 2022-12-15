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

    [Header("Object references")]
    public GameObject controlTip;
    public GameObject beamOfDeathObject;
    public AudioSource chargeSound;

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
                    controlTip.SetActive(true);
                }
            }
            slider.value = count;
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && beamCharged && !gameManager.gamePaused)
        {
            StartCoroutine(FireBeam());
        }
        if (Input.GetKeyUp(KeyCode.Space) && isFiring && !gameManager.gamePaused)
        {
            chargeSound.Stop();
            StopAllCoroutines();
            isFiring = false;
        }
    }

    public IEnumerator FireBeam()
    {
        isFiring = true;
        chargeSound.Play();
        yield return new WaitForSeconds(fireTime);
        chargeSound.Stop();
        //Put effects here. before game ends input explosion.
        beamOfDeathObject.SetActive(true);
        isFiring = false;
        gameManager.endGameWithBoomieHaha();
        yield return new WaitForSeconds(1);
        beamOfDeathObject.SetActive(false);
    }
}
