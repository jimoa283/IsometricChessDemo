using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VFXObjList :MSingleton<VFXObjList>
{
    public List<VFXObj> vFXObjs;
    //private UnityAction actionEffects;
    private UnityAction afterBattleAction;
    //private ActionData actionData;
    private BattleObj battleObj;
    public bool hasActive;

    /* public VFXObjList(BattleObj battleObj,List<Cell> list)
     {
         hasActive = false;
         //actionEffects = action1;
         //afterBattleAction = action2;
         this.battleObj = battleObj;
         for (int i = 0; i < battleObj.targetCells.Count; i++)
         {
             VFXObj temp = PoolManager.Instance.GetObj(battleObj.baseActionData.VFXObjName).GetComponent<VFXObj>();
             temp.CellInit(battleObj.targetCells[i].transform.position, battleObj.targetCells[i].transform.position, battleObj.targetCells[i],this);
         }
     }

     public VFXObjList(BattleObj battleObj,List<Piece> list)
     {
         hasActive = false;
         // actionEffects = action1;
         //closeBattleAction = action2;
         this.battleObj = battleObj;
         for (int i = 0; i < battleObj.targetPieces.Count; i++)
         {
             VFXObj temp = PoolManager.Instance.GetObj(battleObj.baseActionData.VFXObjName).GetComponent<VFXObj>();
             if (battleObj.baseActionData.LaunchType == LaunchType.Instant)
                 temp.PieceInit(battleObj.targetPieces[i].transform.position, battleObj.targetPieces[i].transform.position,this);
             else
                 temp.PieceInit(battleObj.owner.transform.position, battleObj.targetPieces[i].transform.position,this);
         }
     }*/

    public void CreateVFXObjByCell(BattleObj battleObj)
    {
        hasActive = false;
        //actionEffects = action1;
        //afterBattleAction = action2;
        this.battleObj = battleObj;
        for (int i = 0; i < battleObj.targetCells.Count; i++)
        {
            VFXObj temp = PoolManager.Instance.GetObj(battleObj.baseActionData.VFXObjName).GetComponent<VFXObj>();
            temp.transform.SetParent(transform);
            //if (actionData.baseActionData.LaunchType == LaunchType.Instant)
            temp.CellInit(battleObj.targetCells[i].transform.position, battleObj.targetCells[i].transform.position, battleObj.targetCells[i]);
            /*else
                temp.CellInit(actionData.owner.transform.position, actionData.targetCells[i].transform.position, actionData.targetCells[i]);*/
        }
        //StartCoroutine(DoVFXAction());
    }

    public void CreateVFXObjByPiece(BattleObj battleObj)
    {
        hasActive = false;
        // actionEffects = action1;
        //closeBattleAction = action2;
        this.battleObj = battleObj;
        for (int i = 0; i < battleObj.targetPieces.Count; i++)
        {
            VFXObj temp = PoolManager.Instance.GetObj(battleObj.baseActionData.VFXObjName).GetComponent<VFXObj>();
            //temp.transform.SetParent(transform);
            if (battleObj.baseActionData.LaunchType == LaunchType.Instant)
                temp.PieceInit(battleObj.targetPieces[i].transform.position, battleObj.targetPieces[i].transform.position);
            else
                temp.PieceInit(battleObj.owner.transform.position, battleObj.targetPieces[i].transform.position);
        }
        //StartCoroutine(DoVFXAction());
    }

    public void VFXAction()
    {
        if(!hasActive)
        {
            hasActive = true;
            battleObj.owner.StartCoroutine(DoVFXAction());
        }      
    }

    IEnumerator DoVFXAction()
    {
        yield return new WaitForSeconds(battleObj.baseActionData.FirstWaitTime);
        //actionEffects?.Invoke();
        battleObj.CheckEffect();
        //actionEffects?.Invoke();
        //yield return new WaitForSeconds(battleObj.baseActionData.SecondWaitTime);
        //battleObj.ExpUpAction();
    }

    public void ExitVFXAction()
    {
        battleObj.owner.StartCoroutine(DoExitVFXAction());
    }

    IEnumerator DoExitVFXAction()
    {
        yield return new WaitForSeconds(battleObj.baseActionData.SecondWaitTime);
        battleObj.CloseThisBattleAction();
        //afterBattleAction?.Invoke();
    }
}
