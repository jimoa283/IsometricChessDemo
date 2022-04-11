using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellItemShopList : BaseItemListUI
{
    /*public override void OpenItemList()
    {
        base.OpenItemList();
    }*/

    private Text moneyNumText;

    public override void OpenItemList()
    {
        base.OpenItemList();
        moneyNumText.text = GameManager.Instance.money.ToString();
    }

    public override void ReturnItemList()
    {
        base.ReturnItemList();
        moneyNumText.text = GameManager.Instance.money.ToString();
    }

    protected override void SpecialInit()
    {
        allItemList = ItemManager.Instance.AllItemBag;
        allUseItemList = ItemManager.Instance.AllUseItemBag;
        allEquipmentList = ItemManager.Instance.AllEquipmentBag;
        moneyNumText = TransformHelper.GetChildTransform(transform, "MoneyNum").GetComponent<Text>();
    }
}
