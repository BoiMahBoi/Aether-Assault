using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGuider : MonoBehaviour
{
    public GameObject player;
    public SpriteRenderer arrow;

    // Start is called before the first frame update
    void Start()
    {
        arrow.transform.position = player.transform.position;
        //use the public bool out of game by packing system with dot. 
        
    }

    // Update is called once per frame
    void Update()
    {
        //if outOfGame.player

        
    }
}
