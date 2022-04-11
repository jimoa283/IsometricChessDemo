using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class CheckBeforeTurnStartSpecialEventState : LevelState
{
    public List<Func<UnityAction,bool>> specialEventList;

    public CheckBeforeTurnStartSpecialEventState(LevelStateID levelStateID) : base(levelStateID)
    {
        specialEventList = new List<Func<UnityAction,bool>>();
    }

    public override void Act()
    {
        base.Act();
    }

    public override void Change()
    {
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.PieceQueueUI));
        LevelManager.Instance.ChangeLevelState(LevelStateID.TurnStart);
    }

    public override void Enter()
    {

        foreach(var spEvent in specialEventList)
        {
            if(spEvent(Change))
            {
                specialEventList.Remove(spEvent);
                return;
            }
        }
        Change();

    }

    public void AddEvent(Func<UnityAction,bool> action)
    {
        specialEventList.Add(action);
    }

    public void ClearEvent()
    {
        specialEventList.Clear();
    }
}
