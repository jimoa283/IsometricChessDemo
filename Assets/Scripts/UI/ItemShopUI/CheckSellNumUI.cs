using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSellNumUI : BasePanel
{
    private CheckSellWindow checkSellWindow;
    public override void Init()
    {
        base.Init();
        checkSellWindow = TransformHelper.GetChildTransform(transform, "CheckSellWindow").GetComponent<CheckSellWindow>();
        checkSellWindow.Init();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        UIManager.Instance.PauseBeforePanel();
        gameObject.SetActive(true);
        //checkSellWindow.SetWindow();
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
