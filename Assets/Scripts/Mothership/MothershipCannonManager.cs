using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipCannonManager : MonoBehaviour
{
    [Header("Mothership State")]
    public bool isShooting;

    [Header("Current Cannon")]
    public int activeCannon;

    [Header("Cannons")]
    public GameObject[] cannons;
    //public GameObject[] FunctionalCannons;

    void Update()
    {
        TurretMode();
        SwitchTurretUp();
        SwitchTurretDown();
        DestroyedActiveTurret();

        Debug.Log("Current active cannon is: " + activeCannon);
    }

    void TurretMode()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            if (cannons[0].activeSelf == false && cannons[1].activeSelf == false && cannons[2].activeSelf == false)
            {
                isShooting = false;
            }
            else
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

    void SwitchTurretUp()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isShooting)
        {
            activeCannon++;

            if (activeCannon > 2)
            {
                activeCannon = 0;
            }

            if (activeCannon < 0)
            {
                activeCannon = 2;
            }

            if (activeCannon == 0 && cannons[0].activeSelf == false)
            {
                activeCannon = 1;
            }

            if (activeCannon == 1 && cannons[1].activeSelf == false)
            {
                activeCannon = 2;
            }

            if (activeCannon == 2 && cannons[2].activeSelf == false)
            {
                activeCannon = 0;
            }
        }
    }

    void SwitchTurretDown()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && isShooting)
        {
            activeCannon--;

            if (activeCannon > 2)
            {
                activeCannon = 0;
            }

            if (activeCannon < 0)
            {
                activeCannon = 2;
            }

            if (activeCannon == 0 && cannons[0].activeSelf == false)
            {
                activeCannon = 2;
            }

            if (activeCannon == 1 && cannons[1].activeSelf == false)
            {
                activeCannon = 0;
            }

            if (activeCannon == 2 && cannons[2].activeSelf == false)
            {
                activeCannon = 1;
            }
        }
    }

    void DestroyedActiveTurret()
    {
        if (activeCannon == 0 && cannons[0].activeSelf == false)
        {
            activeCannon = 1;
        }

        if (activeCannon == 1 && cannons[1].activeSelf == false)
        {
            activeCannon = 2;
        }

        if (activeCannon == 2 && cannons[2].activeSelf == false)
        {
            activeCannon = 0;
        }
    }
}
