using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FindTargetCellAEnemyAI : EnemyFSM
{
    public bool canAction;
    public int baseTargetCellR;
    public int baseTargetCellL;
    public Cell baseTargetCell;

    public override bool FindTarget()
    {
        ResetEnemyFSM();
        baseActionData = Enemy.pieceStatus.Weapon.ActiveSkill.BaseActionData;

        BaseFindMoveRangeFunc(2);

        getPlayerTargetFunc();

        if (targetPieces.Count == 0||!canAction||RangeManager.Instance.RangeList.Contains(baseTargetCell))
        {
            targetCell = RangeManager.Instance.GetTheNearestCell(baseTargetCell);
            PathManager.Instance.TheShorestPathForEnemy(this, Enemy.currentCell, targetCell, Enemy);
            return true;
        }

        BaseFindDamageTargetPieceFunc();

        return true;
    }

    public override void Init(Enemy enemy)
    {
        base.Init(enemy);
        baseTargetCell = CellManager.Instance.GetCellByRL(baseTargetCellR, baseTargetCellL);
        EnemyIdleState idle = new EnemyIdleState(EnemyStateID.Idle, this);
        EnemyFindState find = new EnemyFindState(EnemyStateID.Find, this);
        EnemyMoveState move = new EnemyMoveState(EnemyStateID.Move, this);
        EnemyAttackState attack = new EnemyAttackState(EnemyStateID.Attack, this);

        AddEnemyFSMState(idle);
        AddEnemyFSMState(find);
        AddEnemyFSMState(move);
        AddEnemyFSMState(attack);
    }

    protected override List<Cell> GetNeighbourMoveCells(Cell center)
    {
        return CellManager.Instance.GetTargetCellDirectionCells(Enemy,center, baseTargetCell);
    }
}
