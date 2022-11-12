using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipCannonManager : MonoBehaviour
{
    [Header("Mothership State")]
    public bool isShooting;

    [Header("Current Cannon")]
    public int activeCannon;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
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

        if (Input.GetKeyDown(KeyCode.UpArrow) && isShooting)
        {
            if (activeCannon < 2)
            {
                activeCannon++;
            }
            else
            {
                activeCannon = 0;
            }

            Debug.Log("the current active cannon value is: " + activeCannon);
        } 
        else if (Input.GetKeyDown(KeyCode.DownArrow) && isShooting)
        {
            if (activeCannon == 0)
            {
                activeCannon = 2;
            }
            else
            {
                activeCannon--;
            }

            Debug.Log("the current active cannon value is: " + activeCannon);
        }
    }
}
