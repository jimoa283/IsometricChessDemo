using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PieceInitBehavior : PlayableBehaviour
{
    private PlayableDirector playableDirector;
    private bool isClipPlayed;

    public override void OnPlayableCreate(Playable playable)
    {
        playableDirector = playable.GetGraph().GetResolver() as PlayableDirector;
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if(isClipPlayed==false&&info.weight>0)
        {
            isClipPlayed = true;
            TimeLineManager.Instance.PauseTimeLine();
            PieceInitDevice pieceInitDevice = playableDirector.GetComponent<PieceInitDevice>();
            pieceInitDevice.PieceInit(TimeLineManager.Instance.ResumeTimeLine);
        }
    }
}
