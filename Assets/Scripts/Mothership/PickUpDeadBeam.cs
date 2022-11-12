using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathBeam : MonoBehaviour
{
    private int Count = 0;
    public float p_startingChange = 0f;
    public float p_MaxChange = 10f;

    public Slider slider;
    private void Start()
    {
        slider.minValue = p_startingChange;
        slider.maxValue = p_MaxChange;
        slider.value = Count;
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Uran"))
        {
            other.gameObject.SetActive(false);
            Count = Count + 1;
            slider.value = Count;
        }
    }

}