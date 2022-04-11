using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LearningWindowClip : PlayableAsset
{
    public LearningWindowBehavior temp = new LearningWindowBehavior();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<LearningWindowBehavior>.Create(graph, temp);
        return playable;
    }
}
