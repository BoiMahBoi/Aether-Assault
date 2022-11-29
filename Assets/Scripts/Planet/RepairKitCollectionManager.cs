using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairKitCollectionManager : MonoBehaviour
{
    [Header("Repair-System Settings")]
    public bool EnablePlanetaryRepairkitCollection;

    [Header("Repair-System Settings")]
    public float repairZoneTime;
    private float repairZoneTimer;
    public float repairKitTime;
    private float repairKitTimer;
    public bool isRepairZoneActive;
    public bool isRepairing;

    [Header("Object References")]
    public GameObject starFighter;
    public GameObject repairZone;
    public GameObject repairKitSpawner;
    public GameObject repairKitPrefab;
    public GameObject inhabitantRescueManager;

    void Start()
    {
        starFighter = GameObject.Find("Starfighter");

        repairZoneTimer = repairZoneTime;
        repairKitTimer = repairKitTime;
    }

    void Update()
    {
        if (EnablePlanetaryRepairkitCollection)
        {
            if (starFighter.transform.gameObject.GetComponent<starfighterHealth>().currentHealth < starFighter.transform.gameObject.GetComponent<starfighterHealth>().maxHealth)
            {
                RepairZoneSpawner();
                MedkitSpawner();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == ("Starfighter"))
        {
            isRepairing = true;
<<<<<<< Updated upstream
            starFighter = collider.gameObject;
            starFighter.GetComponent<Rigidbody2D>().drag = starFighter.GetComponent<Rigidbody2D>().drag * 10;
=======
>>>>>>> Stashed changes
        }

        // disable the inhabitant rescue zone: when entering repairkit collection zone?
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name == ("Starfighter"))
        {
            isRepairing = false;
<<<<<<< Updated upstream
            starFighter.GetComponent<Rigidbody2D>().drag = starFighter.GetComponent<Rigidbody2D>().drag / 10;
            starFighter = null;
=======
>>>>>>> Stashed changes

            repairZoneTimer = repairZoneTime;
            repairKitTimer = repairKitTime;
            isRepairZoneActive = false;
            repairZone.SetActive(false);

            // disable the inhabitant rescue zone: when failing repairkit collection zone?
        }
    }

    void RepairZoneSpawner()
    {
        if (repairZoneTimer > 0)
        {
            repairZoneTimer -= Time.deltaTime;
        }
        else
        {
            if (!isRepairZoneActive)
            {
                // test if the inhabitantRescueZone is active
                    // if yes, get its current rotation
                        // if repairZone rotation is within x degrees in either direction
                            // re randomize a rotation

                transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 359f)));
                isRepairZoneActive = true;
                repairZone.SetActive(true);
            }
        }
    }

    void MedkitSpawner()
    {
        if (isRepairing)
        {
            if (repairKitTimer > 0)
            {
                repairKitTimer -= Time.deltaTime;
            }
            else
            {
                GameObject repairKit = Instantiate(repairKitPrefab, repairKitSpawner.transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 359f))));
                repairKit.GetComponent<RepairKitMovement>().SetStarfighterReference(starFighter.gameObject);

                repairZoneTimer = repairZoneTime;
                repairKitTimer = repairKitTime;
                isRepairZoneActive = false;
                repairZone.SetActive(false);

                // disable the inhabitant rescue zone: when successfully collecting repairkit?
            }
        }
    }
}
