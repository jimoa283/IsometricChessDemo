using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataList : MonoBehaviour
{
    protected GameObject arrow;
    protected DataSlot[] dataSlots;
    protected BasePanel panel;

    //protected CheckWindow checkWindow;

    protected int slotIndex;

    protected UnityAction[] checkWindowActions;

    //protected DataSlot theNewestSlot;

    public int SlotIndex
    {
        get => slotIndex;
        set
        {
            dataSlots[slotIndex].HideDataSlot();
            slotIndex = (value + dataSlots.Length) % dataSlots.Length;
            dataSlots[slotIndex].ShowDataSlot();
            arrow.transform.position = new Vector2(arrow.transform.position.x, dataSlots[slotIndex].transform.position.y);
        }
    }

    public void Init()
    {
        arrow = TransformHelper.GetChildTransform(transform, "Arrow").gameObject;
        dataSlots = GetComponentsInChildren<DataSlot>();
        panel = GetComponentInParent<BasePanel>();
        

        foreach(var slot in dataSlots)
        {
            slot.Init();
        }

        checkWindowActions = new UnityAction[] { DoSlotAction, ExitWindow };
    }

    protected virtual void SpecialInit()
    {

    }

    private void Update()
    {
        if (UIManager.Instance.GetTopPanel() == panel)
        {
            if (Input.GetKeyDown(KeyCode.W))
                --SlotIndex;

            if (Input.GetKeyDown(KeyCode.S))
                ++SlotIndex;

            if (Input.GetKeyDown(KeyCode.I))
                ExitThisPanel();

            if (Input.GetKeyDown(KeyCode.J))
            {
                ShowCheckWindow();
            }
        }

    }

    public void SetAllSaveSlot()
    {
        EventCenter.Instance.EventTrigger("HideMissionInfo");
        for (int i = 0; i < SaveManager.Instance.SaveList.Length; i++)
        {
            if (SaveManager.Instance.SaveList[i] != null)
            {
                /* if (SaveManager.Instance.CurrentSave == SaveManager.Instance.SaveList[i])
                 {
                     dataSlots[i].SetDataSlot(SaveManager.Instance.SaveList[i]);
                     //theNewestSlot = dataSlots[i];
                 }                  
                 else
                     dataSlots[i].SetDataSlot(SaveManager.Instance.SaveList[i]);*/
                dataSlots[i].SetDataSlot(SaveManager.Instance.SaveList[i]);
            }
            else
            {
                dataSlots[i].ClearSlot();
            }
        }

        SlotIndex = 0;
        //checkWindow.gameObject.SetActive(false);
    }

    protected virtual void ExitThisPanel()
    {
        if(BattleGameManager.Instance.isInBattleScene)
           EventCenter.Instance.EventTrigger("ShowMissionInfo");
    }

    protected virtual void DoSlotAction()
    {

    }

    protected void ExitWindow()
    {
        //checkWindow.gameObject.SetActive(false);
        UIManager.Instance.PopPanel(UIPanelType.SimpleCheckWindowUI);
    }

    protected  virtual void ShowCheckWindow()
    {
        //checkWindow.OpenWindow();
    }
}
