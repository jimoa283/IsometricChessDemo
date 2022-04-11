using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SetBattlPieceFunc 
{
    public static UnityAction<Piece,List<Piece>,BaseActionData> GetSetBattlePieceFunc(string Name)
    {
        switch (Name)
        {
            case "Simple":
                return FindExtraAttackPieceSimple;
            case "MoveTarget":
                return FindExtraAttackPieceForTargetMove;
            case "Null":
                return null;
            default:
                return null;
        }
    }

     public static void FindExtraAttackPieceSimple(Piece owner, List<Piece> targetPieces, BaseActionData baseActionData = null)
    {
        if (BattleManager.Instance.extraAttackCount == 0)
            return;

        if (owner.CompareTag("Player"))
            FindExtraAttackPieceSimpleByT(owner, PieceQueueManager.Instance.PlayerList, targetPieces);
        else
            FindExtraAttackPieceSimpleByT(owner, PieceQueueManager.Instance.EnemyList, targetPieces);
    }

    /*private static void FindExtraAttackPiece(Piece owner, List<Piece> targetList, List<Cell> celllist)
    {
        foreach(var target in target)
    }*/

    public static void FindExtraAttackPieceSimpleByT<T>(Piece owner,List<T> pieces ,List<Piece> targetPieces) where T:Piece
    {
        foreach (var target in targetPieces)
        {
            foreach (var piece in pieces)
            {
                if (piece == owner)
                    continue;

                BaseActionData temp = piece.pieceStatus.Weapon.ActiveSkill.BaseActionData;

                /*if (temp.GetTargetCellsFunc(temp, piece, piece.currentCell, false).Contains(target.currentCell)) 
                {
                    BattleManager.Instance.ShowExtraAttackPiece(piece, target, target.currentCell);
                    return;
                }*/
                List<Cell> tempList = temp.GetTargetCellsFunc(temp, piece, piece.currentCell, false);
                foreach (var cell in tempList)
                {
                    if(CellManager.Instance.GetEffectCells(piece,cell,temp).Contains(target.currentCell))
                    {
                        BattleManager.Instance.ShowExtraAttackPiece(piece, target, target.currentCell);
                        return;
                    }
                }
            }
        }
    }

    public static void FindExtraAttackPieceForTargetMove(Piece owner,List<Piece> targetPieces,BaseActionData baseActionData)
    {
        if (BattleManager.Instance.extraAttackCount == 0)
            return;

        List<Cell> tempCells = new List<Cell>();
        List<Piece> tempPieces = new List<Piece>();

        foreach (var piece in targetPieces)
        {
            Cell target;
            int tempNum;
            Piece hit = CellManager.Instance.GetMoveCell(piece.currentCell, piece, baseActionData.MaxMoveNum,
                owner.floorIndex - 1, out target, out tempNum, AdjustPieceDirection(owner.currentCell,piece.currentCell));

            if (hit != null)
            {
                BattleManager.Instance.AddExtraBattlePiece(hit);
                tempCells.Add(target);
                tempPieces.Add(piece);
            }
        }

        if (owner.CompareTag("Player"))
        {
            FindExtraAttackPieceForTargetMoveByT(owner, tempPieces, tempCells, PieceQueueManager.Instance.PlayerList);
        }
        else
            FindExtraAttackPieceForTargetMoveByT(owner, tempPieces, tempCells, PieceQueueManager.Instance.EnemyList);
    }

    

    private static void  FindExtraAttackPieceForTargetMoveByT<T>(Piece owner,List<Piece> targetList,List<Cell> cellList,List<T> pieces) where T:Piece
    {
        for (int i = 0; i < cellList.Count; i++)
        {
            foreach (var piece in pieces)
            {
                if (piece == owner)
                    continue;

                BaseActionData temp = piece.pieceStatus.Weapon.ActiveSkill.BaseActionData;

                if (temp.GetTargetCellsFunc(temp,piece,piece.currentCell,false).Contains(cellList[i]))
                {
                    BattleManager.Instance.ShowExtraAttackPiece(piece, targetList[i], cellList[i]);
                    return;
                }
            }
        }
    }

    public static LookDirection AdjustPieceDirection(Cell cell1, Cell cell2)
    {
        int rowIndex = cell2.rowIndex - cell1.rowIndex;
        int lineIndex = cell2.lineIndex - cell1.lineIndex;

        if (rowIndex >= lineIndex && rowIndex > -lineIndex)
            return LookDirection.Right;
        else if (lineIndex > rowIndex && lineIndex >= -rowIndex)
            return LookDirection.Up;
        else if (lineIndex < -rowIndex && lineIndex >= rowIndex)
            return LookDirection.Left;
        else
            return LookDirection.Down;
    }

    
}
