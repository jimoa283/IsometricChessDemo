using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

public class EventCondition : MonoBehaviour
{
    private PlayableDirector playableDirector;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    protected virtual bool CheckEvent(UnityAction action=null)
    {
       if(TriggerCondition())
        {
            DoEvent(action);
            return true;
        }

        return false;
    }

    public virtual void DoEvent(UnityAction action=null)
    {
        TimeLineManager.Instance.SetTimeLine(playableDirector, action);
    }

    protected virtual bool TriggerCondition()
    {
        return false;
    }

    public virtual void FinishEvent()
    {
        TimeLineManager.Instance.FinishTimeLineAction();
    }
}
