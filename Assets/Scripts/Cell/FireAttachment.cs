using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttachment : CellAttachment
{
    public override void MoveOnEffect(Piece piece)
    {
        if (piece == null)
            return;
        int damage;
        if (piece.pieceStatus.CurrentHealth <= piece.pieceStatus.maxHealth / 16)
            damage = piece.pieceStatus.CurrentHealth - 1;
        else
            damage = piece.pieceStatus.maxHealth / 16;
        piece.PieceHealthChange(-damage);
    }

    public override void RemoveEffect(Piece piece)
    {
        if (cell.topCellAttachment != this)
            return;
        base.RemoveEffect(piece);
        cell.topCellAttachment = null;
        if(cell.lowCellAttachment!=null&&cell.lowCellAttachment.propertyType==AttachmentPropertyType.Grass)
        {
           cell.lowCellAttachment.RemoveImmediate();
            CellAttachmentManager.Instance.AddCellAttachment(cell, "Embers");
        }
    }

    public override void TurnStartEffect(Piece piece)
    {
        if (piece == null)
            return;
        int damage;
        if (piece.pieceStatus.CurrentHealth <= piece.pieceStatus.maxHealth / 8)
            damage = piece.pieceStatus.CurrentHealth - 1;
        else
            damage = piece.pieceStatus.maxHealth / 8;
        piece.PieceHealthChange(-damage);
    }
}
