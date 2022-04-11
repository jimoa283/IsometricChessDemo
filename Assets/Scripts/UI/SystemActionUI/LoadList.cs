using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoadList : DataList
{
    protected override void DoSlotAction()
    {
        SaveManager.Instance.LoadSave(slotIndex);
    }

    protected override void ExitThisPanel()
    {
        base.ExitThisPanel();
        UIManager.Instance.PopPanel(UIPanelType.LoadUI);
    }

    protected override void ShowCheckWindow()
    {
        if (SaveManager.Instance.SaveList[slotIndex] == null)
            return;
        CheckWindowManager.Instance.SetSimpleCheckWindow("LoadCheckWindow", checkWindowActions);
    }

}
