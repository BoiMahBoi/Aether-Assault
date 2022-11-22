using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InhabitantRescueManager : MonoBehaviour
{
    #region attributes
    [Header("Rescue-System Settings")]
    public float rescueZoneTime;
    private float rescueZoneTimer;
    public float inhabitantRescueTime;
    private float inhabitantRescueTimer;
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
        if (collider.gameObject.name == ("StarFighter")) 
        {
            isRescuing = true;
            starFighter = collider.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name == ("StarFighter"))
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
