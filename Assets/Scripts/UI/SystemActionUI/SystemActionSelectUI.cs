using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemActionSelectUI : BasePlayerActionSelectUI
{
    private BBActionSlot save;
    private BBActionSlot load;
    private BBActionSlot returnToTitle;

    public override void PopThisPanel()
    {
        UIManager.Instance.PopPanel(UIPanelType.SystemActionUI);
    }

    protected override void SpecialInit()
    {
        save = TransformHelper.GetChildTransform(transform, "Save").GetComponent<BBActionSlot>();
        save.Init(OpenSaveUI);

        load = TransformHelper.GetChildTransform(transform, "Load").GetComponent<BBActionSlot>();
        load.Init(OpenLoadUI);

        returnToTitle = TransformHelper.GetChildTransform(transform, "ReturnToTitle").GetComponent<BBActionSlot>();
        returnToTitle.Init(ReturnToTitle);

        bBActionSlots = new BBActionSlot[] { save, load, returnToTitle };
    }

    private void OpenSaveUI()
    {
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.SaveUI));
    }

    private void OpenLoadUI()
    {
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.LoadUI));
    }

    private void ReturnToTitle()
    {
        //BattleGameManager.Instance.BigSceneChangeEvent("StartScene");
        EventCenter.Instance.EventTrigger("BigSceneChangeFadeOut", "StartScene");
    }
}
