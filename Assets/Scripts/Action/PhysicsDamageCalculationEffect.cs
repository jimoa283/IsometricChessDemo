using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDamageCalculationEffect : IImpactEffect
{
    public void DoEffect(BattleObj battleObj)
    {
       /* foreach (var target in battleObj.targetPieces)
        {
            int hitLimitValue = battleObj.owner.pieceStatus.HitRate - target.pieceStatus.Avoid;

            int temp1 = Random.Range(0, 101);
            if(temp1<=hitLimitValue)
            {
                int damage = battleObj.baseActionData.Power + battleObj.owner.pieceStatus.Power - target.pieceStatus.Defense;
                if (damage < 0)
                    damage = 0;

                if(damage>0)
                {
                    int critical = battleObj.baseActionData.Critical + battleObj.owner.pieceStatus.critical+battleObj.owner.pieceStatus.Lucky/2;
                    int temp2 = Random.Range(0, 101);
                    if (temp2 <= critical)
                    {
                        damage *= 2;
                        damage += 10000;
                    }
                }
                
                battleObj.damageList.Add(damage);
            }
            else
            {
                battleObj.damageList.Add(-1);
            }
        }

        battleObj.CheckEffect();*/
    }
}
