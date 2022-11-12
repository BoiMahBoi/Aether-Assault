using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGeneration : MonoBehaviour
{
    [Header("Coordinates")]
    public float mapX;
    public float mapY;

    [Header("Amount to spawn")]
    public int asteroidAmount;

    [Header("Asteroid References")]
    public GameObject[] asteroidPrefabs;

    void Start()
    {
        for (int i = 0; i < asteroidAmount; i++)
        {
            GenerateAsteroid();
        }
    }

    void GenerateAsteroid()
    {
        int randAsteroid = Random.Range(0, asteroidPrefabs.Length);
        
        float randX = Random.Range(-mapX, mapX);
        float randY = Random.Range(-mapY, mapY);
        Vector2 randLoc = new Vector2(randX, randY);

        float randRotation = Random.Range(0, 359);
        Quaternion randRot = new Quaternion(0, 0, randRotation, 0);

        if (isLocationValid(randLoc))
        {
            Instantiate(asteroidPrefabs[randAsteroid], randLoc, randRot, transform);
        }
        else
        {
            GenerateAsteroid();
        }
    }

    bool isLocationValid(Vector2 randLoc)
    {
        float distance = Vector2.Distance(randLoc, transform.position);

        if (distance > 20)
        {
            return true;
        }
        return false;
    }
}
