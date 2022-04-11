using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PieceInitClip : PlayableAsset
{
    public PieceInitBehavior temp = new PieceInitBehavior();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<PieceInitBehavior>.Create(graph, temp);
        return playable;
    }
}
