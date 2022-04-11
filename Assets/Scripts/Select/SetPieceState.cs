using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPieceState : SelectState
{
    public SetPieceState(SelectStateID selectStateID, PieceActionSelectUI pieceActionSelectUI) : base(selectStateID, pieceActionSelectUI)
    {

    }

    public override void ActionOnI()
    {
        UIManager.Instance.PopPanel(UIPanelType.PieceQueueUI);
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.BattleReadyUI));
        //Select.Instance.ChangeSelectState(SelectStateID.FreezeState);
        EventCenter.Instance.EventTrigger("HideCombatantNum");
        Select.Instance.Freeze();
    }

    public override void ActionOnJ()
    {
       if(pieceActionSelectUI.SettingChangeActive())
        {
            Select.Instance.SetChangePlayer();
        }
    }

    public override void ActionOnK()
    {
        if(pieceActionSelectUI.RemoveActive())
        {
            PieceQueueManager.Instance.RemoveSettingPiece(Select.Instance.currentCell.currentPiece);
        }
    }

    public override void ActionOnL()
    {
        if(pieceActionSelectUI.ShowPieceInfoActive())
        {
            UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.PieceInformationUI));
            //Select.Instance.ChangeSelectState(SelectStateID.FreezeState);
            Select.Instance.Freeze();
        }
        
    }

    public override void OnEnter()
    {
        Debug.Log("Enter");
        Select.Instance.gameObject.SetActive(true);
        Select.Instance.WakeUp();
        //Select.Instance.SetPosSimple(BattleGameManager.Instance.pieceList[0].currentCell);
        pieceActionSelectUI.ChangeSetPieceSelectAction();
        SetPos(Select.Instance.currentCell);
    }

    public override void SetPos(Cell cell)
    {
        //Select.Instance.SetPosSimple(cell);
        Debug.Log("Move");
        Select.Instance.SetPosBeforeBattle(cell);
        base.SetPos(cell);
    }
}
