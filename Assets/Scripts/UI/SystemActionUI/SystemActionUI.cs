using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemActionUI : BasePanel
{
    private SystemActionSelectUI systemActionSelectUI;

    public override void Init()
    {
        base.Init();
        systemActionSelectUI = TransformHelper.GetChildTransform(transform, "SystemActionSelectUI").GetComponent<SystemActionSelectUI>();
        systemActionSelectUI.Init();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        UIManager.Instance.PauseBeforePanel();
        systemActionSelectUI.OpenSelectUI();
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
    }

    public override void OnResume()
    {
        base.OnResume();
    }
}
