using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShopFunctuinSelectUI : BasePlayerActionSelectUI
{
    //private GameObject arrow;
    //private BBActionSlot[] bBActionSlots;
    private BBActionSlot buyItemShop;
    private BBActionSlot sellItemShop;

    //private int actionIndex;

    //public int ActionIndex { get => actionIndex; set => actionIndex = value; }

    //public bool canControll;

    /*public void Init()
    {
        arrow = TransformHelper.GetChildTransform(transform, "arrow").gameObject;

        buyItemShop = TransformHelper.GetChildTransform(transform, "ItemShopBuy").GetComponent<BBActionSlot>();
        buyItemShop.Init(OpenBuyItemShop);

        sellItemShop = TransformHelper.GetChildTransform(transform, "ItemShopSell").GetComponent<BBActionSlot>();
        sellItemShop.Init(OpenSellItemShop);

        bBActionSlots = new BBActionSlot[] { buyItemShop, sellItemShop };

        actionIndex = 0;
    }*/

   /* private void Update()
    {
        if (UIManager.Instance.GetPanel(UIPanelType.ItemShopSelectUI) == UIManager.Instance.GetTopPanel())
        {
           
        }
    }*/

    /* public void ChangeSelectAction()
     {
         bBActionSlots[actionIndex].BeSelected();
         arrow.transform.position = new Vector3(arrow.transform.position.x, bBActionSlots[actionIndex].transform.position.y, arrow.transform.position.z);
     }

     public void ResetLastAction()
     {
         bBActionSlots[actionIndex].ResetSlot();
     }*/

    private void OpenBuyItemShop()
    {
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.ItemShopBuyUI));
    }

    private void OpenSellItemShop()
    {
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.ItemShopSellUI));
    }

    protected override void SpecialInit()
    {
        buyItemShop = TransformHelper.GetChildTransform(transform, "ItemShopBuy").GetComponent<BBActionSlot>();
        buyItemShop.Init(OpenBuyItemShop);

        sellItemShop = TransformHelper.GetChildTransform(transform, "ItemShopSell").GetComponent<BBActionSlot>();
        sellItemShop.Init(OpenSellItemShop);

        bBActionSlots = new BBActionSlot[] { buyItemShop, sellItemShop };
    }
}
