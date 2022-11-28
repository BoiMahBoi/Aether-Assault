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

    [Header("Object References")]
    public GameObject starFighter;
    public GameObject rescueZone;
    public GameObject inhabitantSpawner;
    public GameObject inhabitantPrefab;
    public GameObject repairKitCollectionManager;
    public Slider rescueSlider;

    [Header("Rescue Win")]
    public int rescueCount;
    public int maxRescueCount;
    public int fleeTime; // the starfighter has to stay alive for a certain amount of time before it can win by rescuing
    #endregion


    #region builtin methods
    void Start()
    {
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
            starFighter = collider.gameObject;

            // disable the repairkit collection zone: when entering inhabitant rescue zone?
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name == ("Starfighter"))
        {
            isRescuing = false;
            starFighter = null;

            rescueZoneTimer = rescueZoneTime;
            inhabitantRescueTimer = inhabitantRescueTime;
            isRescueZoneActive = false;
            rescueZone.SetActive(false);

            // disable the repairkit collection zone: when failing inhabitant rescue zone?
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
                // test if the inhabitantRescueZone is active
                    // if yes, get its current rotation
                        // if repairZone rotation is within x degrees in either direction
                            // re randomize a rotation

                transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 359f)));
                isRescueZoneActive = true;
                rescueZone.SetActive(true);
            }
        }
    }

    void InhabitantSpawner()
    {
        if (isRescuing)
        {
            if (starFighter != null)
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

                    // disable the repairkit collection zone: when successfully rescuing inhabitant?
                }
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
