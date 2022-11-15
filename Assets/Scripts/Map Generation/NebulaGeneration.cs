using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NebulaGeneration : MonoBehaviour
{
    [Header("Coordinates")]
    public float mapX;
    public float mapY;

    [Header("Amount to spawn")]
    public int spaceDetailAmount;

    [Header("Nebula References")]
    public GameObject[] spaceDetailPrefabs;

    void Start()
    {
        for (int i = 0; i < spaceDetailAmount; i++)
        {
            GenerateSpaceDetail();
        }
    }

    void GenerateSpaceDetail()
    {
        int randSpaceDetail = Random.Range(0, spaceDetailPrefabs.Length);

        float randX = Random.Range(-mapX, mapX);
        float randY = Random.Range(-mapY, mapY);
        Vector2 randLoc = new Vector2(randX, randY);

        float randRotation = Random.Range(0, 359);
        Quaternion randRot = new Quaternion(0, 0, randRotation, 0);

        if (isLocationValid(randLoc))
        {
            Instantiate(spaceDetailPrefabs[randSpaceDetail], randLoc, randRot, transform);
        }
        else
        {
            GenerateSpaceDetail();
        }
    }

    bool isLocationValid(Vector2 randLoc)
    {
        float distance = Vector2.Distance(randLoc, transform.position);

        if (distance > 40)
        {
            return true;
        }
        return false;
    }
}
