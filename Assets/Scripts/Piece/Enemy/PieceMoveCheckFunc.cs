using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MoveType
{
    PlayerSimple,
    EnemySimple,
    PlayerThrogh
}

public class PieceMoveCheckFunc : MonoBehaviour
{
    public static Func<Cell, Piece, bool> GetMoveCheckFunc(MoveType moveType)
    {
        switch (moveType)
        {
            case MoveType.PlayerSimple:
                return SimplePlayerMoveCellCheck;
            case MoveType.EnemySimple:
                return SimplePlayerMoveCellCheck;
            case MoveType.PlayerThrogh:
                return SimplePlayerMoveCellCheckForThrough;
            default:
                return null;
        }
    }

    

    public static bool SimplePlayerMoveCellCheck(Cell checkCell, Piece movePiece)
    {
        if (CampCheckFunc.CampAgainstCheck(movePiece, checkCell.currentPiece))
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    public static bool SimplePlayerMoveCellCheckForThrough(Cell checkCell, Piece movePiece)
    {
        return true;
    }
}
