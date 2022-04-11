using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceAnimController : MonoBehaviour
{
    private Animator anim;
    private PieceStatus pieceStatus;
    private PieceCellPos pieceCellPos;

    public void Init()
    {
        anim = GetComponent<Animator>();
        pieceStatus = GetComponent<PieceStatus>();
        pieceCellPos = GetComponent<PieceCellPos>();
    }

    /// <summary>
    /// 人物的运动动画的转变
    /// </summary>
    /// <param name="target"></param>
    private void RunDirectorChange(Cell target)
    {
        Cell cell = pieceCellPos.currentCell;
        if (target.rowIndex == cell.rowIndex)
        {
            if (target.lineIndex > cell.lineIndex)
            {
                anim.Play("Run_Up");
                pieceStatus.lookDirection = LookDirection.Up;
            }

            else
            {
                anim.Play("Run_Down");
                pieceStatus.lookDirection = LookDirection.Down;
            }

        }
        else
        {
            if (target.rowIndex > cell.rowIndex)
            {
                anim.Play("Run_Right");
                pieceStatus.lookDirection = LookDirection.Right;
            }

            else
            {
                anim.Play("Run_Left");
                pieceStatus.lookDirection = LookDirection.Left;
            }
        }
    }
}
