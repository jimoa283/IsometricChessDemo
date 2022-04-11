using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeforeTurnStartEvent : EventCondition
{
    protected override void Init()
    {
        base.Init();
        LevelManager.Instance.AddSpecialEventBeforeTurnStart(CheckEvent);
    }

}
