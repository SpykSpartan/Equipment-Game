using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NonRectangleButton : MonoBehaviour
{
    public float alphaThreshold = 0.1f;

    private void Start()
    {
        transform.GetComponent<Image>().alphaHitTestMinimumThreshold = alphaThreshold;
    }
}
