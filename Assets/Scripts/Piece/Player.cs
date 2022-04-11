using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Piece
{
    public override void PieceMove(Cell cell)
    {
        foreach(var enemy in PieceQueueManager.Instance.EnemyList)
        {
            enemy.ClearHostilityLine();
        }
        base.PieceMove(cell);
    }

    public override void SetBattlePos(Cell cell)
    {
        base.SetBattlePos(cell);
        //PieceQueueManager.Instance.AddPlayer(this);
    }
}
