using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairKitMovement : MonoBehaviour
{
    [Header("RepairKit Settings")]
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
                Debug.Log("RepairKit is being collected");
                transform.position = Vector3.MoveTowards(transform.position, starFighter.transform.position, moveSpeed * Time.deltaTime);
            }
            else // if inhabitant is NOT within x units
            {
                Debug.Log("RepairKit was lost");
                Destroy(gameObject);
            }

            if (Vector3.Distance(transform.position, starFighter.transform.position) < minCollectionDistance) // if inhabitant is within x unit
            {
                Debug.Log("RepairKit was collected");
                starFighter.transform.gameObject.GetComponent<starfighterHealth>().currentHealth += 10;
                starFighter.transform.gameObject.GetComponent<starfighterHealth>().UpdateHealthBar();
                Destroy(gameObject);
            }
        }
    }

    public void SetStarfighterReference(GameObject _starFighter)
    {
        starFighter = _starFighter; 
    }
}
