using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InhabitantRescueManager : MonoBehaviour
{
    #region attributes
    [Header("Rescue-System Settings")]
    public float rescueZoneTimer;
    private float _rescueZoneTimer;
    public float inhabitantRescueTimer;
    private float _inhabitantRescueTimer;
    public bool isRescueZoneActive;
    public bool isRescuing;

    [Header("Object References")]
    public GameObject starFighter;
    public GameObject rescueZone;
    public GameObject inhabitantPrefab;
    #endregion

    #region builtin methods
    void Start()
    {
        _rescueZoneTimer = rescueZoneTimer;
        _inhabitantRescueTimer = inhabitantRescueTimer;
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

            _rescueZoneTimer = rescueZoneTimer;
            _inhabitantRescueTimer = inhabitantRescueTimer;
            isRescueZoneActive = false;
            rescueZone.SetActive(false);
        }
    }
    #endregion

    #region custom methods
    void RescueZoneSpawner()
    {
        if (_rescueZoneTimer > 0)
        {
            _rescueZoneTimer -= Time.deltaTime;
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
                if (_inhabitantRescueTimer > 0)
                {
                    _inhabitantRescueTimer -= Time.deltaTime;
                }
                else
                {
                    GameObject inhabitant = Instantiate(inhabitantPrefab, rescueZone.transform.position, transform.GetChild(0).rotation);
                    inhabitant.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 359f)));
                    //inhabitant.transform.position += inhabitant.transform.up * 2.5f;
                    inhabitant.GetComponent<InhabitantMovement>().SetTarget(starFighter.gameObject);

                    _rescueZoneTimer = rescueZoneTimer;
                    _inhabitantRescueTimer = inhabitantRescueTimer;
                    isRescueZoneActive = false;
                    rescueZone.SetActive(false);
                }
            }
        }
    }
    #endregion
}
