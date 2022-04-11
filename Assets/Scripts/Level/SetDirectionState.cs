using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDirectionState : LevelState
{
    public SetDirectionState(LevelStateID levelStateID) : base(levelStateID)
    {

    }

    public override void Act()
    {
        base.Act();
    }

    public override void Change()
    {
        base.Change();
    }

    public override void Enter()
    {
        Select.Instance.gameObject.SetActive(false);
        RangeManager.Instance.OpenPieceDirectionSet();
    }

}
