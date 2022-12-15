using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InhabitantRescueManager : MonoBehaviour
{
    [Header("Rescue-System Settings")]
    public float rescueZoneTime; // Amount of time before each rescue zone is instantiated
    public float rescueZoneTimer; // Current time until a rescue zone is instantiatied
    public float inhabitantRescueTime; // Amount of time before an inhabitant is instantiated while in the rescue zone
    public float inhabitantRescueTimer; // current time until an inhabitant is instantiated
    public bool isRescueZoneActive;
    public bool isRescuing;
    public float planetaryRotationInhabitant;
    private bool hasRescuedEveryone = false;
    private bool isPoweringUp = false;

    [Header("Object References")]
    private GameManager gameManager;
    public GameObject starFighter;
    public GameObject warpEffect;
    public GameObject rescueZone;
    public GameObject repairZone;
    public GameObject repairZoneManager;
    public GameObject inhabitantSpawner;
    public GameObject inhabitantPrefab;
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

            rescueZoneTimer = rescueZoneTime;
            inhabitantRescueTimer = inhabitantRescueTime;
            var _repairZoneManager = repairZoneManager.transform.gameObject.GetComponent<RepairKitCollectionManager>();
            _repairZoneManager.repairZoneTimer = _repairZoneManager.repairZoneTime;
            _repairZoneManager.repairKitTimer = _repairZoneManager.repairKitTime;

            isRescueZoneActive = false;
            rescueZone.transform.gameObject.SetActive(false);

            // replace this with a coroutine, switching the logo with the failed logo
            repairZoneManager.transform.gameObject.GetComponent<RepairKitCollectionManager>().isRepairZoneActive = false;
            repairZone.transform.gameObject.SetActive(false);
        }
    }

    void RescueZoneSpawner()
    {
        if (rescueZoneTimer > 0)
        {
            rescueZoneTimer -= Time.deltaTime;
        }
        else
        {
            if (!isRescueZoneActive)
            {
                planetaryRotationInhabitant = Random.Range(0f, 359f);

                if (repairZoneManager.transform.gameObject.GetComponent<RepairKitCollectionManager>().isRepairZoneActive)
                {
                    float planetaryRotationRepair = repairZoneManager.transform.gameObject.GetComponent<RepairKitCollectionManager>().planetaryRotationRepair;

                    // ideally I want to write a function that rotates planetaryRotationInhabitant away in the direction it is already in, until its far enough away

                    if (Mathf.Abs(planetaryRotationInhabitant - planetaryRotationRepair) < 20)
                    {
                        Debug.Log("Marker was moved, too close to previously existing marker!");
                        planetaryRotationInhabitant += 40;
                    }
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
            if (inhabitantRescueTimer > 0)
            {
                inhabitantRescueTimer -= Time.deltaTime;
            }
            else
            {
                GameObject inhabitant = Instantiate(inhabitantPrefab, inhabitantSpawner.transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 359f))));
                inhabitant.GetComponent<InhabitantMovement>().SetObjectReferences(starFighter.gameObject, this.transform.gameObject);

                rescueZoneTimer = rescueZoneTime;
                inhabitantRescueTimer = inhabitantRescueTime;
                var _repairZoneManager = repairZoneManager.transform.gameObject.GetComponent<RepairKitCollectionManager>();
                _repairZoneManager.repairZoneTimer = _repairZoneManager.repairZoneTime;
                _repairZoneManager.repairKitTimer = _repairZoneManager.repairKitTime;

                isRescueZoneActive = false;
                rescueZone.transform.gameObject.SetActive(false);

                // replace this with a coroutine, switching the logo with the failed logo
                repairZoneManager.transform.gameObject.GetComponent<RepairKitCollectionManager>().isRepairZoneActive = false;
                repairZone.transform.gameObject.SetActive(false);
            }
        }
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
