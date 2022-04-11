using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TriggerTimeType
{
    Always,
    TurnStart,
    Move,
    AfterAction,
    BeAttack,
    AfterBeAttack,
    Attack,
    TurnEnd,
    ShowAction,
    SelectAction
}

public class PassiveSkill : Skill
{
    public List<TriggerTimeType> TriggerTimeTypeList;
    //public UnityAction<Piece> PassiveSkillEffect;
    public IPassiveEffect PassiveSkillEffect;

    public PassiveSkill(int iD, string name, ElementType type, string info, Sprite sprite, SkillType skillType,
        List<TriggerTimeType> triggerTimeTypeList,IPassiveEffect passiveSkillEffect) : base(iD, name, type, info, sprite, skillType)
    {
        TriggerTimeTypeList = triggerTimeTypeList;
        PassiveSkillEffect = passiveSkillEffect;
    }
}
