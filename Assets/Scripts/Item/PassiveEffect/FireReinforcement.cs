using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireReinforcement : AMPassiveEffect
{
    public override void AddEffect(Piece piece)
    {
        piece.pieceStatus.baseFireResistance += 30;
        piece.pieceStatus.baseIceResistance -= 30;
    }

    public override void RemoveEffect(Piece piece)
    {
        piece.pieceStatus.baseFireResistance -= 30;
        piece.pieceStatus.baseIceResistance += 30;
    }
}
