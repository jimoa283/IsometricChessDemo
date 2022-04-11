using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EnemyStateID
{
    Idle,
    Find,
    Move,
    Attack,
    Treat,
}

public class EnemyFSM : MonoBehaviour
{
    public Cell targetCell;
    public Piece bestTargetPiece;
    public Cell bestActionCell;
    protected Enemy enemy;
    public List<Piece> targetPieces;

    protected List<EnemyState> enemyFSMStates;

    protected EnemyStateID currentStateID;

    protected EnemyState currentState;

    public BaseActionData baseActionData;

    public List<Piece> enemyInMoveList;

    public List<Piece> playerInMoveList;

    public List<Cell> boundaryList;

    public EnemyStateID CurrentStateID { get => currentStateID; }
    public EnemyState CurrentState { get => currentState; }
    public Enemy Enemy { get => enemy; }

    public virtual void Init(Enemy enemy)
    {
        this.enemy = enemy;
        targetPieces = new List<Piece>();
        enemyFSMStates = new List<EnemyState>();
        enemyInMoveList = new List<Piece>();
        playerInMoveList = new List<Piece>();
        boundaryList = new List<Cell>();
    }

    public void AddEnemyFSMState(EnemyState enemyState)
    {
        if(enemyFSMStates.Count==0)
        {
            enemyFSMStates.Add(enemyState);
            currentState = enemyState;
            currentStateID = enemyState.EnemyStateID;
            return;
        }

        foreach(var state in enemyFSMStates)
        {
            if(state.EnemyStateID==enemyState.EnemyStateID)
            {
                return;
            }
        }

        enemyFSMStates.Add(enemyState);
    }

    public void PerformTransition(EnemyStateID id)
    {
        currentStateID = id;

        foreach(var state in enemyFSMStates)
        {
            if(state.EnemyStateID==currentStateID)
            {
                currentState = state;
                break;
            }
        }

        currentState.Enter();
    }

    protected void ResetEnemyFSM()
    {
        targetCell = null;
        bestActionCell = null;
        targetPieces.Clear();
        playerInMoveList.Clear();
        boundaryList.Clear();
    }

    public virtual bool FindTarget()
    {
        return false;
    }

    protected virtual List<Cell> GetNeighbourMoveCells(Cell center)
    {
        return CellManager.Instance.GetNeighbourCellsForPiece(Enemy, center);
    }

    protected void BaseFindMoveRangeFunc(int directionNum)
    {
        if (!Enemy.pieceStatus.CanMove || Enemy.pieceStatus.HMove == 0)
                return;

        Queue<Cell> queue = new Queue<Cell>();
        queue.Enqueue(Enemy.currentCell);
        Enemy.currentCell.hCount = 0;
        Enemy.currentCell.vCount = 0;
        Enemy.currentCell.canMove = false;
        RangeManager.Instance.RangeList.Add(Enemy.currentCell);

        while (queue.Count > 0)
        {
            Cell now = queue.Dequeue();
            if (now.hCount < Enemy.pieceStatus.HMove)
            {
                int temp = 0;
                List<Cell> neighbours = GetNeighbourMoveCells(now);
                foreach (var cell in neighbours)
                {
                    if (cell.canMove)
                    {
                        temp++;
                        continue;
                    }
                    if (cell.currentPiece != null)
                    {
                        if (!enemy.moveCheckFunc(cell, enemy))
                        {
                            if (!playerInMoveList.Contains(cell.currentPiece))
                                playerInMoveList.Add(cell.currentPiece);
                            continue;
                        }
                        else
                        {
                            if (now.hCount + 1 == Enemy.pieceStatus.HMove)
                                continue;
                            if (!enemyInMoveList.Contains(cell.currentPiece))
                                enemyInMoveList.Add(cell.currentPiece);
                        }
                    }
                    cell.canMove = true;
                    cell.preCell = now;
                    cell.hCount = now.hCount + 1;
                    RangeManager.Instance.RangeList.Add(cell);
                    queue.Enqueue(cell);
                    temp++;
                }
                if (temp < directionNum || (neighbours.Count > directionNum && temp < neighbours.Count))
                {
                    if (!boundaryList.Contains(now))
                        boundaryList.Add(now);
                }
            }
            else
            {
                if (!boundaryList.Contains(now))
                    boundaryList.Add(now);
            }
        }

    }



    protected void BaseFindTargetFunc()
    {
        foreach (var cell in boundaryList)
        {
            List<Piece> targets = baseActionData.GetTargetPiecesFunc(baseActionData, Enemy, cell, baseActionData.CheckTargetFunc);

            foreach (var piece in targets)
            {
                if (!targetPieces.Contains(piece))
                    targetPieces.Add(piece);
            }
        }
    }

    protected void getPlayerTargetFunc()
    {
        if(Enemy.pieceStatus.CanMove && Enemy.pieceStatus.HMove >= 1)
        {
            BaseFindTargetFunc();
            foreach (var player in playerInMoveList)
            {
                List<Cell> cells = baseActionData.GetTargetCellsFunc(baseActionData, Enemy, player.currentCell, false);
                foreach (var cell in cells)
                {
                    if (cell.canMove)
                    {
                        targetPieces.Add(player);
                        break;
                    }
                }
            }
        }
       else
        {
            targetPieces = baseActionData.GetTargetPiecesFunc(baseActionData, Enemy, Enemy.currentCell, baseActionData.CheckTargetFunc);
        }
    }

    protected void getEnemyTargetFunc()
    {
        if(Enemy.pieceStatus.CanMove && Enemy.pieceStatus.HMove >= 1)
        {
            BaseFindTargetFunc();
            foreach (var enemy in enemyInMoveList)
            {
                List<Cell> cells = baseActionData.GetTargetCellsFunc(baseActionData, Enemy, enemy.currentCell, false);
                foreach (var cell in cells)
                {
                    if (cell.canMove)
                    {
                        targetPieces.Add(enemy);
                        break;
                    }
                }
            }
        }
       else
        {
            targetPieces = baseActionData.GetTargetPiecesFunc(baseActionData, Enemy, Enemy.currentCell, baseActionData.CheckTargetFunc);
        }
       
    }

    protected void BaseFindDamageTargetPieceFunc()
    {
        int topThreat = -1;

        List<Cell> checkList = new List<Cell>();
        LookDirection oriLookDirection = Enemy.pieceStatus.lookDirection;

        foreach (var target in targetPieces)
        {
            List<Cell> actionPosList = baseActionData.GetTargetCellsFunc(baseActionData, Enemy, enemy.currentCell, false);
            actionPosList.Add(target.currentCell);
            foreach (var actionPos in actionPosList)
            {

                if (checkList.Contains(actionPos))
                    continue;

                checkList.Add(actionPos);



                Enemy.pieceStatus.lookDirection = SetBattlPieceFunc.AdjustPieceDirection(Enemy.currentCell, actionPos);

                int threat = 0;

                List<Piece> tempTargets = CellManager.Instance.GetEffectPieces(Enemy, actionPos, baseActionData);
                
                foreach(var tempTarget in tempTargets)
                {
                    int damage = DamageCalculationFunc.SimulationDamageCaculationFunc(Enemy, tempTarget, baseActionData, baseActionData.HitCalculationFunc,
                             baseActionData.DamageCalculationFunc, baseActionData.CriticalCalculationFunc, baseActionData.OtherEffectDamageCalculationFunc);

                    threat += damage + target.pieceStatus.baseThreat;

                    if (Enemy.threatPieceList.Contains(target))
                        threat += 30;

                    if (damage >= target.pieceStatus.CurrentHealth)
                        threat += 50;
                }               

                if (threat > topThreat)
                {
                    topThreat = threat;
                    bestActionCell = actionPos;
                }
            }                  
        }

        if(targetCell==null)
        {
            float distance=999;
            foreach (var cell in baseActionData.GetTargetCellsFunc(baseActionData, Enemy, bestActionCell, false))
            {
                if (cell.canMove)
                {
                    float tempDis = PieceQueueManager.Instance.GetDistance(cell, bestActionCell, cell.floorIndex + Enemy.pieceStatus.isFly, bestActionCell.floorIndex);
                    if(tempDis<distance)
                    {
                        targetCell = cell;
                        distance = tempDis;
                    }                    
                }
            }
        }

        PathManager.Instance.TheShorestPathForEnemy(this, Enemy.currentCell, targetCell, Enemy);

        Enemy.pieceStatus.lookDirection = oriLookDirection;
    }
}
