using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GarrisonDevice : MonoBehaviour
{
    private UnityAction[] actions;

    private void Start()
    {
        MainPlayerController.Instance.gameObject.SetActive(true);
        MainPlayerController.Instance.canControll = false;
        actions = new UnityAction[] { ExitGarrison, CancelCheckWindow };

        EventCenter.Instance.EventTrigger<UnityAction>("SceneStartFadeIn", Init);
    }

    private void Init()
    {
        MainPlayerController.Instance.canControll = true;
        MainPlayerController.Instance.ExitAction = ExitAction;
    }

    private void ExitAction()
    {
        CheckWindowManager.Instance.SetSimpleCheckWindow("ExitGarrisonCheckWindow", actions);
    }

    private void ExitGarrison()
    {
        EventCenter.Instance.EventTrigger("BigSceneChangeFadeOut", BattleGameManager.Instance.oriLevelSceneName);
        //BattleGameManager.Instance.BigSceneChangeEvent(BattleGameManager.Instance.oriLevelSceneName);
    }

    private void CancelCheckWindow()
    {
        UIManager.Instance.PopPanel(UIPanelType.SimpleCheckWindowUI);
    }
}
