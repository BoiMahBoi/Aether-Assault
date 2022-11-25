using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class starBlinker : MonoBehaviour
{
    public Color startcolor = Color.white;
    public Color endcolor = Color.black;
    [Range(0f, 8f)]
    public float fadetime = 1;

    Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        rend.material.color = Color.Lerp(startcolor, endcolor, Mathf.PingPong(Time.time * fadetime, 1)); 
    }
}
