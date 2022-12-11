using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfighterMovement : MonoBehaviour
{
    private TrailRenderer thrusterTrail;

    [Header("Movement Settings")]
    public float Speed;
    public float rotateSpeed;
    public float speedBackwards;
    private Rigidbody2D rb;
    public AudioSource thrusterSound;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        thrusterTrail = gameObject.GetComponentInChildren<TrailRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!gameManager.gamePaused)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                thrusterSound.Stop();
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.AddForce(transform.up * Time.deltaTime * Speed);
                playThrusterSound();
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(new Vector3(0, 0, rotateSpeed) * Time.deltaTime * Speed, Space.World);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(new Vector3(0, 0, -rotateSpeed) * Time.deltaTime * Speed, Space.World);
            }

            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                thrusterSound.Stop();
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                rb.AddForce(-transform.up * Time.deltaTime * speedBackwards);

            }
        }
    }


    //Effects are handled below
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            thrusterTrail.enabled = false;
            thrusterSound.Stop();
        }

        if(!gameManager.gamePaused)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                thrusterTrail.enabled = true;
                playThrusterSound();
            }
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
