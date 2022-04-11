using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;
using UnityEngine.Timeline;

public class TimeLineManager : Singleton<TimeLineManager>
{
    private PlayableDirector currentDirector;
    private UnityAction action;

    public void SetTimeLine(PlayableDirector playableDirector,UnityAction action)
    {
        currentDirector = playableDirector;
        this.action = action;
        currentDirector.Play();
    }

    public void PauseTimeLine()
    {
        currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(0d);
    }

    public void ResumeTimeLine()
    {
        currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(1d);
    }

    public void FinishTimeLineAction()
    {
        action?.Invoke();
    }
}
