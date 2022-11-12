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
    public bool BeamChange;
    public float FireTime;

    public Slider slider;

    private bool IsFirening = false;
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
            
            if(Count >= p_MaxChange)
            {
                BeamChange = true;
            }
            else
            {
                slider.value = Count;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && BeamChange)
        {
            //beam fire
            StartCoroutine(FireBeam());
        }
        if (Input.GetKeyUp(KeyCode.KeypadEnter) && IsFirening)
        {
            StopCoroutine(FireBeam());
        }
    }

    public IEnumerator FireBeam()
    {
        yield return new WaitForSeconds(FireTime);
        Debug.Log("Motherships Wins!!!");
    }


}