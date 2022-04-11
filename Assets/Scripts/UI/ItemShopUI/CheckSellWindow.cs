using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSellWindow : BaseShopCheckWindow
{
    protected override void ChangeBuyNum(int num)
    {
        itemNum = num;
        itemNumText.text = num.ToString();
        surplusNumText.text = "剩余" + (item.Number - num);
        if (num == 1)
            leftArrow.SetActive(false);
        else
            leftArrow.SetActive(true);

        if (num == item.Number)
            rightArrow.SetActive(false);
        else
            rightArrow.SetActive(true);

        int totalPrice = num * item.SellPrice;
        totalMoneyText.text = totalPrice.ToString();
    }

    protected override void ConfirmAction()
    {
        item.Number -= itemNum;

        if(item.Number==0)
        {
            /*if (item.ItemType == ItemType.UseItem)
            {
                ItemManager.Instance.AllUseItemBag.Remove(item as UseItem);
            }
            else if (item.ItemType == ItemType.Equipment)
            {
                ItemManager.Instance.AllEquipmentBag.Remove(item as Equipment);
            }*/
            ItemManager.Instance.RemoveItem(item);
        }

        GameManager.Instance.money += itemNum * item.SellPrice;

        UIManager.Instance.PopPanel(UIPanelType.CheckSellNumUI);
    }

    protected override void ConfirmCancel()
    {
        UIManager.Instance.PopPanel(UIPanelType.CheckSellNumUI);
    }

    public void OpenSellItemShopWindow(Item item)
    {
        SetItemShopWindow(item);
    }

    protected override void SpecialInit()
    {
        EventCenter.Instance.AddEventListener<Item>("OpenSellItemShopWindow", OpenSellItemShopWindow);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEvent<Item>("OpenSellItemShopWindow", OpenSellItemShopWindow);
    }

    public override void SetItemShopWindow(Item item)
    {
        itemPriceText.text = item.SellPrice.ToString();
        base.SetItemShopWindow(item);
    }
}
