using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseActionState : SelectState
{
    public ChooseActionState(SelectStateID selectStateID, PieceActionSelectUI pieceActionSelectUI) : base(selectStateID, pieceActionSelectUI)
    {

    }


    public override void ActionOnJ()
    {
        if(pieceActionSelectUI.ConfirmObjActive())
        {
            if(LevelManager.Instance.CurrentPiece.actionCount>0)
            {
                if (Select.Instance.currentCell.currentPiece == null)
                {
                    Select.Instance.InitExtraPieceObj();
                }
                else
                {
                    Select.Instance.OpenSelectAction(false);
                }
            }
            else if(Select.Instance.currentCell!=LevelManager.Instance.CurrentPiece.currentCell)
            {
                Select.Instance.gameObject.SetActive(false);
                LevelManager.Instance.CurrentPiece.pieceActionQueue.AddPieceAction(Select.Instance.MoveOnly);
                LevelManager.Instance.CurrentPiece.pieceActionQueue.DoPieceAction();
            }
        }
        
    }

    public override void ActionOnK()
    {
        if(pieceActionSelectUI.ActionFinishActive())
        {
            foreach(var enemy in PieceQueueManager.Instance.EnemyList)
            {
                enemy.ClearHostilityLine();
            }
            RangeManager.Instance.CloseMoveRange();
            PathManager.Instance.ClearPath();
            LevelManager.Instance.ChangeLevelState(LevelStateID.SetPieceDirection);
        }       
    }

    public override void ActionOnL()
    {
        if(pieceActionSelectUI.ShowPieceInfoActive())
        {
            UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.PieceInformationUI));
            Select.Instance.Freeze();
        }
        
    }

    public override void OnEnter()
    {
        Select.Instance.gameObject.SetActive(true);     
        Select.Instance.WakeUp();
        pieceActionSelectUI.ChangeChooseActionSelectAction();
        RangeManager.Instance.ShowPlayerMoveRange();
        SetPos(LevelManager.Instance.CurrentPiece.currentCell);        
    }

    public override void SetPos(Cell cell)
    {
        base.SetPos(cell);
        Select.Instance.SetPos(cell);       
        foreach (var enemy in PieceQueueManager.Instance.EnemyList)
        {
            enemy.ClearHostilityLine();
        }
        Select.Instance.currentCell.ShowEnemyLine();
    }  
}
