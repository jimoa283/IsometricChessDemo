using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckTargetFunc 
{
    public static Func<Cell,Piece, bool> GetCheckFunc(string targetType)
    {
        switch (targetType)
        {
            case "BenefitFriend":
                return BenefitFriendTarget;
            case "BenefitSelf":
                return BenefitSelfTarget;
            case "AgainstEnemy":
                return AgainstEnemyTarget;
            case "SetBlock":
                return SetBlockTarget;
            default:
                return null;
        }
    }

    public static bool TargetAllPiece(Cell cell,Piece piece)
    {
        return true;
    }

    public static bool BenefitFriendTarget(Cell cell,Piece piece)
    {
        //if (cell.canAttack)
        //{
            if (cell.currentPiece == null || (cell.currentPiece != null && cell.currentPiece.CompareTag(piece.tag)))
                    return true;
       // }

        return false;
    }

    public static bool BenefitSelfTarget(Cell cell,Piece piece)
    {
     
            if (cell.currentPiece == null || (cell.currentPiece != null && cell.currentPiece == piece))
                return true;
 

        return false;
    }

    public static bool AgainstEnemyTarget(Cell cell,Piece piece)
    {

            if (cell.currentPiece == null || (cell.currentPiece != null && !cell.currentPiece.CompareTag(piece.tag)))
                return true;


        return false;
    }

    public static bool SetBlockTarget(Cell cell,Piece piece)
    {
            if (cell.currentPiece == null)
                return true;
        return false;
    }
}
