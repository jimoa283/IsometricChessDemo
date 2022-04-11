using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SaveList : DataList
{
    protected override void DoSlotAction()
    {
        Save save = SaveManager.Instance.AddSave(slotIndex,SceneManager.GetActiveScene().name);
        for (int i = 0; i < SaveManager.Instance.SaveList.Length; i++)
        {
            if (SaveManager.Instance.SaveList[i] != null)
            {
                
                dataSlots[i].SetDataSlot(SaveManager.Instance.SaveList[i]);
            }
            else
            {
                dataSlots[i].ClearSlot();
            }
        }
        UIManager.Instance.PopPanel(UIPanelType.SimpleCheckWindowUI);
    }

    protected override void ExitThisPanel()
    {
        base.ExitThisPanel();
        UIManager.Instance.PopPanel(UIPanelType.SaveUI);
    }

    protected override void ShowCheckWindow()
    {
        CheckWindowManager.Instance.SetSimpleCheckWindow("SaveCheckWindow", checkWindowActions);
    }

}
