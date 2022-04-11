using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Block : CellAttachment
{
    public override void AddEffect(Piece piece)
    {
        cell.cantMove = true;
    }

    public override void RemoveEffect(Piece piece)
    {
        if (cell.topCellAttachment != this)
            return;
        cell.cantMove = false;
        cell.topCellAttachment = null;
    }
}
