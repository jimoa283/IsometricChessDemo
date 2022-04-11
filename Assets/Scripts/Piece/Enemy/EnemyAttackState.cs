using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(EnemyStateID enemyStateID, EnemyFSM enemyFSM) : base(enemyStateID, enemyFSM)
    {
    }

    public override void Act()
    {
        
    }

    public override void Change()
    {
        
    }

    public override void Enter()
    {
        PathManager.Instance.ClearPath();
        enemyFSM.StartCoroutine(ShowEffectRange());
    }

    IEnumerator ShowEffectRange()
    {
        BaseActionData temp = enemyFSM.baseActionData;
        enemyFSM.Enemy.SetIdleDirection(SetBattlPieceFunc.AdjustPieceDirection(enemyFSM.Enemy.currentCell, enemyFSM.bestActionCell));
        RangeManager.Instance.ShowPieceAttackRange(temp, enemyFSM.Enemy, enemyFSM.Enemy.currentCell);
        yield return new WaitForSeconds(0.5f);
        EffectRangeManager.Instance.ResigterBaseActionDate(enemyFSM.baseActionData);
        Select.Instance.ChangeSelectState(SelectStateID.ChooseActionPosState);
        Select.Instance.currentSelectState.SetPos(enemyFSM.bestActionCell);
        yield return new WaitForSeconds(1f);
        Select.Instance.StartAction();
    }
}
