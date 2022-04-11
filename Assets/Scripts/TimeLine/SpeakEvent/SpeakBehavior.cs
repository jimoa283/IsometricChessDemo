using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class SpeakBehavior : PlayableBehaviour
{
    private PlayableDirector playableDirector;
    private bool isClipPlayed;
    //public string speakIndex;
    public MyDialogueContainerSO speakSO;
    public string pieceName;
    public string targetPieceName;
    private LookDirection oriLookDirection;
    private Piece piece;

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
            piece = PieceQueueManager.Instance.GetPieceByName(pieceName);
            Piece target = PieceQueueManager.Instance.GetPieceByName(targetPieceName);
            if(piece!=null)
            {
                oriLookDirection = piece.pieceStatus.lookDirection;
                LookDirection lookDirection = piece.pieceStatus.lookDirection;
                if(target!=null)
                {
                    lookDirection = SetBattlPieceFunc.AdjustPieceDirection(piece.currentCell, target.currentCell);
                }
                piece.SetIdleDirection(lookDirection);
                //DynamicSpeakUI speakUI = PoolManager.Instance.GetPoolObj("SpeakUI").GetComponent<DynamicSpeakUI>();
                //speakUI.StartSpeak(speakData, piece.transform.position, ReturnToOri);
                SpeakManager.Instance.StartSpeakBySO(speakSO, piece.transform.position, ReturnToOri);
            }          
        }
    }

    private void ReturnToOri()
    {
        piece.SetIdleDirection(oriLookDirection);
        TimeLineManager.Instance.ResumeTimeLine();
        
    }
}
