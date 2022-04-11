using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Event", menuName = "MyDialogue/IntroduceEvent")]
[System.Serializable]

public class IntroduceEvent : EventSO
{
    public string IntroducePieceName;

    public override void DoEvent(UnityAction action) 
    {
        PieceIntroduceManager.Instance.SetPieceIntroduce(IntroducePieceName,action);
    }
}
