using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtraFunction 
{
    public static void CloneObj(GameObject item,GameObject obj)
    {
       SpriteRenderer sr1 = item.GetComponent<SpriteRenderer>();
       SpriteRenderer sr2 = obj.GetComponent<SpriteRenderer>();

        sr2.sprite = sr1.sprite;
        sr2.sortingLayerID = sr1.sortingLayerID;
        sr2.sortingOrder = sr1.sortingOrder;

        sr2.color = new Color(1, 1, 1, 0.5f);

        obj.SetActive(true);
    }

    
}
