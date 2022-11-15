using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    [Header("Asteroid Settings")]
    public int hp;
    public float minSpeed, maxSpeed;
    public GameObject explosionParticles;

    // Start is called before the first frame update
    void Start()
    {
        hp = 5;
    }

    public void AsteroidGotShot(){
        hp--;
        //Spawn dust particle
        if(hp <= 0) {

            Instantiate(explosionParticles);

            //Spawn asteroid explosion particle
            Destroy(gameObject);
        }
    }
}
