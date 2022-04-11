using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathManager :Singleton<PathManager>
{
    private List<Cell> pathList;
    private Cell startCell;
    private Cell endCell;

    public PathManager()
    {
       pathList = new List<Cell>();
    }

    /*public void FindPathForEnemy(Cell startPoint,Cell endPoint,Piece piece, Func<Cell, Cell, int, bool> func,int range)
    {
        Queue<Cell> queue = new Queue<Cell>();
        queue.Enqueue(startPoint);
        startPoint.hasPathChecked = true;
        while(queue.Count>0)
        {
            Cell now = queue.Dequeue();
            *//*if (now == endPoint)
                return;*//*
            if (func(now, endPoint, range))
            {
                endCell = now;
                return;
            }

            List<Cell> neighbours = CellManager.Instance.GetCellsForPath(now,piece);         
            foreach(var neighbour in neighbours)
            {
                queue.Enqueue(neighbour);
                neighbour.preCell = now;
                neighbour.hasPathChecked = true;
            }
        }
    }

    /// <summary>
    /// 为当前棋子寻找路径
    /// </summary>
    /// <param name="startPoint">起点格子</param>
    /// <param name="endPoint">终点格子</param>
    public void FindPathForPlayer(Cell startPoint,Cell endPoint,Piece piece,Func<Cell,Cell,int,bool> func)
    {       
        if (startPoint == endPoint||!endPoint.canMove)
            return;
        Queue<Cell> queue = new Queue<Cell>();
        queue.Enqueue(startPoint);
        startPoint.hasPathChecked = true;
        while(queue.Count>0)
        {
            Cell now = queue.Dequeue();
            *//*if (now == endPoint)
                return;*//*
            if (func(now, endPoint, 0))
                return;
            List<Cell> neighbours = CellManager.Instance.GetCellsForPath(now,piece);
            foreach(var neighbour in neighbours)
            {
                queue.Enqueue(neighbour);
                neighbour.preCell = now;
                neighbour.hasPathChecked = true;
            }
        }
    }*/

    /// <summary>
    /// 生成路径
    /// </summary>
    /// <param name="startPoint">起点</param>
    /// <param name="endPoint">终点</param>
    private void CreatePath(Cell startPoint,Cell endPoint)
    {
        if (endPoint == startPoint||endPoint==null||!endPoint.canMove)
            return;
        
        pathList.Add(endPoint);
        Cell prepoint = endPoint.preCell;
        prepoint.nextCell = endPoint;
        while(prepoint!=startPoint)
        {
            pathList.Add(prepoint);
            prepoint.preCell.nextCell = prepoint;
            prepoint = prepoint.preCell;
        }
        pathList.Add(startPoint);
        pathList.Reverse();
    }

    /// <summary>
    /// 对外的接口，把各部分集合，生成并显示路径
    /// </summary>
    /// <param name="startPoint">起点</param>
    /// <param name="endPoint">终点</param>
    public void TheShortestPath(Cell startPoint,Cell endPoint,Piece piece)
    {    
        ClearPath();
        if (!endPoint.canMove||endPoint==piece.currentCell)
            return;
        startCell = startPoint;
        endCell = endPoint;
        CreatePath(startPoint, endPoint);
        DisplayPath();
    }

    public void TheShorestPathForEnemy(EnemyFSM enemyFSM,Cell startPoint,Cell endPoint,Piece piece)
    {
        ClearPath();
        startCell = startPoint;
        endCell = endPoint;       
        CreatePath(startCell, endCell);
        enemyFSM.targetCell = endCell;
    }

    /// <summary>
    /// 清除之前的路径和复原到未检测前
    /// </summary>
    public void ClearPath()
    {
        if(pathList.Count>0)
        {
            foreach (var path in pathList)
            {
                if(path.pathObj!=null)
                {
                    path.pathObj.Hide();
                    path.pathObj = null;
                }
            }
        }
        pathList.Clear();
    }

   

    /// <summary>
    /// 用于生成路径的显示
    /// </summary>
    private void DisplayPath()
    {
        if(pathList.Count>0)
        {
            foreach (var path in pathList)
            {
                path.pathObj = PoolManager.Instance.GetObj("pathObj").GetComponent<ShowRange>();
                path.pathObj.SimpleShow(path);
            }
        }
    }

    public bool MoveExactPos(Cell nowPoint,Cell endPoint,int range)
    {
        if (nowPoint == endPoint)
            return true;

        return false;
    }

    public bool MoveSimplePosForEnemy(Cell nowPoint,Cell endPoint,int range)
    {
        int distance = Mathf.Abs(nowPoint.rowIndex - endPoint.rowIndex) + Mathf.Abs(nowPoint.lineIndex - endPoint.lineIndex);

        if (distance <= range)
            return true;

        return false;
    }
}
