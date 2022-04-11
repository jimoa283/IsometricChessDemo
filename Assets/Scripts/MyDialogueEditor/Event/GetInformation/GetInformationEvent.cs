using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Event", menuName = "MyDialogue/GetInformationEvent")]
public class GetInformationEvent : EventSO
{
    public int InformationID;

    public override void DoEvent(UnityAction action)
    {
        BriefIntelligenceManager.Instance.ShowBriefIntelligence(InformationID);
        action?.Invoke();
    }
}
