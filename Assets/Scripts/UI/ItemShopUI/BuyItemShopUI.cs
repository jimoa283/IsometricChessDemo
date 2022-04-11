using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItemShopUI : BasePanel
{
    private BuyItemShopList buyItemShopList;

    public override void Init()
    {
        base.Init();
        buyItemShopList = TransformHelper.GetChildTransform(transform, "BagContent").GetComponent<BuyItemShopList>();
        buyItemShopList.Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)&&UIManager.Instance.GetTopPanel()==this)
            UIManager.Instance.PopPanel(UIPanelType.ItemShopBuyUI);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        UIManager.Instance.PauseBeforePanel();
        gameObject.SetActive(true);
        buyItemShopList.OpenItemList();
    }

    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
    }

    public override void OnPause()
    {
        base.OnPause();
        //gameObject.SetActive(false);
    }

    public override void OnResume()
    {
        base.OnResume();
        buyItemShopList.ReturnItemList();
    }
}
