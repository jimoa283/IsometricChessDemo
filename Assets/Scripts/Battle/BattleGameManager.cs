using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class BattleGameManager : MSingleton<BattleGameManager>
{
    public List<bool> battleLevelExploreList;

    public int battleLevelNum;

    public Transform playerListT;
    public List<Player> playerList=new List<Player>();
    public List<Player> battlePlayerList=new List<Player>();
    public List<Player> importantPlayerList = new List<Player>();
    public Enemy[] enemyList ;
    public Cell[] playerStartCellList;
    public Cell[] enemyStartCellList;
    public LevelInfo levelInfo;
    public UnityAction BeforeBattleVictoryAction;
    public UnityAction BeforeBattleLoseAction;
    public List<PocketData> pocketDatas = new List<PocketData>();
    public int pointGetNum;

    public int maxPlayerNum=3;

    public bool isInBattleScene;
    public bool isBattling=false;

    public string oriLevelSceneName;
    private string nextSceneName;

    public string NextSceneName { get => nextSceneName; set => nextSceneName = value; }

    private void Start()
    {
        battleLevelExploreList = new List<bool>();
        for (int i = 0; i < battleLevelNum; i++)
        {
            battleLevelExploreList.Add(false);
        }
    }

    public void FirstStart()
    {
        ItemManager.Instance.ChangeItemNum(UseItemManager.Instance.GetUseItem(1001), 2);
        ItemManager.Instance.ChangeItemNum(UseItemManager.Instance.GetUseItem(1002), 3);
        ItemManager.Instance.ChangeItemNum(EquipmentManager.Instance.GetEquipment(2002), 3);
        ItemManager.Instance.ChangeItemNum(EquipmentManager.Instance.GetEquipment(2001), 3);
        ItemManager.Instance.ChangeItemNum(UseItemManager.Instance.GetUseItem(1001), 4);

        AddPlayerInPlayerList("Li");
        AddPlayerInPlayerList("Wo");
        AddPlayerInPlayerList("Ta");
    }

    public void AddPlayerInPlayerList(string _name)
    {
        if(playerListT==null)
        {
            playerListT = new GameObject("PlayerList").transform;
            playerListT.SetParent(gameObject.transform);
        }
        Player player =Instantiate(Resources.Load<GameObject>("Piece/Player/"+_name),playerListT).GetComponent<Player>();
        player.Init();
        playerList.Add(player);
        player.gameObject.SetActive(false);
    }

    public Player GetPlayerByName(string playerName)
    {
        foreach(var player in playerList)
        {
            if (player.pieceStatus.pieceName == playerName)
                return player;
        }

        return null;
    }

    public void BattleStart()
    {
        isBattling = true;
        UIManager.Instance.PopPanel(UIPanelType.BattleReadyUI);
        UIManager.Instance.GetPanel(UIPanelType.PassiveSkillShowUI);
        UIManager.Instance.SetGetSkillUI();
        RangeManager.Instance.ClearSettingPieceRange();
        LevelManager.Instance.ChangeLevelState(LevelStateID.CheckSpecEventBeforeTurnStart);       
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.PieceQueueUI));
    }

    public void BattleVictory()
    {
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.VictoriesUI));
    }

    public void BattleLose()
    {
        Debug.Log("Lose");
    }


    public void  BigSceneChangeEvent(string sceneName)
    {
        if(isInBattleScene)
        {
            PieceQueueManager.Instance.ClearPieceQueue();
            LevelManager.Instance.ClearStateList();
            CellManager.Instance.ClearCellDic();
            Select.Instance.gameObject.SetActive(false);
            BattleManager.Instance.BattleClearOnSceneChange();
            foreach (var player in playerList)
            {
                player.gameObject.SetActive(false);
            }
        }

        EventCenter.Instance.ClearEvent();
        UIManager.Instance.ClearDic();
        PoolManager.Instance.ClearDic();
        MainPlayerController.Instance.gameObject.SetActive(false);
        nextSceneName = sceneName;
        SceneManager.LoadScene("LoadScene");
    }

    public void SmallSceneChangeEvent(string sceneName)
    {        
        EventCenter.Instance.ClearEvent();
        PoolManager.Instance.ClearDic();
        UIManager.Instance.ClearDic();
        SceneManager.LoadScene(sceneName);
    }
}
