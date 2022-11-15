using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePosition : MonoBehaviour
{
    //Reference to the object the healthbar needs to follow
    public Transform objectToFollow;
    //The transform of the healthbar
    RectTransform rectTransform;

    private void Awake()
    {
        //On Awake, update healthbar transform to object
        rectTransform = GetComponent<RectTransform>();
    }


    private void Update()
    {
        //On Update, if the positions of healthbar and objects do not match, update new position
        if (objectToFollow != null)
            rectTransform.anchoredPosition = objectToFollow.localPosition;
    }
}