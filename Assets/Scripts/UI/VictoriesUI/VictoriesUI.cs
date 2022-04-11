using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoriesUI : BasePanel
{
    private VictoriesShowWindow victoriesShowWindow;

    public override void Init()
    {
        base.Init();
        victoriesShowWindow = TransformHelper.GetChildTransform(transform, "VictoriesShowUI").GetComponent<VictoriesShowWindow>();
        victoriesShowWindow.Init();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        gameObject.SetActive(true);
        UIManager.Instance.PauseBeforePanel();
        victoriesShowWindow.SetVictoriesWindow();
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
