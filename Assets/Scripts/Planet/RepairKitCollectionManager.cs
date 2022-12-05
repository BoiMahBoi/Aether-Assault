using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairKitCollectionManager : MonoBehaviour
{
    [Header("Repair-System Settings")]
    public bool EnablePlanetaryRepairkitCollection;

    [Header("Repair-System Settings")]
    public float repairZoneTime;
    public float repairZoneTimer;
    public float repairKitTime;
    public float repairKitTimer;
    public bool isRepairZoneActive;
    public bool isRepairing;
    public float planetaryRotationRepair;

    [Header("Object References")]
    public GameObject starFighter;
    public GameObject repairZone;
    public GameObject rescueZone;
    public GameObject rescueZoneManager;
    public GameObject repairKitSpawner;
    public GameObject repairKitPrefab;
    public GameObject inhabitantLogoBlue;
    public GameObject inhabitantLogoRed;
    public AudioSource beamSound;

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
            beamSound.Play();
            starFighter.GetComponent<Rigidbody2D>().drag = starFighter.GetComponent<Rigidbody2D>().drag * 10;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name == ("Starfighter"))
        {
            isRepairing = false;
            beamSound.Stop();
            starFighter.GetComponent<Rigidbody2D>().drag = starFighter.GetComponent<Rigidbody2D>().drag / 10;

            repairZoneTimer = repairZoneTime;
            repairKitTimer = repairKitTime;
            rescueZoneManager.transform.gameObject.GetComponent<InhabitantRescueManager>().rescueZoneTimer = rescueZoneManager.transform.gameObject.GetComponent<InhabitantRescueManager>().rescueZoneTime;
            rescueZoneManager.transform.gameObject.GetComponent<InhabitantRescueManager>().inhabitantRescueTimer = rescueZoneManager.transform.gameObject.GetComponent<InhabitantRescueManager>().inhabitantRescueTime;

            isRepairZoneActive = false;
            repairZone.transform.gameObject.SetActive(false);

            // replace this with a coroutine, switching the logo with the failed logo
            rescueZoneManager.transform.gameObject.GetComponent<InhabitantRescueManager>().isRescueZoneActive = false;
            rescueZone.transform.gameObject.SetActive(false);
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

                if (rescueZoneManager.transform.gameObject.GetComponent<InhabitantRescueManager>().isRescueZoneActive)
                {
                    float planetaryRotationInhabitant = rescueZoneManager.transform.gameObject.GetComponent<InhabitantRescueManager>().planetaryRotationInhabitant;

                    // ideally I want to write a function that rotates planetaryRotationInhabitant away in the direction it is already in, until its far enough away

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
                rescueZoneManager.transform.gameObject.GetComponent<InhabitantRescueManager>().rescueZoneTimer = rescueZoneManager.transform.gameObject.GetComponent<InhabitantRescueManager>().rescueZoneTime;
                rescueZoneManager.transform.gameObject.GetComponent<InhabitantRescueManager>().inhabitantRescueTimer = rescueZoneManager.transform.gameObject.GetComponent<InhabitantRescueManager>().inhabitantRescueTime;

                isRepairZoneActive = false;
                repairZone.transform.gameObject.SetActive(false);

                // replace this with a coroutine, switching the logo with the failed logo
                rescueZoneManager.transform.gameObject.GetComponent<InhabitantRescueManager>().isRescueZoneActive = false;
                rescueZone.transform.gameObject.SetActive(false);
            }
        }
    }
}
