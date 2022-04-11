using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFindState : EnemyState
{
    private bool hasFind;

    public EnemyFindState(EnemyStateID enemyStateID, EnemyFSM enemyFSM) : base(enemyStateID, enemyFSM)
    {
    }

    public override void Act()
    {
        
    }

    public override void Change()
    {
        RangeManager.Instance.CloseMoveRange();
        if (!hasFind)
        {
            LevelManager.Instance.ChangeLevelState(LevelStateID.TurnEnd);
            enemyFSM.PerformTransition(EnemyStateID.Idle);
        }
        else
        {      
            if (enemyFSM.targetCell != enemyFSM.Enemy.currentCell)
            {
                enemyFSM.PerformTransition(EnemyStateID.Move);
            }               
            else
            {
                enemyFSM.PerformTransition(EnemyStateID.Attack);
            }
                
        }
    }

    public override void Enter()
    {      
        hasFind = enemyFSM.FindTarget();
        Change();
    }
}
