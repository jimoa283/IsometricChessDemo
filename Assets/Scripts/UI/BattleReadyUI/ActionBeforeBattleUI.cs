using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionBeforeBattleUI : BasePlayerActionSelectUI
{

    private BBActionSlot pieceSetting;
    private BBActionSlot pieceListDetail;
    private BBActionSlot garrison;
    private BBActionSlot items;
    private BBActionSlot learning;
    private BBActionSlot system;
    private BBActionSlot battleStart;

    protected override void SpecialInit()
    {
        pieceSetting = transform.GetChildTransform( "PieceSetting").GetComponent<BBActionSlot>();
        pieceSetting.Init(OpenPieceSetting);

        pieceListDetail = transform.GetChildTransform("PieceListDetail").GetComponent<BBActionSlot>();
        pieceListDetail.Init(OpenPieceListDetail);

        garrison = transform.GetChildTransform("Garrison").GetComponent<BBActionSlot>();
        garrison.Init(EnterGarrison);

        items = transform.GetChildTransform("Items").GetComponent<BBActionSlot>();
        items.Init(OpenItemBag);

        learning = transform.GetChildTransform("Learning").GetComponent<BBActionSlot>();
        learning.Init(OpenLearningUI);

        system = transform.GetChildTransform("System").GetComponent<BBActionSlot>();
        system.Init(OpenSystemActionUI);

        battleStart = transform.GetChildTransform("BattleStart").GetComponent<BBActionSlot>();
        battleStart.Init(BattleStart);

        if(BattleGameManager.Instance.isInBattleScene)
        {
            pieceListDetail.gameObject.SetActive(false);
            bBActionSlots = new BBActionSlot[] { pieceSetting, garrison, items, learning, system, battleStart };
        }
        else
        {
            battleStart.gameObject.SetActive(false);
            pieceSetting.gameObject.SetActive(false);
            bBActionSlots = new BBActionSlot[] { pieceListDetail, garrison, items, learning, system };
        }       
    }

    public void OpenPieceSelect()
    {
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.PieceInformationUI));
    }

    public void OpenPieceSetting()
    {
        UIManager.Instance.PopPanel(UIPanelType.BattleReadyUI);
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.PieceQueueUI));
        //Select.Instance.SetPosBeforeBattle(Select.Instance.currentCell);
        Select.Instance.ChangeSelectState(SelectStateID.SetPieceState);
    }

    public void BattleStart()
    {
        BattleGameManager.Instance.BattleStart();
    }

    public void OpenItemBag()
    {
        EventCenter.Instance.EventTrigger("HideMissionInfo");
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.ItemBagUI));
    }

    public void EnterGarrison()
    {
        BattleGameManager.Instance.oriLevelSceneName =SceneManager.GetActiveScene().name;
        EventCenter.Instance.EventTrigger("BigSceneChangeFadeOut", "Garrison_");
        //BattleGameManager.Instance.BigSceneChangeEvent("Garrison_");
    }

    public void OpenLearningUI()
    {
        UIManager.Instance.GetPanel(UIPanelType.BattleReadyUI).gameObject.SetActive(false);
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.LearningUI));
    }

    public void OpenSystemActionUI()
    {
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.SystemActionUI));
    }

    public void OpenPieceListDetail()
    {
        UIManager.Instance.GetPanel(UIPanelType.BattleReadyUI).gameObject.SetActive(false);
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.PieceInformationUI));
    }

    public override void PopThisPanel()
    {
        if(!BattleGameManager.Instance.isInBattleScene)
        {
            UIManager.Instance.PopPanel(UIPanelType.BattleReadyUI);
        }
    }
}
