using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PersuadeWindowUI : BasePanel
{
    private PersuadeWindow persuadeWindow;

    public override void Init()
    {
        base.Init();
        persuadeWindow = TransformHelper.GetChildTransform(transform, "PersuadeWindow").GetComponent<PersuadeWindow>();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        gameObject.SetActive(true);
        persuadeWindow.OpenWindow();
    }

    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
        persuadeWindow.OpenWindow();
    }

    public override void OnPause()
    {
        base.OnPause();
    }

    public override void OnResume()
    {
        base.OnResume();
    }

    public void SetPersuadeWindow(UnityAction[] actions,PersuadeWindowData persuadeWindowData)
    {
        persuadeWindow.PersuadeWindowInit(persuadeWindowData, actions);
    }
}
