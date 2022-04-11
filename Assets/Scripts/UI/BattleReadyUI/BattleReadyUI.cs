using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleReadyUI : BasePanel
{
    private ActionBeforeBattleUI actionBeforeBattleUI;
    private MissionInfo missionInfo;

    public override void Init()
    {
        base.Init();
        actionBeforeBattleUI = GetComponentInChildren<ActionBeforeBattleUI>();
        missionInfo = GetComponentInChildren<MissionInfo>();
        actionBeforeBattleUI.Init();
        missionInfo.Init();
    }

    void Update()
    {
        
    }

    public override void OnEnter()
    {
        base.OnEnter();
        gameObject.SetActive(true);
        actionBeforeBattleUI.OpenSelectUI();
        if (BattleGameManager.Instance.isInBattleScene)
        {
            missionInfo.gameObject.SetActive(false);
            missionInfo.ShowMissionInfo();
            EventCenter.Instance.EventTrigger("HideCombatantNum");
        }
        else
        {
            missionInfo.gameObject.SetActive(false);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
    }

    public override void OnPause()
    {
        base.OnPause();
        //gameObject.SetActive(false);
        //missionInfo.gameObject.SetActive(false);
    }

    public override void OnResume()
    {
        base.OnResume();
        gameObject.SetActive(true);
        if(BattleGameManager.Instance.isInBattleScene)
           missionInfo.ShowMissionInfo();
    }

    /*public void MissonInfoHide()
    {
        missionInfo.HideSelf();
    }*/
}
