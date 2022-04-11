using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PersuadeAction : InteractiveAction
{
    public string persuadeWindowName;
    private UnityAction[] actions;
    public AttitudeType attitudeType;
    public string applaudSpeakID;
    public string againstSpeakID;

    protected override void AfterInit()
    {
       tipUI.SetDisable(true);
    }

    public override void DoAction()
    {
        interactiveObject.BaseActionOnClick();
        CheckWindowManager.Instance.SetPersuadeWindow(persuadeWindowName, actions);
    }

    private void ApplaudPersuade()
    {
        InteractiveActionManager.Instance.SetInteractiveActive(interactiveID, true);
        tipUI.SetDisable(true);
        SpeakManager.Instance.StartSpeakById(applaudSpeakID, interactiveObject.transform.position, ActionAfterAllSpeak);
    }

    private void AgainstPersuade()
    {
        InteractiveActionManager.Instance.SetInteractiveActive(interactiveID, true);
        tipUI.SetDisable(true);
        SpeakManager.Instance.StartSpeakById(againstSpeakID, interactiveObject.transform.position, ActionAfterAllSpeak);
    }

    protected override void FirstInit()
    {
        switch (attitudeType)
        {
            case AttitudeType.Applaud:
                actions = new UnityAction[] { ApplaudPersuade };
                break;
            case AttitudeType.Against:
                actions = new UnityAction[] { AgainstPersuade };
                break;
            case AttitudeType.Neutrality:
                actions = new UnityAction[] { ApplaudPersuade, AgainstPersuade };
                break;
            default:
                break;
        }
    }
}
