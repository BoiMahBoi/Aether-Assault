using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGuider : MonoBehaviour
{
    public Transform player;
    public LineGuider lineGuiderScript;
    private SpriteRenderer arrowSprite;

    // Start is called before the first frame update
    void Start()
    {
        arrowSprite = GetComponent<SpriteRenderer>();
        arrowSprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (lineGuiderScript.outOfGame) 
        {
            arrowSprite.enabled=true;
        }

        if (!lineGuiderScript.outOfGame)
        {
            arrowSprite.enabled = false;
        }

        transform.rotation = player.rotation;


    }
}
