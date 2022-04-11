using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneChangeAction : InteractiveAction
{
    public string changeSceneName;
    public string checkWindowName;
    public int passWord;
    private UnityAction[] actions;

    public override void DoAction()
    {
        interactiveObject.BaseActionOnClick();
        if (actions == null)
            actions = new UnityAction[] {ChangeScene, ActionAfterAllSpeak};
        CheckWindowManager.Instance.SetSimpleCheckWindow(checkWindowName, actions);
    }

    private void ChangeScene()
    {
        MainPlayerController.Instance.passWord = passWord;
        //BattleGameManager.Instance.BigSceneChangeEvent(changeSceneName);
        EventCenter.Instance.EventTrigger("BigSceneChangeFadeOut", changeSceneName);
    }

}
