using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfighterMovement : MonoBehaviour
{
    private TrailRenderer thrusterTrail;
    private ParticleSystem SmokeParticles;

    [Header("Movement Settings")]
    public float Speed;
    public float rotateSpeed;
    public float speedBackwards;
    private Rigidbody2D rb;
    public AudioSource thrusterSound;

    

    // Start is called before the first frame update
    void Start()
    {
        thrusterTrail = gameObject.GetComponentInChildren<TrailRenderer>();
        SmokeParticles = gameObject.GetComponentInChildren<ParticleSystem>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKeyUp(KeyCode.W))
        {
            thrusterSound.Stop();
        }

        if (Input.GetKey(KeyCode.W)) 
        {
            rb.AddForce(transform.up * Time.deltaTime * Speed);
            playThrusterSound();
        }

        if (Input.GetKey(KeyCode.A)) 
        {
            transform.Rotate(new Vector3(0, 0, rotateSpeed) * Time.deltaTime * Speed, Space.World);   
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 0, -rotateSpeed) * Time.deltaTime * Speed, Space.World);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            thrusterSound.Stop();
        }

        if (Input.GetKey(KeyCode.S)) 
        {
            rb.AddForce(-transform.up * Time.deltaTime * speedBackwards);

        }
    }


    //Effects are handled below
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            thrusterTrail.enabled = false;
            thrusterSound.Stop();
            SmokeParticles.Stop();
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            thrusterTrail.enabled = true;
            playThrusterSound();
            SmokeParticles.Play();
        }
    }

    void playThrusterSound()
    {
        if(!thrusterSound.isPlaying)
        {
            thrusterSound.Play();
        }
    }

}
