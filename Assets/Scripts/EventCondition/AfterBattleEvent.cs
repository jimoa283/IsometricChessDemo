using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterBattleEvent : EventCondition
{
    protected override void Init()
    {
        base.Init();
        BattleManager.Instance.AfterBattleSpEvent.Add(CheckEvent);
    }
}
