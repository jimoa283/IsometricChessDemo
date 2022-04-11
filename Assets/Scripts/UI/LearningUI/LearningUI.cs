using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningUI : BasePanel
{
    private LearningShowUI learningShowUI;

    public override void Init()
    {
        base.Init();
        learningShowUI = TransformHelper.GetChildTransform(transform, "LearningShowUI").GetComponent<LearningShowUI>();
        learningShowUI.Init();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        gameObject.SetActive(true);
        learningShowUI.OpenLearningShowUI();
    }

    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
        UIManager.Instance.GetPanel(UIPanelType.BattleReadyUI).gameObject.SetActive(true);
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
