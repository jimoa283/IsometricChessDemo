using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCalculation 
{
    public static int GetExp(Piece owner,Piece target)
    {
        if (target == null)
            return 30;
        else if (target.pieceStatus.CurrentHealth == 0)
            return GetPlayerKillExp(owner, target);
        else
            return GetPlayerAttackExp(owner, target);
    }

    public static int GetExtraAttackExp(Piece owner,Piece target)
    {
        return GetExp(owner, target) / 2;
    }

   private static int GetPlayerAttackExp(Piece owner,Piece target)
    {
        int exp = (30 + (owner.pieceStatus.Level - target.pieceStatus.Level)) / 3;
        exp = Mathf.Clamp(exp, 1, 99);
        return 2*exp;
    }

    private static int GetPlayerKillExp(Piece owner,Piece target)
    {
        int exp = (30 + (owner.pieceStatus.Level - target.pieceStatus.Level) * 3);
        exp = Mathf.Clamp(exp, 1, 99);
        return 2*exp;
    }

    
}
