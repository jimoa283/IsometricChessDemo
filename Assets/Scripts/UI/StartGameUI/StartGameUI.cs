using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameUI : BasePanel
{
    private ActionOnStartGameUI actionOnStartGameUI;

    public override void Init()
    {
        base.Init();
        actionOnStartGameUI = TransformHelper.GetChildTransform(transform, "StartGameActionList").GetComponent<ActionOnStartGameUI>();
        actionOnStartGameUI.Init();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        gameObject.SetActive(true);
    }

    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
    }

    public override void OnPause()
    {
        base.OnPause();
        gameObject.SetActive(false);
    }

    public override void OnResume()
    {
        base.OnResume();
        gameObject.SetActive(true);
    }
}
