using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InhabitantMovement : MonoBehaviour
{
    [Header("Inhabitant Settings")]
    public float moveSpeed; // decrease movement speed so its easier to lose the inhabitant due to distance?
    public float maxRescueDistance; // distance inhabitants can remain being rescued from
    public float minRescueDistance; // distance inhabitants are rescued within

    [Header("Object References")]
    public GameObject starFighter;
    public GameObject rescueManager;

    void Update()
    {
        if (starFighter != null)
        {
            if (Vector3.Distance(transform.position, starFighter.transform.position) < maxRescueDistance) // if inhabitant is within x units
            {
                transform.position = Vector3.MoveTowards(transform.position, starFighter.transform.position, moveSpeed * Time.deltaTime); // move inhabitant towards starfighter
                transform.LookAt(starFighter.transform);
            }
            else // if inhabitant is NOT within x units
            {
                // lerp between base color and red for x seconds
                Destroy(gameObject); // die
            }

            if (Vector3.Distance(transform.position, starFighter.transform.position) < minRescueDistance) // if inhabitant is within x unit
            {
                rescueManager.GetComponent<InhabitantRescueManager>().IncreaseRescueCount();
                Destroy(gameObject); // rescue
            }
        }
    }

    public void SetObjectReferences(GameObject _starFighter, GameObject _rescueManager) // receives starfighter GameObject from InhabitantRescue
    {
        starFighter = _starFighter; // store the starFighter gameobject on this instance of the inhabitant
        rescueManager = _rescueManager;
    }
}
