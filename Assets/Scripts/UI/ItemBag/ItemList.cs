using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemList : BaseItemListUI
{   

    protected override void SpecialInit()
    {
        allItemList = ItemManager.Instance.AllItemBag;
        allUseItemList = ItemManager.Instance.AllUseItemBag;
        allEquipmentList = ItemManager.Instance.AllEquipmentBag;
    }

   /* private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            --SlotIndex;

        if (Input.GetKeyDown(KeyCode.S))
            ++SlotIndex;

        if (Input.GetKeyDown(KeyCode.A))
            --BagIndex;

        if (Input.GetKeyDown(KeyCode.D))
            ++BagIndex;
    }*/

   
}
