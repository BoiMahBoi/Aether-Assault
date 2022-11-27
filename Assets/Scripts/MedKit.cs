using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    public starfighterHealth starfighterHealthScript;

    // Start is called before the first frame update
    void Start()
    {
        starfighterHealthScript = GameObject.Find("Starfighter").GetComponent<starfighterHealth>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Starfighter") 
        {
            starfighterHealthScript.currentHealth += 10;
            starfighterHealthScript.UpdateHealthBar();
            Destroy(gameObject);
        }
    }
}
