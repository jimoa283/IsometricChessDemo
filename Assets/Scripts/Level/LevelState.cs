using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelState 
{
    public LevelStateID levelStateID;

    public LevelState(LevelStateID levelStateID)
    {
        this.levelStateID = levelStateID;
    }

    /// <summary>
    /// 阶段开始
    /// </summary>
    public virtual void Enter() 
    {
      //回合开始     
    }

    /// <summary>
    /// 阶段时的行动
    /// </summary>
    public virtual void Act() { }


    /// <summary>
    /// 转阶段
    /// </summary>
    public virtual void Change() { }
}
