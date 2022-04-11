using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnStartState : LevelState
{
    public TurnStartState(LevelStateID levelStateID) : base(levelStateID)
    {

    }

    public override void Act()
    {
      
    }

    public override void Change()
    {
        
    }

    public override void Enter()
    {
        Piece piece = PieceQueueManager.Instance.TheFirstPiece();
        LevelManager.Instance.CurrentPiece = piece;
        CellManager.Instance.CellAttachmentCountDown();
        piece.PieceTimeProcessor.TurnStartTimeProcessorCheck();
        
        EventCenter.Instance.EventTrigger("CurrentPieceUIChange", LevelManager.Instance.CurrentPiece);
        EventCenter.Instance.EventTrigger("SetPiecesUI", PieceQueueManager.Instance.GetPieceQueue());

        piece.StartCoroutine(DoTurnStart(piece));
    }

    IEnumerator DoTurnStart(Piece piece)
    {
        Select.Instance.gameObject.SetActive(true);
        Select.Instance.SetPosSimple(piece.currentCell);
        Select.Instance.PieceActionSelectUI.gameObject.SetActive(false);
        piece.preCell = piece.currentCell;
        yield return new WaitForSeconds(0.2f);           
        if (piece.CompareTag("Player"))
            Select.Instance.ChangeSelectState(SelectStateID.ChooseActionState);
        else
        {           
            yield return new WaitForSeconds(0.5f);
            Select.Instance.gameObject.SetActive(false);
            Enemy enemy = piece as Enemy;
            enemy.EnemyFSM.PerformTransition(EnemyStateID.Find);
        }
    }
}
