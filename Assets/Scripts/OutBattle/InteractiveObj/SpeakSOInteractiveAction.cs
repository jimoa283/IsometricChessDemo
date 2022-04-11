using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakSOInteractiveAction : InteractiveAction
{
    public MyDialogueContainerSO firstSpeakSO;
    public MyDialogueContainerSO secondSpeakSO;
    private MyDialogueContainerSO currentSpeakSO;

    public override void DoAction()
    {
        base.DoAction();
        SpeakManager.Instance.StartSpeakBySO(currentSpeakSO, interactiveObject.transform.position, ActionAfterAllSpeak);
        currentSpeakSO = secondSpeakSO;
    }

    protected override void AfterInit()
    {
        currentSpeakSO = secondSpeakSO;
    }

    protected override void FirstInit()
    {
        currentSpeakSO = firstSpeakSO;
    }
}
