using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActiveSkill : Skill
{
    public BaseActionData BaseActionData;

    public ActiveSkill(int iD, string name, ElementType type, string info, Sprite sprite, SkillType skillType, BaseActionData baseActionData) : base(iD, name, type, info, sprite, skillType)
    {
        BaseActionData = baseActionData;
        BaseActionData.ActionType = ActionType.Skill;
    }

    public override void SetPiece(Piece piece)
    {
        base.SetPiece(piece);
        BaseActionData.owner = piece;
    }


    /* public ActiveSkill(int iD, string name, ElementType type, string info, Sprite sprite, SkillType skillType, BaseActionData baseActionData) : base(iD, name, type, info, sprite)
{
SkillType = skillType;
BaseActionData = baseActionData;
*//* Power = power;
 HMinRange = hMinRange;
 HMaxRange = hMaxRange;
 VMinRange = vMinRange;
 VMaxRange = vMaxRange;
 IsFriend = isFriend;
 AnimName = animName;        
 LaunchType = launchType;
 RangeType = rangeType;
 VFXObjName = vfxObjName;
 Effects = effectTypes;
 RangePos = rangePos;*/

    /*BaseActionData = new BaseActionData(power, hMinRange, hMaxRange, vMinRange, vMaxRange, animName, checkTargetFunc, 
                               launchType, rangeType, vfxObjName, effectTypes, rangePos);*//*
}*/
}
