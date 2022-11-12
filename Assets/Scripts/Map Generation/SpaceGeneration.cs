using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceGeneration : MonoBehaviour
{
    [Header("Coordinates")]
    public float tileAmountX;
    public float tileAmountY;

    // public float offset = 11;
    [Header("Tile References")]
    public GameObject[] tilePrefabs;

    void Start()
    {
        for (int i = 0; i < tileAmountX; i++)
        {
            for (int j = 0; j < tileAmountY; j++)
            {
                GenerateTile(i, j);
            }
        }

        transform.position = new Vector2(-tileAmountX / 2 * 11, -tileAmountY / 2 * 11);
    }

    void GenerateTile(int i, int j)
    {
        int randTile = Random.Range(0, tilePrefabs.Length);

        Instantiate(tilePrefabs[randTile], new Vector2(i * 11, j * 11), Quaternion.identity, transform);
    }
}
