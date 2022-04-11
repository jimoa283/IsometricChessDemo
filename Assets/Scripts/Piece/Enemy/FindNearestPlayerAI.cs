using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNearestPlayerAI : EnemyFSM
{

    public override void Init(Enemy enemy)
    {
        base.Init(enemy);
        EnemyIdleState idle = new EnemyIdleState(EnemyStateID.Idle, this);
        EnemyFindState find = new EnemyFindState(EnemyStateID.Find, this);
        EnemyMoveState move = new EnemyMoveState(EnemyStateID.Move, this);
        EnemyAttackState attack = new EnemyAttackState(EnemyStateID.Attack, this);

        AddEnemyFSMState(idle);
        AddEnemyFSMState(find);
        AddEnemyFSMState(move);
        AddEnemyFSMState(attack);
    }

    public override bool FindTarget()
    {
        ResetEnemyFSM();

        baseActionData = Enemy.pieceStatus.Weapon.ActiveSkill.BaseActionData;

        BaseFindMoveRangeFunc(4);

        getPlayerTargetFunc();


        if (targetPieces.Count==0)
        {
            if (Enemy.pieceStatus.CanMove&&Enemy.pieceStatus.HMove>0)
            {
                Player target = PieceQueueManager.Instance.GetTheNearestPiece(PieceQueueManager.Instance.PlayerList, Enemy);
                targetCell = RangeManager.Instance.GetTheNearestCell(target.currentCell);
                PathManager.Instance.TheShorestPathForEnemy(this, Enemy.currentCell, targetCell, Enemy);
                return true;
            }
            else
            {
                Debug.Log("OK5");
                return false;
            }
                
            
        }

        BaseFindDamageTargetPieceFunc();

        return true;
    }

     
}
