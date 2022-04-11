using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeforeBattleEvent : EventCondition
{
    protected override void Init()
    {
        base.Init();
        BattleManager.Instance.BeforeBattleSpEvent.Add(CheckEvent);
    }

    
}
