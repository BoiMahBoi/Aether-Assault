using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHint : MonoBehaviour
{

    public GameObject MothershipControls;
    public GameObject StarfighterControls;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) 
          || Input.GetKeyDown(KeyCode.S) 
          || Input.GetKeyDown(KeyCode.A) 
          || Input.GetKeyDown(KeyCode.D) 
          || Input.GetKeyDown(KeyCode.F) 
          || Input.GetKeyDown(KeyCode.G) 
          || Input.GetKeyDown(KeyCode.Alpha1)
          || Input.GetKeyDown(KeyCode.Alpha2) 
          || Input.GetKeyDown(KeyCode.Alpha3)
          )
        {
            MothershipControls.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)
          || Input.GetKeyDown(KeyCode.DownArrow)
          || Input.GetKeyDown(KeyCode.LeftArrow)
          || Input.GetKeyDown(KeyCode.RightArrow)
          || Input.GetKeyDown(KeyCode.RightShift)
          )
        {
            StarfighterControls.SetActive(false);
        }
    }
}
