using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMState 
{
    protected AdvanceFSM advanceFSM;

    //字典，字典中每一项都记录了一个“转换-状态”对 的信息
    //protected Dictionary<Transition, FSMStateID> map = new Dictionary<Transition, FSMStateID>();
    //状态编号ID
    //protected FSMStateID stateID;
   // public FSMStateID ID { get { return stateID; } }

    /// <summary>
    /// 用来确定是否需要转换到其他状态，应该发生哪个转换
    /// </summary>
    /// <param name="player"></param>
    /// <param name="npc"></param>
    public abstract void Reason();

    /// <summary>
    /// 定义了在本状态的角色行为，移动，动画等
    /// </summary>
    /// <param name="player"></param>
    /// <param name="npc"></param>
    public abstract void Act();
}
