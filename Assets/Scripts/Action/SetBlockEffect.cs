using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBlockEffect : IImpactEffect
{
    public  void DoEffect(BattleObj battleObj)
    {
        //Debug.Log(battleObj.targetCells.Count);
        foreach(var cell in battleObj.targetCells)
        {
            //CellAttachment temp = PoolManager.Instance.GetObj(battleObj.baseActionData.CellAttachmentName).GetComponent<CellAttachment>();
            //temp.transform.position = cell.transform.position + Vector3.up * 0.6f;
            //temp.FiSetCell(cell);
            CellAttachmentManager.Instance.AddCellAttachment(cell, battleObj.baseActionData.CellAttachmentName);
        }

        battleObj.CheckEffect();
    }
}
