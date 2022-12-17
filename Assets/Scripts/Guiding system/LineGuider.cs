using JetBrains.Annotations;
using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class LineGuider : MonoBehaviour
{
    //we make a linerenderer
    private LineRenderer lineRenderer;
    public GameObject planet;
    [HideInInspector] public bool outOfGame;

    // Start is called before the first frame update
    void Start()
    {
        outOfGame = false; 
        //we make a reference to our linerenderer and we get the component, so we can use it
        lineRenderer = GetComponent<LineRenderer>();
        //we set it to two points
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //if we are not in game
        if (outOfGame) 
        {
            //we draw a line from position 1, which is our planet 
            lineRenderer.SetPosition(0, planet.transform.position);
            //to our player
            lineRenderer.SetPosition(1, transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the game object we collided with has the tag levelboarders, then we
        if(collision.gameObject.CompareTag("Levelborders")) 
        {
            //are out of the game and the bool = false, means it draw a line to guide us back.
            outOfGame = false;
            lineRenderer.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Levelborders"))
        {
            outOfGame = true;
            lineRenderer.enabled = true;
        }
    }
}
