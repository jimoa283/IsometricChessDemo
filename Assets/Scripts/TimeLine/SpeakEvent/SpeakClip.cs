using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SpeakClip : PlayableAsset
{
    public SpeakBehavior temp = new SpeakBehavior();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<SpeakBehavior>.Create(graph, temp);
        return playable;
    }
}
