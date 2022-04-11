using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairCell : MonoBehaviour
{
    private SpriteRenderer sr;
    public int floor;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<SpriteRenderer>().sortingLayerName = SortingLayerSlover.sortingLayerName[floor+1];
        }
    }
}
