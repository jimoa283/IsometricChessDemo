using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShopUI : BasePanel
{
    private ItemShopFunctuinSelectUI itemShopFunctuinSelectUI;

    public override void Init()
    {
        base.Init();
        itemShopFunctuinSelectUI = TransformHelper.GetChildTransform(transform, "ShopFunctionSelect").GetComponent<ItemShopFunctuinSelectUI>();
        itemShopFunctuinSelectUI.Init();
    }

    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.I)&&this==UIManager.Instance.GetTopPanel())
        {
            UIManager.Instance.PopPanel(UIPanelType.ItemShopUI);
            //EventCenter.Instance.EventTrigger("CancelItemShopUI");          
            MainPlayerController.Instance.WakeInteractiveObject();
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        //MainPlayerController.Instance.canControll = false;
        gameObject.SetActive(true);
    }

    public override void OnExit()
    {
        base.OnExit();
        //MainPlayerController.Instance.canControll = true;
        gameObject.SetActive(false);
    }

    public override void OnPause()
    {
        base.OnPause();
    }

    public override void OnResume()
    {
        base.OnResume();
    }
}
