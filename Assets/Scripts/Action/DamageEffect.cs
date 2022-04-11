using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : IImpactEffect
{

    public void DoEffect(BattleObj battleObj)
    {
        bool hasShake = false;

        for (int i = 0; i < battleObj.targetPieces.Count; i++)
        {
            int damage = DamageCalculationFunc.DamageCalculationFuncModel(battleObj.owner, battleObj.targetPieces[i], battleObj.baseActionData,
                                                                        battleObj.baseActionData.HitCalculationFunc,
                                                                        battleObj.baseActionData.DamageCalculationFunc,
                                                                        battleObj.baseActionData.CriticalCalculationFunc,
                                                                        battleObj.baseActionData.OtherEffectDamageCalculationFunc);

            if(damage>0)
            {
                battleObj.targetPieces[i].PieceHealthChange(-damage);
                if (damage>10000)
                {   
                    while(damage>1000)
                    {
                        damage -= 10000;
                    }
                    if(!hasShake)
                    {
                        CameraController.Instance.CameraShake();
                        hasShake = true;
                    }
                }         
            }
            else if(damage==0)
            {
                SpecialBattleTip temp = PoolManager.Instance.GetObj("SpecialBattleTip").GetComponent<SpecialBattleTip>();
                temp.ShowNoDamageTip(battleObj.targetPieces[0].transform.position);
            }
            else
            {
                SpecialBattleTip temp = PoolManager.Instance.GetObj("SpecialBattleTip").GetComponent<SpecialBattleTip>();
                temp.ShowMissTip(battleObj.targetPieces[0].transform.position);
            }
            battleObj.targetPieces[i].pieceStatus.beDamageRate = 1;
        }

        battleObj.CheckEffect();
    }
}
