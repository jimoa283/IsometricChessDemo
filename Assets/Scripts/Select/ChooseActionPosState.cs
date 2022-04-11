using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseActionPosState : SelectState
{
    public ChooseActionPosState(SelectStateID selectStateID, PieceActionSelectUI pieceActionSelectUI) : base(selectStateID, pieceActionSelectUI)
    {

    }

    public override void ActionOnI()
    {
        EffectRangeManager.Instance.ClearEffectRange();
        Select.Instance.SetPosSimple(LevelManager.Instance.CurrentPiece.currentCell);
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.PieceActionUI));
        //Select.Instance.ChangeSelectState(SelectStateID.FreezeState);
        Select.Instance.Freeze();
    }

    public override void ActionOnJ()
    {
        if(pieceActionSelectUI.ConfirmObjActive())
        {
            foreach(var enemy in PieceQueueManager.Instance.EnemyList)
            {
                enemy.ClearHostilityLine();
            }
            Select.Instance.StartAction();
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
        Select.Instance.gameObject.SetActive(true);
        Select.Instance.WakeUp();
        pieceActionSelectUI.ChangeChooseActionPosSelectAction();
        SetPos(Select.Instance.currentCell);
        EffectRangeManager.Instance.CreateEffectRange(Select.Instance.currentCell, LevelManager.Instance.CurrentPiece);
    }

    public override void SetPos(Cell cell)
    {
        Select.Instance.SetPosForAction(cell);
        base.SetPos(cell);
    }
}
