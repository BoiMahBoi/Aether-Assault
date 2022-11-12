using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class PickUpDeadBeam : MonoBehaviour
{
    private int Count;
    public float p_startingChange = 0f;
    public float p_MaxChange = 10f;
    public Slider p_Slider;
    public Image p_FillImage;
    public Color p_Color = Color.red;

    private float p_CurrentChange;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Uran pick up"))
        {
            other.gameObject.SetActive(false);
            Count = Count + 1;
        }
    }

    public void CurrentChangeStatic(int count)
    {
        p_CurrentChange = p_startingChange;
        SetCountUI();
    }
    private void OnEnable()
    {
        p_CurrentChange = p_MaxChange;
        SetCountUI();
    }
    private void SetCountUI()
    {
        p_Slider.value = p_CurrentChange;

        p_FillImage.color = Color.Lerp(p_Color, p_Color, p_CurrentChange / p_startingChange);
    }
}
