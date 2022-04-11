using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTwice : IPassiveEffect
{
    bool hasAction = false;

    public override void DoEffect(Piece piece)
    {
        if(!hasAction)
        {
            piece.actionCount++;
            hasAction = true;
            EventCenter.Instance.EventTrigger<Sprite, string>("ShowPassiveSkill", piece.pieceStatus.pieceSprite, "二次行动");
        }
        else
        {
            hasAction = false;
        }
    }
}
