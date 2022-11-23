using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs;

    [Header("Coordinates")]
    public float mapX;
    public float mapY;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 2; i++)
        {
            SpawnAsteroid();
        }
        StartCoroutine(SpawnEnumerator());
    }

    public IEnumerator SpawnEnumerator()
    {
        SpawnAsteroid();
        int randomTime = Random.Range(1, 2);
        yield return new WaitForSeconds(randomTime);
        StartCoroutine(SpawnEnumerator());
    }

    public void SpawnAsteroid()
    {
        int spawnPicker = Random.Range(0, 4);

        int randomPrefab = Random.Range(0, asteroidPrefabs.Length);
        float randRotation = Random.Range(0, 359);
        Quaternion randRot = new Quaternion(0, 0, randRotation, 0);

        if (spawnPicker == 0)
        {
            Vector2 randLoc = new Vector2(Random.Range(-60,60), Random.Range(30, mapY));
            Instantiate(asteroidPrefabs[randomPrefab], randLoc, randRot, transform);
        }
        else if (spawnPicker == 1)
        {
            Vector2 randLoc = new Vector2(Random.Range(-60, 60), Random.Range(-30, -mapY));
            Instantiate(asteroidPrefabs[randomPrefab], randLoc, randRot, transform);
        }
        else if (spawnPicker == 2)
        {
            Vector2 randLoc = new Vector2(Random.Range(50, mapX), Random.Range(-40, 40));
            Instantiate(asteroidPrefabs[randomPrefab], randLoc, randRot, transform);
        }
        else
        {
            Vector2 randLoc = new Vector2(Random.Range(-50, -mapX), Random.Range(-40, 40));
            Instantiate(asteroidPrefabs[randomPrefab], randLoc, randRot, transform);
        }
    }


}
