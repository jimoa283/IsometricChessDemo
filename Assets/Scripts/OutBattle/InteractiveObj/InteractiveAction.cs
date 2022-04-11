using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveAction:MonoBehaviour
{
    //public bool hasAction;
    protected InteractiveObject interactiveObject;
    protected TipUI tipUI;
    public string interactiveID;

    public virtual void Init(InteractiveObject interactiveObject,TipUI tipUI)
    {
        this.interactiveObject = interactiveObject;
        this.tipUI = tipUI;
        if(InteractiveActionManager.Instance.GetInteractiveActive(interactiveID))
        {
            AfterInit();
        }
        else
        {
            FirstInit();
        }
    }

    protected virtual void FirstInit()
    {

    }

    protected virtual void AfterInit()
    {
        
    }

    public virtual void DoAction()
    {
        interactiveObject.BaseActionOnClick();
        InteractiveActionManager.Instance.SetInteractiveActive(interactiveID,true);
    }


    protected virtual void ActionAfterAllSpeak()
    {
        MainPlayerController.Instance.WakeInteractiveObject();
    }
}
