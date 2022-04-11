using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaknessEffect : DamagePassiveEffect
{
    public override int DoDamageEffect(int damage,Piece piece, Piece target)
    {
        if (BattleManager.Instance.IsBattling)
            EventCenter.Instance.EventTrigger("ShowPassiveSkill", piece.pieceStatus.pieceSprite, "弱点特效");
        return (int)(damage * 1.5);
    }
}
