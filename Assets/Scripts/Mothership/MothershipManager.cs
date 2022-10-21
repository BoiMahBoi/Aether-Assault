using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipManager : MonoBehaviour
{
    public bool isShooting;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isShooting)
            {
                isShooting = false;
            }
            else
            {
                isShooting = true;
            }
        }
    }
}
