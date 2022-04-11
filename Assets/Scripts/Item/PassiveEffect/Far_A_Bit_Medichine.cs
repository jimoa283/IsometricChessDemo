using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Far_A_Bit_Medichine : ActionRangePassiveEffect
{
    public override int ActionRangeChangeEffect(BaseActionData baseActionData,int range)
    {
        if(baseActionData.ActionType==ActionType.UseItem&& baseActionData.CheckTargetFunc == CheckTargetFunc.BenefitFriendTarget)
        {
                return range + 1;
        }
       
        return range;
    }

    public override void DoEffect(Piece piece)
    {
        if (EffectRangeManager.Instance.BaseActionData.ActionType == ActionType.UseItem&& EffectRangeManager.Instance.BaseActionData.CheckTargetFunc == CheckTargetFunc.BenefitFriendTarget)
        {
            
            EventCenter.Instance.EventTrigger<Sprite, string>("ShowPassiveSkill", piece.pieceStatus.pieceSprite, "再远一点");
        }
    }
}
