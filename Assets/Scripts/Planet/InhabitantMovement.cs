using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InhabitantMovement : MonoBehaviour
{
    public GameObject starFighter;
    public float moveSpeed;

    void Update()
    {
        if (starFighter != null)
        {
            if ((starFighter.transform.position - transform.position).sqrMagnitude < 40f) // if inhabitant is within 40 units
            {
                transform.position = Vector3.MoveTowards(transform.position, starFighter.transform.position, moveSpeed * Time.deltaTime); // move inhabitant towards starfighter
            }
            else // if inhabitant is NOT within 40 untis
            {
                // lerp between base color and red for x seconds
                Destroy(gameObject); // die
            }

            if ((starFighter.transform.position - transform.position).sqrMagnitude < 1f) // if inhabitant is within 1 unit
            {
                Destroy(gameObject); // resucue
            }
        }
    }

    public void SetTarget(GameObject _starFighter) // receives starfighter GameObject from InhabitantRescue
    {
        starFighter = _starFighter; // store the starFighter gameobject on this instance of the inhabitant
    }
}
