using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellItemShopUI : BasePanel
{
    private SellItemShopList sellItemShopList;

    public override void Init()
    {
        base.Init();
        sellItemShopList = TransformHelper.GetChildTransform(transform, "BagContent").GetComponent<SellItemShopList>();
        sellItemShopList.Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && UIManager.Instance.GetTopPanel() == this)
            UIManager.Instance.PopPanel(UIPanelType.ItemShopSellUI);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        UIManager.Instance.PauseBeforePanel();
        sellItemShopList.OpenItemList();
        gameObject.SetActive(true);
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
        sellItemShopList.ReturnItemList();
    }
}
