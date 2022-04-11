using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBagUI : BasePanel
{
    private ItemList itemList;

    public override void Init()
    {
        base.Init();
        itemList = GetComponentInChildren<ItemList>();
        itemList.Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            UIManager.Instance.PopPanel(UIPanelType.ItemBagUI);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        UIManager.Instance.PauseBeforePanel();
        itemList.OpenItemList();
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
        gameObject.SetActive(false);
    }

    public override void OnResume()
    {
        base.OnResume();
        
    }
}
