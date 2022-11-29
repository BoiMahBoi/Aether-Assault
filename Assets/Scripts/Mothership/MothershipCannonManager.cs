using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipCannonManager : MonoBehaviour
{
    private GameManager gameManager;
    [HideInInspector]
    public float rotateDirection;

    [Header("Mothership State")]
    public bool isShooting;

    [Header("Current Cannon")]
    public int activeCannon;

    [Header("Cannons")]
    public GameObject[] cannons;
    //public GameObject[] FunctionalCannons;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rotateDirection = 1;
    }

    void Update()
    {
        switchTurret();
        DestroyedActiveTurret();
        Debug.Log("Current active cannon is: " + activeCannon);
    }

    void switchTurret()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            activeCannon = 1;
            rotateDirection = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            activeCannon = 2;
            rotateDirection = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            activeCannon = 0;
            rotateDirection = -1;
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
