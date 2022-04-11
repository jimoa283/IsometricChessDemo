using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCost : IImpactEffect
{
    public  void DoEffect(BattleObj battleObj)
    {
        Debug.Log("SkillCost");

        battleObj.CheckEffect();
    }
}
