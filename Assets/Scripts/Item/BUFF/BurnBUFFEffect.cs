using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnBUFFEffect : IPassiveEffect
{
    public override void DoEffect(Piece piece)
    {
        int damage = piece.pieceStatus.maxHealth / 6;
        if (damage >= piece.pieceStatus.CurrentHealth)
            damage = piece.pieceStatus.CurrentHealth - 1;
        piece.PieceHealthChange(-damage);
        /*HealthChangeNumText temp = PoolManager.Instance.GetObj("HealthChangeNumText").GetComponent<HealthChangeNumText>();
        temp.Init(true, piece, damage);*/
    }

   
}
