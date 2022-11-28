using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    /*
     using 'Find..'. is bad practice especially when we already gain a reference to the starfighter on the collider detection
     */


    //public starfighterHealth starfighterHealthScript;

    // Start is called before the first frame update
    //void Start()
    //{
    //    starfighterHealthScript = GameObject.Find("Starfighter").GetComponent<starfighterHealth>();
    //}


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Starfighter")
        {
            collider.transform.gameObject.GetComponent<starfighterHealth>().currentHealth += 10;
            collider.transform.gameObject.GetComponent<starfighterHealth>().UpdateHealthBar();
            Destroy(gameObject);
        }
    }
}
