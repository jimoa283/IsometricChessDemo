using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RangeManager : MSingleton<RangeManager>
{

    private List<Cell> rangeList;

    private List<GameObject> directionRanges;
    private GameObject currentDirection;

    public List<Cell> RangeList { get => rangeList; }

    private void Update()
    {
        if(BattleGameManager.Instance.isBattling)
        {
            if (LevelManager.Instance.CurrentLevelState.levelStateID == LevelStateID.SetPieceDirection)
            {
                if (Input.GetKeyDown(KeyCode.W))
                    SelectDirection(LookDirection.Up, directionRanges[0]);
                if (Input.GetKeyDown(KeyCode.S))
                    SelectDirection(LookDirection.Down, directionRanges[1]);
                if (Input.GetKeyDown(KeyCode.A))
                    SelectDirection(LookDirection.Left, directionRanges[2]);
                if (Input.GetKeyDown(KeyCode.D))
                    SelectDirection(LookDirection.Right, directionRanges[3]);
                if (Input.GetKeyDown(KeyCode.J))
                {
                    ClearDirectionRange();
                    LevelManager.Instance.ChangeLevelState(LevelStateID.TurnEnd);
                }
            }
        }      
    }


    public void OpenPieceDirectionSet()
    {
        directionRanges.Clear();
        CreateDirectionArrow( new Vector3(-0.5f, -0.3f, 0), LookDirection.Up);
        CreateDirectionArrow( new Vector3(0.5f, -0.9f, 0), LookDirection.Down);
        CreateDirectionArrow( new Vector3(-0.5f, -0.9f, 0), LookDirection.Left);
        CreateDirectionArrow( new Vector3(0.5f, -0.3f, 0), LookDirection.Right);
    }

    public void SelectDirection(LookDirection lookDirection,GameObject direction)
    {
        Piece piece = LevelManager.Instance.CurrentPiece;
        piece.SetIdleDirection(lookDirection);
        currentDirection.GetComponent<SpriteRenderer>().color = new Color(0.9f, 1, 0.4f, 0.3f);
        direction.GetComponent<SpriteRenderer>().color= new Color(0.9f, 1, 0.4f, 0.7f);
        currentDirection = direction;
    }

    public void InitSettingPieceRange(Cell[] list)
    {
        rangeList = new List<Cell>(list);
        foreach(var cell in list)
        {
            cell.range = PoolManager.Instance.GetObj("setRange").GetComponent<ShowRange>();
            cell.range.SimpleShow(cell);
        }
    }

    public void ClearSettingPieceRange()
    {
        foreach (var cell in RangeList)
        {
            cell.range.Hide();
            cell.range = null;
        }
        RangeList.Clear();
    }

    private void CreateDirectionArrow(Vector3 pos,LookDirection lookDirection)
    {
        GameObject obj = PoolManager.Instance.GetObj("directionRange");
        obj.transform.SetParent(LevelManager.Instance.CurrentPiece.transform);
        obj.transform.localPosition = pos;
        directionRanges.Add(obj);
        if(lookDirection==LevelManager.Instance.CurrentPiece.pieceStatus.lookDirection)
        {
            currentDirection = obj;
            obj.GetComponent<SpriteRenderer>().color = new Color(0.9f, 1, 0.4f, 0.7f);
        }
        else
        {
            obj.GetComponent<SpriteRenderer>().color = new Color(0.9f, 1, 0.4f, 0.3f);
        }
    }

    private void ClearDirectionRange()
    {
        foreach(var dir in directionRanges)
        {
            PoolManager.Instance.PushObj("directionRange",dir);
        }
    }


    /// <summary>
    /// 显示玩家阵营角色的移动范围
    /// </summary>
    public void ShowPlayerMoveRange()
    {
        Piece piece = LevelManager.Instance.CurrentPiece;
        if (!piece.pieceStatus.CanMove)
            return;
        Queue<Cell> queue = new Queue<Cell>();
        queue.Enqueue(piece.currentCell);
        piece.currentCell.canMove = true;
        piece.currentCell.hCount = 0;
        piece.currentCell.vCount = 0;
        //checkList.Add(piece.currentCell);
        RangeList.Add(piece.currentCell);
        int maxRange = piece.pieceStatus.HMove;
        while (queue.Count > 0)
        {
            Cell now = queue.Dequeue();
            if (now.hCount < maxRange)
            {
                List<Cell> temp = CellManager.Instance.GetNeighbourCellsForPiece(piece, now);
                for (int i = 0; i < temp.Count; i++)
                {
                    if(temp[i].canMove||temp[i]==piece.currentCell)
                    {
                        continue;
                    }
                    if (temp[i].currentPiece!=null)
                    {
                        if (!piece.moveCheckFunc(temp[i], piece))
                        {
                            continue;
                        }
                    }
                    if (temp[i].riskEnemyList.Count == 0)
                        temp[i].range = PoolManager.Instance.GetObj("moveRange").GetComponent<ShowRange>();
                    else
                        temp[i].range = PoolManager.Instance.GetObj("riskMoveRange").GetComponent<ShowRange>();
                    temp[i].preCell = now;
                    temp[i].range.ShowByAnim(temp[i]);
                    temp[i].canMove = true;
                    temp[i].hCount = now.hCount + 1;
                    queue.Enqueue(temp[i]);
                    RangeList.Add(temp[i]);                                    
                }
            }
        }
    }

    public Cell GetTheNearestCell(Cell targetCell)
    {
        int distance=1000;
        Cell nearCell=null;
        Debug.Log(rangeList.Count);
        foreach(var cell in rangeList)
        {
            int temp = Mathf.Abs(cell.rowIndex - targetCell.rowIndex) + Mathf.Abs(cell.lineIndex - targetCell.lineIndex);
            if(temp<distance)
            {
                nearCell = cell;
                distance = temp;
            }
        }      
        return nearCell;
    }

    public void ShowPieceAttackRange(BaseActionData baseActionData, Piece piece, Cell center)
    {
        CloseAttackRange();
        rangeList = baseActionData.GetTargetCellsFunc(baseActionData, piece, center, true);
    }

    /// <summary>
    /// 清除角色移动范围的显示
    /// </summary>
    public void CloseMoveRange()
    {
        foreach(var move in RangeList)
        {
            move.canMove = false;
            if(move.range!=null)
            {
                move.range.Hide();
                move.range = null;
            }                       
        }
        RangeList.Clear();
    }

    public void CloseAttackRange()
    {
        foreach (var cell in RangeList)
        {
            cell.canAttack = false;
            if(cell.range!=null)
            {
                cell.range.Hide();
                cell.range = null;
            }              
        }
        RangeList.Clear();           
    }


    protected override void Init()
    {
        rangeList = new List<Cell>();
        directionRanges = new List<GameObject>();
    }

    public List<Cell> FindRiskCellForEnemy(Enemy enemy)
    {
        List<Cell> list = new List<Cell>();        
        BaseActionData data = enemy.pieceStatus.Weapon.ActiveSkill.BaseActionData;
        if (enemy.pieceStatus.CanMove && enemy.pieceStatus.HMove >= 1)
        {
            List<Cell> checkList = new List<Cell>();
            Queue<Cell> queue = new Queue<Cell>();
            queue.Enqueue(enemy.currentCell);
            enemy.currentCell.hCount = 0;
            enemy.currentCell.vCount = 0;
            enemy.currentCell.canMove = true;
            list.Add(enemy.currentCell);
            List<Cell> farest = new List<Cell>();

            while (queue.Count > 0)
            {
                Cell now = queue.Dequeue();
                if (now.hCount < enemy.pieceStatus.HMove)
                {
                    int temp = 0;
                    List<Cell> neighbours = CellManager.Instance.GetNeighbourCellsForPiece(enemy, now);
                    foreach (var cell in neighbours)
                    {
                        if(cell.canMove)
                        {
                            temp++;
                            continue;
                        }
                        if (cell.currentPiece != null)
                        {
                            if (!enemy.moveCheckFunc(cell,enemy))
                            {
                                cell.canMove = true;
                                cell.riskEnemyList.Add(enemy);
                                list.Add(cell);
                                continue;
                            }
                            else if (now.hCount + 1 == enemy.pieceStatus.HMove)
                            {
                                continue;
                            }
                        }
                        cell.hCount = now.hCount + 1;
                        cell.canMove = true;
                        list.Add(cell);
                        cell.riskEnemyList.Add(enemy);
                        queue.Enqueue(cell);
                        temp++;
                    }
                    if (temp < 4||(neighbours.Count>4&&temp<neighbours.Count))
                    {
                        if (!farest.Contains(now))
                            farest.Add(now);
                    }
                    
                }
                else
                {
                    if (!farest.Contains(now))
                        farest.Add(now);
                }
            }
            
            foreach (var cell in farest)
            {
                List<Cell> tempList = data.GetTargetCellsFunc(data, enemy, cell, false);
                foreach (var _cell in tempList)
                {
                    if (!list.Contains(_cell))
                    {
                        list.Add(_cell);
                        _cell.riskEnemyList.Add(enemy);
                    }
                }
            }
        }

        foreach (var cell in list)
        {
            cell.canMove = false;
        }

        return list;
    }
}
