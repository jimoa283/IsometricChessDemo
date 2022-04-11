using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public enum LevelStateID
{
   CheckSpecEventBeforeTurnStart,
   TurnStart,   
   SetPieceDirection,
   TurnEnd
}

public class LevelManager : Singleton<LevelManager>
{
    /* 一个角色的回合的流程为：
          1、确认当前有无特殊的人物行动
          2、检查该当前所有角色，场景生成物的状态，确认该消除的buff效果或该处理的效果
          3、把选择框移动到当前回合要行动的角色处并且下方的UI人物顺序变化              //前面3个时点玩家无法操控
          4、（此时点可以让玩家可以操控）玩家此时可以点击空的位置使角色移动，或者点击当前玩家使其做出某些行动
          5、角色根据上回合玩家给予的指令行动（玩家无法操控），有移动、战斗。
          6、回合结束。*/

    private Piece currentPiece;
    private LevelState currentLevelState;

    public LevelState CurrentLevelState { get => currentLevelState; }
    public Piece CurrentPiece { get => currentPiece; set => currentPiece = value; }

    private  List<LevelState> levelStateList;

    public LevelManager()
    {
        levelStateList = new List<LevelState>();
        InitForGameStart();
    }

    public void InitForGameStart()
    {
        levelStateList.Add(new CheckBeforeTurnStartSpecialEventState(LevelStateID.CheckSpecEventBeforeTurnStart));
        levelStateList.Add(new TurnStartState(LevelStateID.TurnStart));
        levelStateList.Add(new SetDirectionState(LevelStateID.SetPieceDirection));
        levelStateList.Add(new TurnEndState(LevelStateID.TurnEnd));
    }

    /// <summary>
    /// 用于获得某阶段
    /// </summary>
    /// <param name="levelStateID"></param>
    /// <returns></returns>
    public LevelState GetLevelState(LevelStateID levelStateID)
    {
        foreach(var ls in levelStateList)
        {
            if (ls.levelStateID == levelStateID)
                return ls;
        }
        return null;
    }

    /// <summary>
    /// 用于改变当前的阶段
    /// </summary>
    /// <param name="levelStateID"></param>
    public void ChangeLevelState(LevelStateID levelStateID)
    {
        foreach(var ls in levelStateList)
        {
            if(ls.levelStateID==levelStateID)
            {
                currentLevelState = ls;
            }
        }
        currentLevelState.Enter();
    }

    public void ClearStateList()
    {
        CheckBeforeTurnStartSpecialEventState state1 = GetLevelState(LevelStateID.CheckSpecEventBeforeTurnStart) as CheckBeforeTurnStartSpecialEventState;
        state1.ClearEvent();

        /*CheckAfterBattleSpecialEventState state2 = GetLevelState(LevelStateID.CheckSpecEventAfterBattle) as CheckAfterBattleSpecialEventState;
        state2.ClearEvent();*/
    }

    public void AddSpecialEventBeforeTurnStart(Func<UnityAction,bool> action)
    {
        CheckBeforeTurnStartSpecialEventState state = GetLevelState(LevelStateID.CheckSpecEventBeforeTurnStart) as CheckBeforeTurnStartSpecialEventState;
        state.AddEvent(action);
    }
}
