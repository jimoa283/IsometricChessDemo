using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemShop : SpeakInteractiveAction
{
    protected override void ActionAfterAllSpeak()
    {
        OpenItemShop();
    }

    private void OpenItemShop()
    {
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.ItemShopUI));
    }

  
}
