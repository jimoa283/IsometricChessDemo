using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExploreOpenDevice : MonoBehaviour
{
    
    private GameObject interactiveObjectListObj;
    private LevelStartSetDevice levelStartSetDevice;
    public int levelID;
    public int changeGameLevel;
    private UnityAction[] actions;

    private void Start()
    {        
        interactiveObjectListObj = TransformHelper.GetChildTransform(transform, "InteractiveObjectListObj").gameObject;
        levelStartSetDevice = TransformHelper.GetChildTransform(transform, "LevelStartSetDevice").GetComponent<LevelStartSetDevice>();
        levelStartSetDevice.Init();
        if(!BattleGameManager.Instance.battleLevelExploreList[levelID])
        {
            actions = new UnityAction[] { ExitExplore, CancelCheckWindow };
            interactiveObjectListObj.SetActive(true);
            MainPlayerController.Instance.gameObject.SetActive(true);
            MainPlayerController.Instance.canControll = false;
            EventCenter.Instance.EventTrigger<UnityAction>("SceneStartFadeIn", ExploreInit);
        }       
        else
        {
            interactiveObjectListObj.SetActive(false);
            MainPlayerController.Instance.gameObject.SetActive(false);
            BattleGameManager.Instance.isInBattleScene = true;
            EventCenter.Instance.EventTrigger<UnityAction>("SceneStartFadeIn",BattleInit);
        }
    }

    private void ExploreInit()
    {
        MainPlayerController.Instance.ExitAction = ExitAction;
        MainPlayerController.Instance.canControll = true;
    }

    private void BattleInit()
    {
        ChangeToBattleScene();
    }

    private void ExitAction()
    {
        CheckWindowManager.Instance.SetSimpleCheckWindow("ExitExploreCheckWindow", actions);
    }

    public void ChangeToBattleScene()
    {     
        levelStartSetDevice.GameStartSet();
    }

    public void ExitExplore()
    {
        BattleGameManager.Instance.battleLevelExploreList[levelID] = true;
        interactiveObjectListObj.SetActive(false);
        GameManager.Instance.gameLevel = changeGameLevel;
        EventCenter.Instance.EventTrigger("BigSceneChangeFadeOut", "Map");
    }

    public void CancelCheckWindow()
    {
        UIManager.Instance.PopPanel(UIPanelType.SimpleCheckWindowUI);
    }
}


