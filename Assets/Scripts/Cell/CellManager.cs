using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CellManager : Singleton<CellManager>
{
    private Dictionary<Vector2, Cell> cellDic;
    private List<CellAttachment> cellAttachmentList;

    public List<CellAttachment> CellAttachmentList { get => cellAttachmentList; }

    public CellManager()
    {
        cellDic = new Dictionary<Vector2, Cell>();
        cellAttachmentList = new List<CellAttachment>();
    }

    public void ClearCellDic()
    {
        cellDic.Clear();
        cellAttachmentList.Clear();
    }

    /// <summary>
    /// 用于登记格子
    /// </summary>
    /// <param name="rl">格子的行列</param>
    /// <param name="cell">格子本身</param>
    public void AddCell(Vector2 rl, Cell cell)
    {
        Debug.Log("Add");
        if (!cellDic.ContainsKey(rl))
        {
            cellDic.Add(rl, cell);
        }
        else
        {
            if (cell.floorIndex > cellDic[rl].floorIndex)
                  cellDic[rl] = cell;
        }
    }

    public void CellAttachmentCountDown()
    {
        foreach(var cellattachment in cellAttachmentList)
        {
            cellattachment.TimeCountDown();
        }
    }

    /// <summary>
    /// 给选择框
    /// </summary>
    /// <param name="row"></param>
    /// <param name="line"></param>
    /// <returns></returns>
    public Cell GetCellByRL(int row,int line)
    {
        Vector2 rl = new Vector2(row, line);
        if (cellDic.ContainsKey(rl))
            return cellDic[rl];
        else
        {
            return null;
        }
            
    }

    public void GetTransferCell(List<Cell> list,Cell checkCell,Cell formCell)
    {
        if(checkCell!=null)
        {
            checkCell.vCount = formCell.vCount + Mathf.Clamp(checkCell.floorIndex - formCell.floorIndex, -1, 1);
            list.Add(checkCell);
        }
    }

    public List<Cell> GetNeighbourCellsForPiece(Piece piece,Cell cell)
    {
        List<Cell> temp = new List<Cell>();
        GetTransferCell(temp,cell.upHTransferCell,cell);
        GetTransferCell(temp,cell.downHTransferCell, cell);
        GetTransferCell(temp,cell.leftHTransferCell, cell);
        GetTransferCell(temp,cell.rightHTransferCell, cell);
        GetNeighbourCellsForPieceOne(piece,GetCellByRL(cell.rowIndex+1,cell.lineIndex), temp,cell);
        GetNeighbourCellsForPieceOne(piece, GetCellByRL(cell.rowIndex-1, cell.lineIndex), temp,cell);
        GetNeighbourCellsForPieceOne(piece, GetCellByRL(cell.rowIndex, cell.lineIndex+1), temp,cell);
        GetNeighbourCellsForPieceOne(piece, GetCellByRL(cell.rowIndex, cell.lineIndex-1), temp,cell);
        return temp;
    }

    public void GetNeighbourCellsForPieceOne(Piece piece,Cell checkCell,List<Cell> queue,Cell formCell)
    {
        if (checkCell == null||checkCell.cantMove||queue.Contains(checkCell))
            return;
        if (Mathf.Abs(formCell.vCount + checkCell.floorIndex - formCell.floorIndex) <= piece.pieceStatus.VMove)
        {
            checkCell.vCount = (checkCell.floorIndex - formCell.floorIndex) + formCell.vCount;
            queue.Add(checkCell);
        }      
    }

    public List<Cell> GetTargetCellDirectionCells(Piece piece,Cell cell,Cell targetCell)
    {
        List<Cell> temp = new List<Cell>();
        int rowIndex = piece.currentCell.rowIndex - targetCell.rowIndex;
        int lineIndex = piece.currentCell.lineIndex - targetCell.lineIndex;

        if(rowIndex<=0&&lineIndex<0)
        {
            GetNeighbourCellsForPieceOne(piece, GetCellByRL(cell.rowIndex + 1, cell.lineIndex), temp,cell);
            GetNeighbourCellsForPieceOne(piece, GetCellByRL(cell.rowIndex , cell.lineIndex+1), temp,cell);
        }
        else if(rowIndex>0&&lineIndex<=0)
        {
            GetNeighbourCellsForPieceOne(piece, GetCellByRL(cell.rowIndex - 1, cell.lineIndex), temp,cell);
            GetNeighbourCellsForPieceOne(piece, GetCellByRL(cell.rowIndex, cell.lineIndex+1), temp,cell);
        }
        else if(rowIndex>=0&&lineIndex>0)
        {
            GetNeighbourCellsForPieceOne(piece, GetCellByRL(cell.rowIndex - 1, cell.lineIndex), temp,cell);
            GetNeighbourCellsForPieceOne(piece, GetCellByRL(cell.rowIndex, cell.lineIndex-1), temp,cell);
        }
        else if(rowIndex<0&&lineIndex>=0)
        {
            GetNeighbourCellsForPieceOne(piece, GetCellByRL(cell.rowIndex + 1, cell.lineIndex), temp,cell);
            GetNeighbourCellsForPieceOne(piece, GetCellByRL(cell.rowIndex, cell.lineIndex-1), temp,cell);
        }

        return temp;
    }

    public List<Cell> GetAttackRangeByLine(BaseActionData baseActionData,Piece owner ,Cell center,bool isShow)
    {
        List<Cell> temp=new List<Cell>();
        int hMinRange= baseActionData.HMinRange;
        int hMaxRange = owner.PieceTimeProcessor.ShowActionTimeProcessorCheck(baseActionData, baseActionData.HMaxRange);
        int vMinRange = baseActionData.VMinRange;
        int vMaxRange = baseActionData.VMaxRange;
        for (int i =hMaxRange; i >=hMinRange ; i--)
        {
           Cell cell1= GetAttackCell(center.rowIndex+i, center.lineIndex,vMinRange,vMaxRange,owner,isShow);
            if (cell1 != null)
                temp.Add(cell1);

           Cell cell2= GetAttackCell(center.rowIndex - i,center.lineIndex, vMinRange, vMaxRange, owner,isShow);
            if (cell2 != null)
                temp.Add(cell2);

           Cell cell3=GetAttackCell(center.rowIndex, center.lineIndex + i, vMinRange, vMaxRange, owner,isShow);
            if (cell3 != null)
                temp.Add(cell3);

           Cell cell4=GetAttackCell(center.rowIndex, center.lineIndex - i, vMinRange, vMaxRange, owner,isShow);
            if (cell4 != null)
                temp.Add(cell4);
        }
        return temp;
    }

    public List<Cell> GetAttackRangeByCircle(BaseActionData baseActionData,Piece owner,Cell center,bool isShow)
    {
        List<Cell> temp = new List<Cell>();
        int hMinRange = baseActionData.HMinRange;
        int vMinRange = baseActionData.VMinRange;
        int vMaxRange = baseActionData.VMaxRange;
        int hMaxRange = owner.PieceTimeProcessor.ShowActionTimeProcessorCheck(baseActionData, baseActionData.HMaxRange);
        for (int i = -hMaxRange; i <=0; i++)
        {
            for (int j = -(hMaxRange+i); j <=hMaxRange+i; j++)
            {
                if (Mathf.Abs(i) + Mathf.Abs(j) <= hMinRange - 1)
                    continue;
               Cell cell= GetAttackCell(center.rowIndex + i, center.lineIndex + j, vMinRange, vMaxRange, owner,isShow);
                if (cell != null)
                    temp.Add(cell);
            }
        }

        for (int i = hMaxRange; i >= 1; i--)
        {
            for (int j = -(hMaxRange - i); j <= hMaxRange - i; j++)
            {
                if (Mathf.Abs(i) + Mathf.Abs(j) <= hMinRange - 1)
                    continue;
               Cell cell= GetAttackCell(center.rowIndex + i, center.lineIndex + j, vMinRange, vMaxRange, owner,isShow);
                if (cell != null)
                    temp.Add(cell);
            }
        }

        return temp;
    }

    private Cell GetAttackCell(int rowIndex, int lineIndex,int vMinAttackRange,int vMaxAttackRange ,Piece owner,bool isShow)
    {
        Cell cell = GetCellByRL(rowIndex, lineIndex);
        if (cell == null)
            return null;
        if(vMinAttackRange+owner.floorIndex<=cell.floorIndex&&vMaxAttackRange+owner.floorIndex>=cell.floorIndex)
        {
            if (isShow)
            {
                cell.range = PoolManager.Instance.GetObj("actionRange").GetComponent<ShowRange>();
                cell.range.ShowByAnim(cell);
                cell.canAttack = true;
            }            
            return cell;
        }
        return null;
    }

    public List<Piece> GetEffectPieces(Piece piece,Cell center,BaseActionData baseActionData)
    {
        List<Piece> targets = new List<Piece>();
        List<Vector2> temp;

        if (piece.pieceStatus.lookDirection == LookDirection.Up)
            temp = baseActionData.RangePosUp;
        else if (piece.pieceStatus.lookDirection == LookDirection.Down)
            temp = baseActionData.RangePosDown;
        else if (piece.pieceStatus.lookDirection == LookDirection.Left)
            temp = baseActionData.RangePosLeft;
        else
            temp = baseActionData.RangePosRight;

        foreach (var pos in temp)
        {
            Cell cell = GetEffectCell((int)(center.rowIndex + pos.x), (int)(center.lineIndex + pos.y), baseActionData.VMinRange, baseActionData.VMaxRange, piece);
            if (cell != null && baseActionData.CheckTargetFunc(cell, piece))
            {
                if (baseActionData.CheckTargetFunc != CheckTargetFunc.SetBlockTarget)
                {
                    if (cell.currentPiece != null)
                        targets.Add(cell.currentPiece);
                }
            }
        }

        return targets;
    }

    public List<Cell> GetEffectCells(Piece piece,Cell center,BaseActionData baseActionData)
    {
        List<Cell> targets = new List<Cell>();
        List<Vector2> temp;

        if (piece.pieceStatus.lookDirection == LookDirection.Up)
            temp = baseActionData.RangePosUp;
        else if (piece.pieceStatus.lookDirection == LookDirection.Down)
            temp = baseActionData.RangePosDown;
        else if (piece.pieceStatus.lookDirection == LookDirection.Left)
            temp = baseActionData.RangePosLeft;
        else
            temp = baseActionData.RangePosRight;

        foreach (var pos in temp)
        {
            Cell cell = GetEffectCell((int)(center.rowIndex + pos.x), (int)(center.lineIndex + pos.y), baseActionData.VMinRange, baseActionData.VMaxRange, piece);
            if (cell != null && baseActionData.CheckTargetFunc(cell, piece))
            {
                targets.Add(cell);
            }
        }

        return targets;
    }

    public Cell GetEffectCell(int rowIndex,int lineIndex,int vMinActionRange,int vMaxActionRange,Piece piece)
    {
        Cell cell = GetCellByRL(rowIndex, lineIndex);
        if (cell == null)
            return null;

        if (vMinActionRange + piece.floorIndex<= cell.floorIndex && vMaxActionRange + piece.floorIndex>= cell.floorIndex)
        {
            return cell;
        }
        else
        {
            return null;
        }
    }

    public List<Piece> GetTargetsByLine(BaseActionData baseActionData, Piece owner, Cell center, Func<Cell, Piece, bool> func)
    {
        List<Piece> temp = new List<Piece>();
        int hMinRange = baseActionData.HMinRange;
        int hMaxRange = owner.PieceTimeProcessor.ShowActionTimeProcessorCheck(baseActionData, baseActionData.HMaxRange);
        int vMinRange = baseActionData.VMinRange;
        int vMaxRange = baseActionData.VMaxRange;
        for (int i = hMaxRange; i >= hMinRange; i--)
        {
            Piece target1= GetTarget(center.rowIndex + i, center.lineIndex, vMinRange, vMaxRange, owner, func);
            if (target1 != null)
                temp.Add(target1);

            Piece target2=GetTarget(center.rowIndex - i, center.lineIndex, vMinRange, vMaxRange, owner, func);
            if (target2 != null)
                temp.Add(target2);

            Piece target3=GetTarget(center.rowIndex, center.lineIndex + i, vMinRange, vMaxRange, owner, func);
            if (target3 != null)
                temp.Add(target3);

            Piece target4=GetTarget(center.rowIndex, center.lineIndex - i, vMinRange, vMaxRange, owner, func);
            if (target4 != null)
                temp.Add(target4);
        }
        return temp;
    }


    public List<Piece> GetTargetsByCircle(BaseActionData baseActionData, Piece owner, Cell center,Func<Cell,Piece,bool> func)
    {
        List<Piece> temp = new List<Piece>();
        int hMinRange = baseActionData.HMinRange;
        int hMaxRange = owner.PieceTimeProcessor.ShowActionTimeProcessorCheck(baseActionData, baseActionData.HMaxRange);
        int vMinRange = baseActionData.VMinRange;
        int vMaxRange = baseActionData.VMaxRange;
        for (int i = -hMaxRange; i <= 0; i++)
        {
            for (int j = -(hMaxRange + i); j <= hMaxRange + i; j++)
            {
                if (Mathf.Abs(i) + Mathf.Abs(j) <= hMinRange - 1)
                    continue;
               Piece piece=GetTarget(center.rowIndex + i, center.lineIndex + j, vMinRange, vMaxRange, owner,func);
                if (piece != null)
                    temp.Add(piece);
            }
        }

        for (int i = hMaxRange; i >= 1; i--)
        {
            for (int j = -(hMaxRange - i); j <= hMaxRange - i; j++)
            {
                if (Mathf.Abs(i) + Mathf.Abs(j) <= hMinRange - 1)
                    continue;
                Piece piece = GetTarget(center.rowIndex + i, center.lineIndex + j, vMinRange, vMaxRange, owner,func);
                if (piece != null)
                    temp.Add(piece);
            }
        }

        return temp;
    }

    public Piece GetTarget(int rowIndex, int lineIndex, int vMinAttackRange, int vMaxAttackRange, Piece owner,Func<Cell,Piece,bool> func)
    {
        Cell cell = GetCellByRL(rowIndex, lineIndex);
        if (cell == null||cell.currentPiece==null)
        {
            return null;
        }

        if (vMinAttackRange + owner.floorIndex <= cell.floorIndex && vMaxAttackRange + owner.floorIndex >= cell.floorIndex)
        {
            if (func(cell,owner))
                return cell.currentPiece;
        }

        return null;
    }


    public bool getEffectRangeByCell(BaseActionData baseActionData, Piece owner, Cell center, Cell targetCell)
    {
        int hMinRange = baseActionData.HMinRange;
        int hMaxRange = owner.PieceTimeProcessor.ShowActionTimeProcessorCheck(baseActionData, baseActionData.HMaxRange);
        int vMinRange = baseActionData.VMinRange;
        int vMaxRange = baseActionData.VMaxRange;

        for (int i = -hMaxRange; i <= 0; i++)
        {
            for (int j = -(hMaxRange + i); j <= hMaxRange + i; j++)
            {
                if (Mathf.Abs(i) + Mathf.Abs(j) <= hMinRange - 1)
                    continue;

                Cell cell = GetAttackCell(center.rowIndex + i, center.lineIndex + j, vMinRange, vMaxRange, owner, false);
                if (cell == targetCell)
                    return true;
            }
        }

        for (int i = hMaxRange; i >= 1; i--)
        {
            for (int j = -(hMaxRange - i); j <= hMaxRange - i; j++)
            {
                if (Mathf.Abs(i) + Mathf.Abs(j) <= hMinRange - 1)
                    continue;

                Cell cell = GetAttackCell(center.rowIndex + i, center.lineIndex + j, vMinRange, vMaxRange, owner, false);
                if (cell == targetCell)
                    return true;
            }
        }

        return false;
    }

    public Piece GetMoveCell(Cell start, Piece targetPiece, int maxMoveNum, int floorIndex, out Cell target, out int num,LookDirection lookDirection)
    {
        switch (lookDirection)
        {
            case LookDirection.Up:
                return GetMoveCellForUpOrRight(start, targetPiece, maxMoveNum, floorIndex, out target,out num,lookDirection);
            case LookDirection.Right:
                return GetMoveCellForUpOrRight(start, targetPiece, maxMoveNum, floorIndex, out target, out num,lookDirection);
            case LookDirection.Down:
                return GetMoveCellForDownOrLeft(start, targetPiece, maxMoveNum, floorIndex, out target,out num,lookDirection);
            case LookDirection.Left:
                return GetMoveCellForDownOrLeft(start, targetPiece, maxMoveNum, floorIndex, out target,out num,lookDirection);         
            default:
                target = null;
                num = 0;
                return null;
        }
    }

    public Piece GetMoveCellForUpOrRight(Cell start,Piece targetPiece,int maxMoveNum,int floorIndex,out Cell target, out int num,LookDirection lookDirection)
    {
        Cell temp = start;
        target=temp;
        for (int i = 1; i<=maxMoveNum; i++)
        {
            if(lookDirection==LookDirection.Up)
               temp = GetCellByRL(start.rowIndex, start.lineIndex + i);
            else
               temp= GetCellByRL(start.rowIndex + i, start.lineIndex);
            if (temp!=null)
            {
                if (temp.cantMove || temp.floorIndex != floorIndex)
                {
                    num = i - 1;
                    return null;
                }
                else if (temp.currentPiece != null&&temp.currentPiece!=targetPiece)
                {
                    num = i - 1;
                    return temp.currentPiece;
                }
                else
                {
                    target = temp;
                }
            }
            else
            {
                num = i - 1;
                return null;
            }
        }
        num = maxMoveNum;
        return null;
    }


    public Piece GetMoveCellForDownOrLeft(Cell start, Piece targetPiece, int maxMoveNum, int floorIndex, out Cell target, out int num,LookDirection lookDirection)
    {
        Cell temp = start;
        target = temp;
        for (int i = -1; i >= -maxMoveNum; i--)
        {
            if(lookDirection==LookDirection.Down)
               temp = GetCellByRL(start.rowIndex, start.lineIndex + i);
            else
                temp = GetCellByRL(start.rowIndex + i, start.lineIndex);
            if (temp!=null)
            {
                if (temp.cantMove || temp.floorIndex != floorIndex)
                {
                    num = -i - 1;
                    return null;
                }
                else if (temp.currentPiece != null&&temp.currentPiece!=targetPiece)
                {
                    num = -i - 1;
                    return temp.currentPiece;
                }
                else
                {
                    target = temp;
                }
            }
            else
            {
                num = -i - 1;
                return null;
            }
        }
        num = maxMoveNum;
        return null;
    }

    public List<Piece> GetPieceNeighbour(Piece owner)
    {
        List<Piece> pieces = new List<Piece>();
        Piece temp1 = GetTarget(owner.currentCell.rowIndex, owner.currentCell.lineIndex + 1, -1, 1, owner, CheckTargetFunc.TargetAllPiece);
        if (temp1 != null)
            pieces.Add(temp1);

        Piece temp2= GetTarget(owner.currentCell.rowIndex, owner.currentCell.lineIndex - 1, -1, 1, owner, CheckTargetFunc.TargetAllPiece);
        if (temp2 != null)
            pieces.Add(temp2);

        Piece temp3 = GetTarget(owner.currentCell.rowIndex+1, owner.currentCell.lineIndex , -1, 1, owner, CheckTargetFunc.TargetAllPiece);
        if (temp3 != null)
            pieces.Add(temp3);

        Piece temp4 = GetTarget(owner.currentCell.rowIndex-1, owner.currentCell.lineIndex, -1, 1, owner, CheckTargetFunc.TargetAllPiece);
        if (temp4 != null)
            pieces.Add(temp4);

        return pieces;
    }
}
