using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    #endregion

    #region builtin methods
    void Start()
    {
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
                    inhabitant.GetComponent<InhabitantMovement>().SetTarget(starFighter.gameObject);

                    rescueZoneTimer = rescueZoneTime;
                    inhabitantRescueTimer = inhabitantRescueTime;
                    isRescueZoneActive = false;
                    rescueZone.SetActive(false);
                }
            }
        }
    }
    #endregion
}
