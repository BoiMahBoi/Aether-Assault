using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairKitCollectionManager : MonoBehaviour
{
    [Header("Repair-System Settings")]
    public bool EnablePlanetaryRepairkitCollection;

    [Header("Repair-System Settings")]
    public float repairZoneTime;
    public float repairKitTime;
    [HideInInspector] public float repairZoneTimer;
    [HideInInspector] public float repairKitTimer;
    public float zoneTimerOffsetRNG;
    public bool isRepairZoneActive;
    public bool isRepairing;
    public float planetaryRotationRepair;
    private bool isFailed = false;

    [Header("Object References")]
    public GameObject starFighter;
    public GameObject repairZone;
    public GameObject rescueZone;
    public GameObject rescueZoneManager;
    public GameObject repairKitSpawner;
    public GameObject repairKitPrefab;
    public GameObject RepairLogo;
    public GameObject RescueLogo;
    public GameObject failedRepairLogo;
    public GameObject failedRescueLogo;
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
                RepairKitSpawner();
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

            repairZoneTimer = repairZoneTime + Random.Range(0f, zoneTimerOffsetRNG);
            repairKitTimer = repairKitTime;

            var _rescueZoneManager = rescueZoneManager.transform.gameObject.GetComponent<InhabitantRescueManager>();
            _rescueZoneManager.rescueZoneTimer = _rescueZoneManager.rescueZoneTime + Random.Range(0f, zoneTimerOffsetRNG);
            _rescueZoneManager.inhabitantRescueTimer = _rescueZoneManager.inhabitantRescueTime;

            StartCoroutine("LogoSwitch");
        }
    }

    void RepairZoneSpawner()
    {
        if (repairZoneTimer > 0 && !isFailed)
        {
            repairZoneTimer -= Time.deltaTime;
        }
        else
        {
            if (!isRepairZoneActive)
            {
                if (rescueZoneManager.transform.gameObject.GetComponent<InhabitantRescueManager>().isRescueZoneActive)
                {
                    float planetaryRotationInhabitant = rescueZoneManager.transform.gameObject.GetComponent<InhabitantRescueManager>().planetaryRotationInhabitant;
                    float degreeOffset = Random.Range(-30f, 30f);
                    planetaryRotationRepair = planetaryRotationInhabitant + 180f + degreeOffset;
                }
                else
                {
                    planetaryRotationRepair = Random.Range(0f, 359f);
                }

                transform.rotation = Quaternion.Euler(new Vector3(0, 0, planetaryRotationRepair));
                isRepairZoneActive = true;
                repairZone.SetActive(true);
            }
        }
    }

    void RepairKitSpawner()
    {
        if (isRepairing)
        {
            if (repairKitTimer > 0 && !isFailed)
            {
                repairKitTimer -= Time.deltaTime;
            }
            else
            {
                GameObject repairKit = Instantiate(repairKitPrefab, repairKitSpawner.transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 359f))));
                repairKit.GetComponent<RepairKitMovement>().SetStarfighterReference(starFighter.gameObject);
                repairZone.transform.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator LogoSwitch()
    {
        isFailed = true;
        failedRepairLogo.transform.gameObject.SetActive(true);
        RepairLogo.transform.gameObject.SetActive(false);
        failedRescueLogo.transform.gameObject.SetActive(true);
        RescueLogo.transform.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        isRepairZoneActive = false;
        repairZone.transform.gameObject.SetActive(false);

        rescueZoneManager.transform.gameObject.GetComponent<InhabitantRescueManager>().isRescueZoneActive = false;
        rescueZone.transform.gameObject.SetActive(false);

        failedRepairLogo.transform.gameObject.SetActive(false);
        RepairLogo.transform.gameObject.SetActive(true);
        failedRescueLogo.transform.gameObject.SetActive(false);
        RescueLogo.transform.gameObject.SetActive(true);
        isFailed = false;
    }
}
