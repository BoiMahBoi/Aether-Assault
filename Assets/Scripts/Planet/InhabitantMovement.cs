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
            if ((starFighter.transform.position - transform.position).sqrMagnitude < 40f)
            {
                transform.position = Vector3.MoveTowards(transform.position, starFighter.transform.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                // lerp between base color and red for x seconds
                Destroy(gameObject);
            }

            if ((starFighter.transform.position - transform.position).sqrMagnitude < 1f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetTarget(GameObject _starFighter)
    {
        starFighter = _starFighter;
    }
}
