using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBuffEffect : IImpactEffect
{
    public void DoEffect(BattleObj battleObj)
    {

        battleObj.owner.StartCoroutine(DoAddBuffEffect(battleObj));
    }

    IEnumerator DoAddBuffEffect(BattleObj battleObj)
    {
        foreach(var buffID in battleObj.baseActionData.BuffIDList)
        {           
            foreach(var target in battleObj.targetPieces)
            {
                target.PieceBUFFList.AddBUFF(BUFFManager.Instance.GetBUFF(buffID));
            }
            yield return new WaitForSeconds(0.2f);
        }

        battleObj.CheckEffect();
    }
}
