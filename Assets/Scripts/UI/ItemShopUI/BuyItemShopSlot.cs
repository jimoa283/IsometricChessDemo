using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItemShopSlot : ItemSlot
{ 
    private Text priceNumText;
    private Text stockNumText;
    private GameObject blackMask;

    public override void ClearItemSlot()
    {
        base.ClearItemSlot();
        priceNumText.gameObject.SetActive(false);
        stockNumText.gameObject.SetActive(false);
        blackMask.gameObject.SetActive(false);
    }

    public override void Init()
    {
        base.Init();
        priceNumText = TransformHelper.GetChildTransform(transform, "ItemPrice").GetComponent<Text>();
        stockNumText = TransformHelper.GetChildTransform(transform, "ItemStockNum").GetComponent<Text>();
        blackMask = TransformHelper.GetChildTransform(transform, "BlackMask").gameObject;
    }

    public override void ResetSlot()
    {
        base.ResetSlot();
        priceNumText.color = Color.white;
        stockNumText.color = Color.white;
    }

    public override void SelectSlot()
    {
        base.SelectSlot();
        priceNumText.color = Color.black;
        stockNumText.color = Color.black;
    }

    public override void SetItemSlot(Item item)
    {  
        BaseSetItemSlot(item);
        priceNumText.gameObject.SetActive(true);
        stockNumText.gameObject.SetActive(true);
        priceNumText.text = item.BuyPrice.ToString();
        if(item.Number>0)
        {
            stockNumText.text = item.Number.ToString();
            blackMask.SetActive(false);
        }         
        else
        {
            stockNumText.text = "已售罄";
            blackMask.SetActive(true);
        }
        Item temp = ItemManager.Instance.CheckItemInBag(item.ID);
        if(temp!=null)
        {
            itemNum.text = temp.Number.ToString();
        }
        else
        {
            itemNum.text = "0";
        }
    }

    public override void slotAction()
    {
        if(item!=null)
        {
            UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.CheckBuyNumUI));
            EventCenter.Instance.EventTrigger<Item>("OpenBuyItemShopWindow", item);
        }
        
    }
}
