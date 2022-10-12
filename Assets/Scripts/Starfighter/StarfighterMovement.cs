using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfighterMovement : MonoBehaviour
{
    public float Speed;
    public float rotateSpeed;
    private Rigidbody2D Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Speed = 200f;
        rotateSpeed = 2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow)) 
        {
            Rigidbody.AddForce(transform.up * Time.deltaTime * Speed);
        }

        if (Input.GetKey(KeyCode.RightArrow)) 
        {
            transform.Rotate(new Vector3(0, 0, rotateSpeed) * Time.deltaTime * Speed, Space.World);   
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, 0, -rotateSpeed) * Time.deltaTime * Speed, Space.World);
        }
    }
}
