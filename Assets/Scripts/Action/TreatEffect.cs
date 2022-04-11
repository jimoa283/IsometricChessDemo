using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatEffect : IImpactEffect
{
    public void DoEffect(BattleObj battleObj)
    {
        foreach(var target in battleObj.targetPieces)
        {
            int treat = DamageCalculationFunc.TreatCalculationFuncModel(battleObj.owner, target, battleObj.baseActionData,
                                                                     battleObj.baseActionData.DamageCalculationFunc,
                                                                     battleObj.baseActionData.OtherEffectDamageCalculationFunc);

            target.PieceHealthChange(treat);
        }

        battleObj.CheckEffect();
    }
}
