using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InhabitantMovement : MonoBehaviour
{
    [Header("Inhabitant Settings")]
    public float moveSpeed;

    [Header("Starfighter Reference")]
    public GameObject starFighter;


    void Update()
    {
        if (starFighter != null)
        {
            Debug.Log(Vector3.Distance(transform.position, starFighter.transform.position));

            if (Vector3.Distance(transform.position, starFighter.transform.position) < 10.0f) // if inhabitant is within x units
            {
                Debug.Log("Inhabitant is being rescued");
                transform.position = Vector3.MoveTowards(transform.position, starFighter.transform.position, moveSpeed * Time.deltaTime); // move inhabitant towards starfighter
            }
            else // if inhabitant is NOT within x units
            {
                // lerp between base color and red for x seconds
                Debug.Log("Inhabitant was abandoned");
                Destroy(gameObject); // die
            }

            if (Vector3.Distance(transform.position, starFighter.transform.position) < 2.0f) // if inhabitant is within x unit
            {
                Debug.Log("Inhabitant was rescued");
                Destroy(gameObject); // rescue
                // starFighter.transform.GetComponent<>().rescueInhabitant();
            }
        }
    }

    public void SetTarget(GameObject _starFighter) // receives starfighter GameObject from InhabitantRescue
    {
        starFighter = _starFighter; // store the starFighter gameobject on this instance of the inhabitant
    }
}
