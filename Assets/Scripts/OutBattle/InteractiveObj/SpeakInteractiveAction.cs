using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeakInteractiveAction : InteractiveAction
{
    public string firstSpeakStartID;
    public string secondSpeakStartID;
    private string currentSpeakStartID;

    public override void DoAction()
    {
        base.DoAction();
        SpeakManager.Instance.StartSpeakById(currentSpeakStartID, interactiveObject.transform.position, ActionAfterAllSpeak);
        currentSpeakStartID = secondSpeakStartID;
    }

    protected override void AfterInit()
    {
        currentSpeakStartID = firstSpeakStartID;
    }

    protected override void FirstInit()
    {
        currentSpeakStartID = secondSpeakStartID;
    }
}
