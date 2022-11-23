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
        StartCoroutine(spawnAsteroid());
    }

    public IEnumerator spawnAsteroid()
    {
        int spawnPicker = Random.Range(1, 4);

        int randomPrefab = Random.Range(0, asteroidPrefabs.Length);

        float randXAbove = Random.Range(50, mapX);
        float randXBelow = Random.Range(-50, -mapX);
        float randYAbove = Random.Range(30, mapY);
        float randYBelow = Random.Range(-30, -mapY);

        float randRotation = Random.Range(0, 359);
        Quaternion randRot = new Quaternion(0, 0, randRotation, 0);

        if(spawnPicker == 1)
        {
            Vector2 randLoc = new Vector2(randXAbove, randYAbove);
            Instantiate(asteroidPrefabs[randomPrefab], randLoc, randRot, transform);
        } else if (spawnPicker == 2)
        {
            Vector2 randLoc = new Vector2(randXAbove, randYBelow);
            Instantiate(asteroidPrefabs[randomPrefab], randLoc, randRot, transform);
        } else if (spawnPicker == 3)
        {
            Vector2 randLoc = new Vector2(randXBelow, randYAbove);
            Instantiate(asteroidPrefabs[randomPrefab], randLoc, randRot, transform);
        } else
        {
            Vector2 randLoc = new Vector2(randXBelow, randYBelow);
            Instantiate(asteroidPrefabs[randomPrefab], randLoc, randRot, transform);
        }

        int randomTime = Random.Range(3, 10);
        yield return new WaitForSeconds(randomTime);
        StartCoroutine(spawnAsteroid());
    }

}
