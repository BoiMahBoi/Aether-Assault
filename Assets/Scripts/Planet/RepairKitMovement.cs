using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairKitMovement : MonoBehaviour
{
    [Header("RepairKit Settings")]
    public int repairAmount;
    public float moveSpeed;
    public float maxCollectionDistance;
    public float minCollectionDistance;
    
    [Header("Object References")]
    public GameObject starFighter;

    void Update()
    {
        if (starFighter != null)
        {
            if (Vector3.Distance(transform.position, starFighter.transform.position) < maxCollectionDistance) // if inhabitant is within x units
            {
                transform.position = Vector3.MoveTowards(transform.position, starFighter.transform.position, moveSpeed * Time.deltaTime);
                transform.LookAt(starFighter.transform);
            }
            else // if inhabitant is NOT within x units
            {
                Destroy(gameObject);
            }

            if (Vector3.Distance(transform.position, starFighter.transform.position) < minCollectionDistance) // if inhabitant is within x unit
            {
                starFighter.transform.gameObject.GetComponent<starfighterHealth>().RepairDamage(repairAmount);
                Destroy(gameObject);
            }
        }
    }

    public void SetStarfighterReference(GameObject _starFighter)
    {
        starFighter = _starFighter; 
    }
}
