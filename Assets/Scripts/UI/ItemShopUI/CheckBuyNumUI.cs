using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBuyNumUI : BasePanel
{
    private CheckBuyWindow checkBuyWindow;

    public override void Init()
    {
        base.Init();
        checkBuyWindow = TransformHelper.GetChildTransform(transform, "CheckBuyWindow").GetComponent<CheckBuyWindow>();
        checkBuyWindow.Init();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        UIManager.Instance.PauseBeforePanel();
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
