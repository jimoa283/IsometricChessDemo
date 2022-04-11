using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class LearningWindowBehavior : PlayableBehaviour
{
    private PlayableDirector playableDirector;
    private bool isClipPlayed;
    public int id;

    public override void OnPlayableCreate(Playable playable)
    {
        playableDirector = playable.GetGraph().GetResolver() as PlayableDirector;
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (isClipPlayed == false && info.weight > 0)
        {
            isClipPlayed = true;
            TimeLineManager.Instance.PauseTimeLine();
            LearningManager.Instance.SetLearingWindowUI(id);
        }
    }
}
