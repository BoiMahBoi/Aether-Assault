using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothershipMovement : MonoBehaviour
{

    private GameManager gameManager;

    [Header("Movement Settings")]
    public float moveSpeed;

    [Header("SoundFX")]
    public AudioSource thrusterSound;
    public MothershipCannonManager cannonManager;
    public Rigidbody2D rb;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    { 
        if (!cannonManager.isShooting && !gameManager.gamePaused)
        {
            Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Movement(direction);
        }
    }
    
    void Movement(Vector2 direction)
    {
        direction.Normalize();
        rb.AddForce(direction * moveSpeed);
    }

    private void Update()
    {
            if (!thrusterSound.isPlaying && !cannonManager.isShooting && !gameManager.gamePaused &&
                                                                      (Input.GetKeyDown(KeyCode.UpArrow)
                                                                    || Input.GetKeyDown(KeyCode.RightArrow)
                                                                    || Input.GetKeyDown(KeyCode.DownArrow)
                                                                    || Input.GetKeyDown(KeyCode.LeftArrow)))
            {
                thrusterSound.Play();
            }

            else if (
                                                                         (!Input.GetKey(KeyCode.UpArrow)
                                                                       && !Input.GetKey(KeyCode.RightArrow)
                                                                       && !Input.GetKey(KeyCode.DownArrow)
                                                                       && !Input.GetKey(KeyCode.LeftArrow)))
            {
                thrusterSound.Stop();
            }
    }
}
