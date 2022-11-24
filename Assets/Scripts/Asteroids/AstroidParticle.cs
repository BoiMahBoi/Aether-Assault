using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidParticle : MonoBehaviour
{

    private AudioSource boomAudio;

    // Start is called before the first frame update
    void Start()
    {
        boomAudio = GetComponent<AudioSource>();
        boomAudio.Play();
        Invoke("destroyParticles", 5f);
    }

    void destroyParticles()
    {
        Destroy(gameObject);
    }
}
