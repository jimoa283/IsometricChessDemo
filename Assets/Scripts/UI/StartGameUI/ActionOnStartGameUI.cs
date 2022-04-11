using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionOnStartGameUI : BasePlayerActionSelectUI
{
    private BBActionSlot startGame;
    private BBActionSlot continueGame;
    private BBActionSlot options;

    protected override void SpecialInit()
    {
        startGame = transform.GetChildTransform( "StartGame").GetComponent<BBActionSlot>();
        startGame.Init(StartGame);

        continueGame = transform.GetChildTransform( "ContinueGame").GetComponent<BBActionSlot>();
        continueGame.Init(ContinueGame);

        options = transform.GetChildTransform("Options").GetComponent<BBActionSlot>();
        options.Init(OpenOptions);

        bBActionSlots = new BBActionSlot[] { startGame, continueGame, options };
    }

    private void ContinueGame()
    {
        UIManager.Instance.PauseBeforePanel();
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.LoadUI));       
    }

    private void StartGame()
    {
        GameManager.Instance.gameLevel = 11;
        LevelEventManager.Instance.hasShowLevelEventList.Clear();
        BattleGameManager.Instance.FirstStart();
        //BattleGameManager.Instance.SmallSceneChangeEvent("Map");       
        //(UIManager.Instance.GetPanel(UIPanelType.SceneChangeFadeUI) as SceneChangeFadeUI).SmallSceneChangeFadeOut("Map");
        EventCenter.Instance.EventTrigger("SmallSceneChangeFadeOut", "Map");
    }

    private void OpenOptions()
    {

    }
}
