using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionUI : MonoBehaviour
{
    private ActionSlot[] actionSlots;
    private ELementDetailShowUI eLementDetailShow;
    private UnityAction[] changeActions;
    private GameObject arrow;

    private int slotIndex;
    private int bagIndex;
    private int limitIndex;

    public int SlotIndex { get => slotIndex;
        set
        {
            actionSlots[slotIndex].ResetSlot();
            slotIndex = (value + limitIndex) % limitIndex;
            actionSlots[slotIndex].SelectAction();
            arrow.transform.position = new Vector2(arrow.transform.position.x, actionSlots[slotIndex].transform.position.y);
            if (bagIndex == 0)
                eLementDetailShow.ChangeSkillInfoShow(actionSlots[slotIndex].skill);
            else
                eLementDetailShow.ChangeUseItemInfoShow(actionSlots[slotIndex].useItem);
        }             
     }

    public int BagIndex { get => bagIndex;
        set
        {
            bagIndex = (value + changeActions.Length) % changeActions.Length;
            changeActions[bagIndex].Invoke();
        }
    }

    public void Init()
    {
        actionSlots = GetComponentsInChildren<ActionSlot>();
        eLementDetailShow = GetComponentInChildren<ELementDetailShowUI>();
        changeActions = new UnityAction[] { SetSkillAction, SetUseItemAction };
        arrow = transform.Find("Arrow").gameObject;
        foreach(var slot in actionSlots)
        {
            slot.Init();
        }
        eLementDetailShow.Init();
        limitIndex = 1;
        slotIndex = 0;
        bagIndex = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            --SlotIndex;
        if (Input.GetKeyDown(KeyCode.S))
            ++SlotIndex;

        if (Input.GetKeyDown(KeyCode.A))
            --BagIndex;

        if (Input.GetKeyDown(KeyCode.D))
            ++BagIndex;

        if (Input.GetKeyDown(KeyCode.J))
        {
            if(actionSlots[slotIndex].baseActionData!=null)
            {
                actionSlots[slotIndex].ResigterAction();
                UIManager.Instance.PopPanel(UIPanelType.PieceActionUI);
                Select.Instance.ChangeSelectState(SelectStateID.ChooseActionPosState);
            }         
        }        
    }

    public void SetAction()
    {
        changeActions[bagIndex].Invoke();
    }

    public void SetSkillAction()
    {
        DoSetSkillAction(LevelManager.Instance.CurrentPiece.pieceStatus.Weapon,LevelManager.Instance.CurrentPiece.pieceStatus.skillList);
        SlotIndex = 0;
    }


    public void SetUseItemAction()
    {
        DoSetUseItemAction(ItemManager.Instance.AllUseItemBag);
        SlotIndex = 0;
    }

    private void DoSetSkillAction(Weapon weapon,List<Skill> list)
    {
        int i = 0;
        actionSlots[0].SetSkillAction(weapon.ActiveSkill);
        for (i = 1; i < actionSlots.Length; i++)
        {
            if (list.Count < i )
                break;
            actionSlots[i].SetSkillAction(list[i-1]);
        }
        limitIndex = i;
        for (; i < actionSlots.Length; i++)
        {
            actionSlots[i].ClearSlot();
        }
    }


    private void DoSetUseItemAction(List<UseItem> list)
    {
        int i = 0;
        for (i = 0; i < actionSlots.Length; i++)
        {
            if (list.Count < i + 1)
                break;
            actionSlots[i].SetUseItemAction(list[i]);
        }
        limitIndex = i;
        for (; i < actionSlots.Length; i++)
        {
            actionSlots[i].ClearSlot();
        }
    }

}
