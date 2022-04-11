using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    private ScrollRect rect;
    public float hor;
    void Start()
    {
        rect = GetComponent<ScrollRect>();
    }

    void Update()
    {
        hor = rect.horizontalNormalizedPosition;
    }
}
