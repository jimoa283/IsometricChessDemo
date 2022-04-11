using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class CheckAfterBattleSpecialEventState : LevelState
{
    public List<Func<UnityAction, bool>> specialEventList;

    public CheckAfterBattleSpecialEventState(LevelStateID levelStateID) : base(levelStateID)
    {
        specialEventList = new List<Func<UnityAction, bool>>();
    }

    public override void Act()
    {
        base.Act();
    }

    public override void Change()
    {
        LevelManager.Instance.ChangeLevelState(LevelStateID.SetPieceDirection);
    }

    public override void Enter()
    {

        foreach (var spEvent in specialEventList)
        {
            if (spEvent(Change))
            {
                specialEventList.Remove(spEvent);
                return;
            }
        }
        Change();
    }

    public void AddEvent(Func<UnityAction, bool> action)
    {
        specialEventList.Add(action);
    }

    public void ClearEvent()
    {
        specialEventList.Clear();
    }
}
