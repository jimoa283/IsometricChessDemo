using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    

    public EnemyMoveState(EnemyStateID enemyStateID, EnemyFSM enemyFSM) : base(enemyStateID, enemyFSM)
    {
    }

    public override void Act()
    {
        
    }

    public override void Change()
    {
        if(enemyFSM.bestActionCell!=null&&enemyFSM.Enemy.actionCount>0)
        {
            enemyFSM.PerformTransition(EnemyStateID.Attack);
        }
        else
        {
            LevelManager.Instance.ChangeLevelState(LevelStateID.TurnEnd);
            enemyFSM.PerformTransition(EnemyStateID.Idle);
        }
    }

    public override void Enter()
    {
        enemyFSM.Enemy.PieceMove(enemyFSM.targetCell);
    }
}
