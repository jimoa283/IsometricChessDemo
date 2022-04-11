using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SortingLayerSlover 
{
    public static string[] sortingLayerName = { "BG2", "BG3", "BG4", "BG5", "BG6", "BG7", "BG8", "BG9", "BG10" };
    public static string[] floorIndexName = {"FirstFloor","SecondFloor" , "ThirdFloor" , "FourthFloor", "FifFloor", "SixFloor","SeventhFloor","EigthFloor","NinthFloor" };

    public static void GetSortingLayerName(SpriteRenderer sr,int floor,int num)
    {
        sr.sortingLayerName= sortingLayerName[floor];
        int order = (int)(sr.transform.position.y * -10 + num);
        sr.sortingOrder =order;
    }

    public static void CloneCellSortLayer(Cell cell, GameObject range, int num)
    {
        SpriteRenderer sr1 = cell.GetComponent<SpriteRenderer>();
        SpriteRenderer sr2 = range.GetComponent<SpriteRenderer>();

        sr2.sortingLayerID = sr1.sortingLayerID;
        sr2.sortingOrder = sr1.sortingOrder + num;
    }
}
