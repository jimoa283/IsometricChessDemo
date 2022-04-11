using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemShopList : BaseItemListUI
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
        allItemList = ItemShopManager.Instance.CurrentShopItemList.AllItemList;
        allUseItemList = ItemShopManager.Instance.CurrentShopItemList.AllUseItemList;
        allEquipmentList = ItemShopManager.Instance.CurrentShopItemList.AllEquipmentList;
        moneyNumText = TransformHelper.GetChildTransform(transform, "MoneyNum").GetComponent<Text>();
    }

}
