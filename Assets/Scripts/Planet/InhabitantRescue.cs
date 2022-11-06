using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InhabitantRescue : MonoBehaviour
{
    public bool isRescuing;
    public GameObject inhabitantPrefab;
    public Transform inhabitantSpawnTransform;
    public GameObject starFighter;

    /*
     * IEnumerator with a random amount of yield seconds
     * after yielding, rotate rescue anchor to random rotation
     * set rescue zone active
     * 
     * limit instantiations to one per second
     */

    void Update()
    {
        if (isRescuing)
        {
            if (starFighter != null)
            {
                GameObject inhabitant = Instantiate(inhabitantPrefab, inhabitantSpawnTransform.position, transform.GetChild(0).rotation, transform.GetChild(0));

                inhabitant.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 359f)));
                //inhabitant.transform.position += inhabitant.transform.up * 2.5f;

                inhabitant.GetComponent<InhabitantMovement>().SetTarget(starFighter.gameObject);
            }
        }
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
        }
    }
}
