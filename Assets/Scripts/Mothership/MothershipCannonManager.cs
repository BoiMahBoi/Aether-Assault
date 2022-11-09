using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipCannonManager : MonoBehaviour
{
    public bool isShooting;
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

        if (Input.GetKeyDown(KeyCode.UpArrow))
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
    }
}
