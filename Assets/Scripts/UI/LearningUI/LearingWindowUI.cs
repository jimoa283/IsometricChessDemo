using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearingWindowUI : BasePanel
{
    private LearningWindow learningWindow;
    

    public override void Init()
    {
        base.Init();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        gameObject.SetActive(true);
        OpenLearingWindow();
    }

    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
        Destroy(transform.GetChild(0).gameObject);
    }

    private void OpenLearingWindow()
    {
        Select.Instance.canMove = false;
        gameObject.SetActive(true);
        learningWindow = GetComponentInChildren<LearningWindow>();
        learningWindow.Init();
        learningWindow.SetControll(true);
        learningWindow.OpenLearingWindowByAnim(transform,Vector3.one,ExitLearningWindowUI);
    }

    private void ExitLearningWindowUI()
    {
        if(BattleGameManager.Instance.isBattling)
            Select.Instance.canMove = true;
        TimeLineManager.Instance.ResumeTimeLine();
        UIManager.Instance.PopPanel(UIPanelType.LearningWindowUI);
    }
}
