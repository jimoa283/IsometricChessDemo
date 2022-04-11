using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerSettingPosState : SelectState
{
    public ChangePlayerSettingPosState(SelectStateID selectStateID, PieceActionSelectUI pieceActionSelectUI) : base(selectStateID, pieceActionSelectUI)
    {
    }

    public override void ActionOnI()
    {
        //Select.Instance.CancelChangePlayerPos();
    }

    public override void ActionOnJ()
    {
       if(pieceActionSelectUI.ConfirmObjActive())
        {
            Select.Instance.ChangePlayerPos();
        }
    }

    public override void ActionOnK()
    {
      if(pieceActionSelectUI.RemoveActive())
        {
            Select.Instance.CancelChangePlayerPos();
        }
    }

    public override void ActionOnL()
    {
      
    }

    public override void OnEnter()
    {
        pieceActionSelectUI.ChangeChangePlayerSettingPosAction();
        SetPos(Select.Instance.currentCell);
    }

    public override void SetPos(Cell cell)
    {
        Select.Instance.SetPosSimple(cell);
        base.SetPos(cell);
    }
}
