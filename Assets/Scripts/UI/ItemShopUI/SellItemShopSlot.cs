using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellItemShopSlot : ItemSlot
{
    private Text priceNumText;

    public override void ClearItemSlot()
    {
        base.ClearItemSlot();
        priceNumText.gameObject.SetActive(false);
    }

    public override void Init()
    {
        base.Init();
        priceNumText = TransformHelper.GetChildTransform(transform, "ItemPrice").GetComponent<Text>();
    }

    public override void ResetSlot()
    {
        base.ResetSlot();
        priceNumText.color = Color.white;
    }

    public override void SelectSlot()
    {
        base.SelectSlot();
        priceNumText.color = Color.black;
    }

    public override void SetItemSlot(Item item)
    {
        BaseSetItemSlot(item);
        priceNumText.gameObject.SetActive(true);
        priceNumText.text = item.SellPrice.ToString();
        itemNum.text = item.Number.ToString();
    }

    public override void slotAction()
    {
        if(item!=null)
        {
            UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.CheckSellNumUI));
            EventCenter.Instance.EventTrigger<Item>("OpenSellItemShopWindow", item);
        }        
    }

    
}
