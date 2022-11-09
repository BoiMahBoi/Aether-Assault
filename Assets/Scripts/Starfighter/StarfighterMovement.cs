using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfighterMovement : MonoBehaviour
{
    public float Speed;
    public float rotateSpeed;
    private Rigidbody2D rb;
    public AudioSource thrusterSound;
    public float speedBackwards;

    // Start is called before the first frame update
    void Start()
    {
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


    // Sounds are handled in the methods below.
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            thrusterSound.Stop();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            
            playThrusterSound();
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
