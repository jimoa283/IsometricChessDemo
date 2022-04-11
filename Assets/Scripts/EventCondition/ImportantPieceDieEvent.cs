using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantPieceDieEvent : AfterBattleEvent
{
    public string importantPieceName;

    protected override bool TriggerCondition()
    {
        foreach(var piece in BattleManager.Instance.dyingPieceList)
        {
            if(piece.pieceStatus.pieceName==importantPieceName)
            {
                return true;
            }
        }
        return false;
    }
}
