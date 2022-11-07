using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{

    private int hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = 5;
    }

    public void AsteroidGotShot(){
        hp--;
        //Spawn dust particle
        if(hp <= 0) {
            //Spawn asteroid explosion particle
            Destroy(gameObject);
        }
    }
}
