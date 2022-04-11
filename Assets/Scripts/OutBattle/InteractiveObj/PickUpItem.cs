using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpItem : InteractiveAction
{
    public int itemID;
    public int itemNum;

    protected override void AfterInit()
    {
        Destroy(interactiveObject.gameObject);
    }

    public override void DoAction()
    {
        interactiveObject.BaseActionOnClick();
        Item item = ItemManager.Instance.GetNumItem(itemID, 0);
        ItemManager.Instance.ChangeItemNum(item,itemNum);
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.BriefInfoUI));
        string info = "已取得\n" + item.Name + " X" + itemNum;
        EventCenter.Instance.EventTrigger<string, UnityAction>("SetBriefInfo", info,ActionAfterAllSpeak);
        InteractiveActionManager.Instance.SetInteractiveActive(interactiveID, true);
        Destroy(interactiveObject.gameObject);
    }

}
