using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantMoveBUFFEffect : AMPassiveEffect
{
    public override void AddEffect(Piece piece)
    {
        piece.pieceStatus.CanMove = false;
    }

    public override void RemoveEffect(Piece piece)
    {
        piece.pieceStatus.CanMove = true;
    }
}
