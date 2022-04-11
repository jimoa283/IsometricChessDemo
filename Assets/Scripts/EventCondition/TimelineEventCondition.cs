using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

public class TimelineEventCondition : MonoBehaviour
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

   /* protected virtual bool StartThisTimeLine(UnityAction action)
    {
        if (TriggerCondition())
        {
            if (playableDirector == null)
                playableDirector = GetComponent<PlayableDirector>();
            TimeLineManager.Instance.SetTimeLine(playableDirector, action);
            return true;
        }

        return false;
    }*/

    protected virtual bool TriggerCondition()
    {
        return false;
    }
}
