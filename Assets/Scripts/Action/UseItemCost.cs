using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemCost : IImpactEffect
{
    public  void DoEffect(BattleObj battleObj)
    {
        Debug.Log("UseItemCost");

        battleObj.CheckEffect();
    }
}
