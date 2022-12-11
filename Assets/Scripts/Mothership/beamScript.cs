using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beamScript : MonoBehaviour
{

    public Transform mothershipTransform;
    public Transform planetTransform;
    public LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (mothershipTransform.position - planetTransform.position) / 2;
        transform.LookAt(planetTransform.position);
        lineRenderer.SetPosition(0, planetTransform.position);
        lineRenderer.SetPosition(1, mothershipTransform.position);
    }
}
