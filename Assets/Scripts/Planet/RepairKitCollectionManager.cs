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
    public float planetaryRotationRepair;

    [Header("Object References")]
    public GameObject starFighter;
    public GameObject repairZone;
    public GameObject repairKitSpawner;
    public GameObject repairKitPrefab;
    public GameObject inhabitantRescueManager;
    public GameObject inhabitantLogoBlue;
    public GameObject inhabitantLogoRed;

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
            starFighter.GetComponent<Rigidbody2D>().drag = starFighter.GetComponent<Rigidbody2D>().drag * 10;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name == ("Starfighter"))
        {
            isRepairing = false;
            starFighter.GetComponent<Rigidbody2D>().drag = starFighter.GetComponent<Rigidbody2D>().drag / 10;

            repairZoneTimer = repairZoneTime;
            repairKitTimer = repairKitTime;
            isRepairZoneActive = false;
            repairZone.SetActive(false);
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
                planetaryRotationRepair = Random.Range(0f, 359f);

                if (inhabitantRescueManager.transform.gameObject.GetComponent<InhabitantRescueManager>().isRescueZoneActive)
                {
                    float planetaryRotationInhabitant = inhabitantRescueManager.transform.gameObject.GetComponent<InhabitantRescueManager>().planetaryRotationInhabitant;

                    // ideally I want to write a function that rotates planetaryRotationInhabitant away in the direction it is already in, until its far enough away, but I have 13 minutes to complete this

                    if (Mathf.Abs(planetaryRotationRepair - planetaryRotationInhabitant) < 20)
                    {
                        Debug.Log("Marker was moved, too close to previously existing marker!");
                        planetaryRotationRepair += 40;
                    }
                }

                transform.rotation = Quaternion.Euler(new Vector3(0, 0, planetaryRotationRepair));
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

                // replace this with a coroutine, switching the logo with the failed logo
                inhabitantRescueManager.transform.GetChild(0).transform.gameObject.SetActive(false);
                inhabitantRescueManager.transform.gameObject.GetComponent<InhabitantRescueManager>().isRescueZoneActive = false;
            }
        }
    }
}
