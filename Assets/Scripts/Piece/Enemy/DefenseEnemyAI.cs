using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DefenseEnemyAI : EnemyFSM
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

        if (targetPieces.Count == 0)
            return false;

        BaseFindDamageTargetPieceFunc();

        return true;
    }
}
