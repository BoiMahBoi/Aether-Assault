using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InhabitantRescueManager : MonoBehaviour
{
    [Header("Rescue-System Settings")]
    public float rescueZoneTime; // Amount of time before each rescue zone is instantiated
    public float inhabitantRescueTime; // Amount of time before an inhabitant is instantiated while in the rescue zone
    [HideInInspector] public float rescueZoneTimer; // Current time until a rescue zone is instantiatied
    [HideInInspector] public float inhabitantRescueTimer; // current time until an inhabitant is instantiated
    public float zoneTimerOffsetRNG;
    public bool isRescueZoneActive;
    public bool isRescuing;
    public float planetaryRotationInhabitant;
    private bool hasRescuedEveryone = false;
    private bool isPoweringUp = false;
    private bool isFailed = false;

    [Header("Object References")]
    private GameManager gameManager;
    public GameObject starFighter;
    public GameObject warpEffect;
    public GameObject rescueZone;
    public GameObject repairZone;
    public GameObject repairZoneManager;
    public GameObject inhabitantSpawner;
    public GameObject inhabitantPrefab;
    public GameObject RescueLogo;
    public GameObject RepairLogo;
    public GameObject failedRescueLogo;
    public GameObject failedRepairLogo;
    public Slider rescueSlider;
    public AudioSource beamSound;
    public AudioSource chargeSound;
    public AudioSource fleeSound;
    //public GameObject controlTip;

    [Header("Rescue Win")]
    public int rescueCount;
    public int maxRescueCount;
    public int fleeTime; // the starfighter has to stay alive for a certain amount of time before it can win by rescuing

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        starFighter = GameObject.Find("Starfighter");

        rescueSlider.maxValue = maxRescueCount;
        rescueZoneTimer = rescueZoneTime;
        inhabitantRescueTimer = inhabitantRescueTime;
    }

    void Update()
    {
        RescueZoneSpawner();
        InhabitantSpawner();

        if (Input.GetKeyDown(KeyCode.RightControl) && hasRescuedEveryone && !gameManager.gamePaused)
        {
            StartCoroutine(PowerUpFlee());
        }
        if (Input.GetKeyUp(KeyCode.RightControl) && isPoweringUp && !gameManager.gamePaused)
        {
            chargeSound.Stop();
            StopAllCoroutines();
            isPoweringUp = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == ("Starfighter")) 
        {
            isRescuing = true;
            beamSound.Play();
            starFighter.GetComponent<Rigidbody2D>().drag = starFighter.GetComponent<Rigidbody2D>().drag * 10;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name == ("Starfighter"))
        {
            isRescuing = false;
            beamSound.Stop();
            starFighter.GetComponent<Rigidbody2D>().drag = starFighter.GetComponent<Rigidbody2D>().drag / 10;

            rescueZoneTimer = rescueZoneTime + Random.Range(0f, zoneTimerOffsetRNG);
            inhabitantRescueTimer = inhabitantRescueTime;

            var _repairZoneManager = repairZoneManager.transform.gameObject.GetComponent<RepairKitCollectionManager>();
            _repairZoneManager.repairZoneTimer = _repairZoneManager.repairZoneTime + Random.Range(0f, zoneTimerOffsetRNG);
            _repairZoneManager.repairKitTimer = _repairZoneManager.repairKitTime;

            StartCoroutine("LogoSwitch");
        }
    }

    void RescueZoneSpawner()
    {
        if (rescueZoneTimer > 0 && !isFailed)
        {
            rescueZoneTimer -= Time.deltaTime;
        }
        else
        {
            if (!isRescueZoneActive)
            {
                if (repairZoneManager.transform.gameObject.GetComponent<RepairKitCollectionManager>().isRepairZoneActive)
                {
                    float planetaryRotationRepair = repairZoneManager.transform.gameObject.GetComponent<RepairKitCollectionManager>().planetaryRotationRepair;
                    float degreeOffset = Random.Range(-30f, 30f);
                    planetaryRotationInhabitant = planetaryRotationRepair + 180f + degreeOffset;
                }
                else
                {
                    planetaryRotationInhabitant = Random.Range(0f, 359f);
                }

                transform.rotation = Quaternion.Euler(new Vector3(0, 0, planetaryRotationInhabitant));
                isRescueZoneActive = true;
                rescueZone.SetActive(true);
            }
        }
    }

    void InhabitantSpawner()
    {
        if (isRescuing)
        {
            if (inhabitantRescueTimer > 0 && !isFailed)
            {
                inhabitantRescueTimer -= Time.deltaTime;
            }
            else
            {
                GameObject inhabitant = Instantiate(inhabitantPrefab, inhabitantSpawner.transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 359f))));
                inhabitant.GetComponent<InhabitantMovement>().SetObjectReferences(starFighter.gameObject, this.transform.gameObject);
                rescueZone.transform.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator LogoSwitch()
    {
        isFailed = true;
        failedRescueLogo.transform.gameObject.SetActive(true);
        RescueLogo.transform.gameObject.SetActive(false);
        failedRepairLogo.transform.gameObject.SetActive(true);
        RepairLogo.transform.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        isRescueZoneActive = false;
        rescueZone.transform.gameObject.SetActive(false);

        repairZoneManager.transform.gameObject.GetComponent<RepairKitCollectionManager>().isRepairZoneActive = false;
        repairZone.transform.gameObject.SetActive(false);

        failedRescueLogo.transform.gameObject.SetActive(false);
        RescueLogo.transform.gameObject.SetActive(true);
        failedRepairLogo.transform.gameObject.SetActive(false);
        RepairLogo.transform.gameObject.SetActive(true);
        isFailed = false;
    }

    public void IncreaseRescueCount()
    {
        rescueCount++;
        rescueSlider.value = rescueCount;

        if (rescueCount >= maxRescueCount)
        {
            rescueCount = maxRescueCount;
            hasRescuedEveryone = true;
            //controlTip.SetActive(true);
        }
    }

    public IEnumerator PowerUpFlee()
    {
        isPoweringUp = true;
        chargeSound.Play();
        yield return new WaitForSeconds(fleeTime);
        chargeSound.Stop();
        fleeSound.Play();
        //Put effects here. before game ends input explosion.
        starFighter.GetComponent<PolygonCollider2D>().enabled = false;
        starFighter.GetComponent<StarfighterMovement>().enabled = false;
        starFighter.GetComponent<ShieldBounce>().enabled = false;
        warpEffect.SetActive(true);
        starFighter.GetComponent<Rigidbody2D>().AddForce(starFighter.transform.up * 1000);
        isPoweringUp = false;
        yield return new WaitForSeconds(1);
        gameManager.GameOver("Starfighter");
    }
}
