using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeState : SelectState
{
    public FreezeState(SelectStateID selectStateID, PieceActionSelectUI pieceActionSelectUI) : base(selectStateID, pieceActionSelectUI)
    {
    }

    public override void OnEnter()
    {
        Select.Instance.Freeze();
    }
}
