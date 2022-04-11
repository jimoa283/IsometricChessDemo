using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideState : SelectState
{
    public HideState(SelectStateID selectStateID, PieceActionSelectUI pieceActionSelectUI) : base(selectStateID, pieceActionSelectUI)
    {
    }

    public override void OnEnter()
    {
        Select.Instance.gameObject.SetActive(false);
    }
}
