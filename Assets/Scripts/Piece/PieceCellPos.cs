using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCellPos : MonoBehaviour
{
    public int floorIndex;
    public Cell currentCell;

    public void Init(Cell cell)
    {
        currentCell = cell;
    }

   
}
