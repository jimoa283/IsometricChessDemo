using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : BasePanel
{
    private ActionShow actionShow;

    public override void Init()
    {
        base.Init();
        actionShow = GetComponentInChildren<ActionShow>();
        actionShow.Init();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        gameObject.SetActive(true);
        UIManager.Instance.PauseBeforePanel();
        //actionShow.SetActionName(BattleManager.Instance.ActionData.baseActionData.Name);

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
