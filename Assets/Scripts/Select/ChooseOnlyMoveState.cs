using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseOnlyMoveState : SelectState
{
    public ChooseOnlyMoveState(SelectStateID selectStateID, PieceActionSelectUI pieceActionSelectUI) : base(selectStateID, pieceActionSelectUI)
    {

    }

    public override void ActionOnI()
    {
        base.ActionOnI();
    }

    public override void ActionOnJ()
    {
        base.ActionOnJ();
    }

    public override void ActionOnK()
    {
        base.ActionOnK();
    }

    public override void ActionOnL()
    {
        base.ActionOnL();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Select.Instance.gameObject.SetActive(true);
        Select.Instance.WakeUp();
        SetPos(LevelManager.Instance.CurrentPiece.currentCell);
        Debug.Log("Enter");
        RangeManager.Instance.ShowPlayerMoveRange();
    }

    public override void SetPos(Cell cell)
    {
        Select.Instance.SetPos(cell);
        base.SetPos(cell);
    }
}
