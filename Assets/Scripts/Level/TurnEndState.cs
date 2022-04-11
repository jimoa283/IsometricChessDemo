using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEndState : LevelState
{
    public TurnEndState(LevelStateID levelStateID) : base(levelStateID)
    {

    }

    public override void Act()
    {
        base.Act();
    }

    public override void Change()
    {
        LevelManager.Instance.ChangeLevelState(LevelStateID.CheckSpecEventBeforeTurnStart);
    }

    public override void Enter()
    {
        base.Enter();
        //LevelManager.Instance.CurrentPiece.hasAction = false;
        Piece piece = LevelManager.Instance.CurrentPiece;
        if(piece!=null)
        {
            piece.actionCount = LevelManager.Instance.CurrentPiece.oriActionCount;
            piece.moveStepCount = 0;
            piece.hasMove = false;
            piece.PieceBUFFList.BUFFCountDown();
            //piece.pieceActionQueue.ClearPieceAction();
        }

        piece.currentCell.pocket?.GetPocket();

        BattleManager.Instance.ResetBattleManager();

        PieceQueueManager.Instance.SetPieceActionQueue();
        Change();       
    }
}
