using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InhabitantRescue : MonoBehaviour
{
    public bool isRescuing;
    public GameObject inhabitantPrefab;
    public GameObject starFighter;

    void Update()
    {
        if (isRescuing)
        {
            if (starFighter != null)
            {
                GameObject inhabitant = Instantiate(inhabitantPrefab, transform.parent.position, transform.parent.rotation);

                inhabitant.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 360f)));
                inhabitant.transform.position += inhabitant.transform.up * 2.5f;

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
