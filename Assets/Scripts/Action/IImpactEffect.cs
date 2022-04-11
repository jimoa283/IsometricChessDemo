using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    Damage,
    Treat,
    SkillCost,
    UseItemCost,
    SetBlock,
    MoveTarget,
    AddBuff
}

public interface IImpactEffect
{

    void DoEffect(BattleObj battleObj);
}
