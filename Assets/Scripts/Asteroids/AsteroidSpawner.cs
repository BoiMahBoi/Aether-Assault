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
        int randomPrefab = Random.Range(0, asteroidPrefabs.Length);
        float randX = Random.Range(-mapX, mapX);
        float randY = Random.Range(-mapY, mapY);
        Vector2 randLoc = new Vector2(randX, randY);
        float randRotation = Random.Range(0, 359);
        Quaternion randRot = new Quaternion(0, 0, randRotation, 0);
        Instantiate(asteroidPrefabs[randomPrefab], randLoc, randRot, transform);
        int randomTime = Random.Range(1, 5);
        yield return new WaitForSeconds(randomTime);
        StartCoroutine(spawnAsteroid());
    }

}
