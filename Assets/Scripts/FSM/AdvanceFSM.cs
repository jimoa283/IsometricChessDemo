using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceFSM : FSM
{
    //FSM中的所有状态组成的列表
    protected List<FSMState> fsmStates;
    //当前状态的编号
    //The fsmStates are not changing directly but updated by using transitions
   // private FSMStateID currentStateID;
  //  public FSMStateID CurrentStateID { get { return currentStateID; } }
    //当前状态
    //private FSMState currentState;
    //public FSMState CurrentState { get { return currentState; } }
}
