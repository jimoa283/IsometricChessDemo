using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleCheckWindowUI : BasePanel
{
    private SimpleActionCheckWindow simpleActionCheckWindow;

    public override void Init()
    {
        base.Init();
        simpleActionCheckWindow = TransformHelper.GetChildTransform(transform, "SimpleCheckWindow").GetComponent<SimpleActionCheckWindow>();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        gameObject.SetActive(true);
        simpleActionCheckWindow.OpenWindow();
    }

    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
    }

    public override void OnPause()
    {
        base.OnPause();
    }

    public override void OnResume()
    {
        base.OnResume();
    }

    public void SetSimpleCheckWindow(UnityAction[] actions,SimpleCheckWindowData simpleCheckWindowData)
    {
        simpleActionCheckWindow.SimpleWindowInit(actions, simpleCheckWindowData);
    }
}
