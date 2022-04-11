using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttack : IPassiveEffect
{
    public override void DoEffect(Piece piece)
    {
        if (!BattleManager.Instance.IsBattling)
            return;
        if (BattleManager.Instance.countAttackCount == 0)
            return;
        if (BattleManager.Instance.CurrentBattleObj.owner == piece)
            return;
        Piece target = BattleManager.Instance.CurrentBattleObj.owner;
        if (target != LevelManager.Instance.CurrentPiece)
            return;

        //int num = Battle;
        //if (num <= 30)
        //{

        List<Piece> temp1 = new List<Piece>();
        List<Cell> temp2 = new List<Cell>();
        temp1.Add(target);
        temp2.Add(target.currentCell);
        LookDirection lookDirection = SetBattlPieceFunc.AdjustPieceDirection(piece.currentCell, temp2[0]);
       
        BattleManager.Instance.AddBattleObj(piece, temp2, temp1, piece.pieceStatus.Weapon.ActiveSkill.BaseActionData,lookDirection ,true);
        BattleManager.Instance.countAttackCount--;
        //EventCenter.Instance.EventTrigger<Sprite, string>("ShowPassiveSkill", piece.pieceStatus.pieceSprite, "反击之势");
        //}           
    }
}
