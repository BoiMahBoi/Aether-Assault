using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InhabitantRescueManager : MonoBehaviour
{
    #region attributes
    [Header("Rescue-System Settings")]
    public float rescueZoneTime; // Amount of time before each rescue zone is instantiated
    private float rescueZoneTimer; // Current time until a rescue zone is instantiatied
    public float inhabitantRescueTime; // Amount of time before an inhabitant is instantiated while in the rescue zone
    private float inhabitantRescueTimer; // current time until an inhabitant is instantiated
    public bool isRescueZoneActive;
    public bool isRescuing;
    public float planetaryRotationInhabitant;

    [Header("Object References")]
    public GameObject starFighter;
    public GameObject rescueZone;
    public GameObject inhabitantSpawner;
    public GameObject inhabitantPrefab;
    public GameObject repairKitCollectionManager;
    public Slider rescueSlider;
    public AudioSource beamSound;

    [Header("Rescue Win")]
    public int rescueCount;
    public int maxRescueCount;
    public int fleeTime; // the starfighter has to stay alive for a certain amount of time before it can win by rescuing
    #endregion


    #region builtin methods
    void Start()
    {
        starFighter = GameObject.Find("Starfighter");

        rescueSlider.maxValue = maxRescueCount;
        rescueZoneTimer = rescueZoneTime;
        inhabitantRescueTimer = inhabitantRescueTime;
    }

    void Update()
    {
        RescueZoneSpawner();
        InhabitantSpawner();
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
            isRescueZoneActive = false;
            rescueZone.SetActive(false);
        }
    }
    #endregion

    #region custom methods
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

                if (repairKitCollectionManager.transform.gameObject.GetComponent<RepairKitCollectionManager>().isRepairZoneActive)
                {
                    float planetaryRotationRepair = repairKitCollectionManager.transform.gameObject.GetComponent<RepairKitCollectionManager>().planetaryRotationRepair;

                    // ideally I want to write a function that rotates planetaryRotationInhabitant away in the direction it is already in, until its far enough away, but I have 13 minutes to complete this

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
                isRescueZoneActive = false;
                rescueZone.SetActive(false);

                // replace this with a coroutine, switching the logo with the failed logo
                repairKitCollectionManager.transform.GetChild(0).transform.gameObject.SetActive(false);
                repairKitCollectionManager.transform.gameObject.GetComponent<RepairKitCollectionManager>().isRepairZoneActive = false;
            }
        }
    }

    public void IncreaseRescueCount()
    {
        rescueCount++;
        rescueSlider.value = rescueCount;

        if (rescueCount >= maxRescueCount)
        {
            StartCoroutine("Fleeing");
        }
    }

    IEnumerator Fleeing()
    {
        yield return new WaitForSeconds(fleeTime);
        RescueWin();
    }

    void RescueWin()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GameOver("Starfighter");
    }
    #endregion
}
