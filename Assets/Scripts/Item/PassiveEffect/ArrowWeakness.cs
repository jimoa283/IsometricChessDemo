using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowWeakness : DamagePassiveEffect
{
    public override int DoDamageEffect(int damage,Piece piece,Piece target)
    {
        if (BattleManager.Instance.IsBattling)
            EventCenter.Instance.EventTrigger<Sprite, string>("ShowPassiveSkill", piece.pieceStatus.pieceSprite, "飞鹰的弱点");

        return (int)(damage * 1.5f);
    }
}
