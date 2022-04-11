using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartSetDevice : MonoBehaviour
{
    public List<Player> playerList;
    public List<Player> battlePlayerList;
    public Enemy[] enemyList;
    public Cell[] playerStartCellList;
    public Cell[] enemyStartCellList;
    public LevelInfo levelInfo;

    private EventCondition eventCondition;

    private VictoryConditon victoryCondition;
    private LoseCondition loseCondition;
    public string[] importantPlayersName;

    private GameObject enemyListObj;

    public void Init()
    {
        enemyListObj = TransformHelper.GetChildTransform(transform, "EnemyListObj").gameObject;
        enemyListObj.gameObject.SetActive(false);
    }

    public void GameStartSet()
    {
        BattleGameManager.Instance.isInBattleScene = true;
        enemyListObj.SetActive(true);
        victoryCondition = GetComponent<VictoryConditon>();
        victoryCondition.Init();
        loseCondition = GetComponent<LoseCondition>();
        loseCondition.Init();

        RandomNumManager.Instance.CreateNewRandomList();

        MainPlayerController.Instance.gameObject.SetActive(false);

        playerList = new List<Player>(BattleGameManager.Instance.playerList);

        BattleGameManager.Instance.enemyList = enemyList;
        BattleGameManager.Instance.playerStartCellList = playerStartCellList;
        BattleGameManager.Instance.enemyStartCellList = enemyStartCellList;
        BattleGameManager.Instance.maxPlayerNum = playerStartCellList.Length;
        BattleGameManager.Instance.levelInfo = levelInfo;

        battlePlayerList = BattleGameManager.Instance.battlePlayerList;

        if(battlePlayerList.Count==0)
        {        
            int i = 0;
            int length = importantPlayersName.Length;
            for (; i < length; i++)
            {
                Player player = BattleGameManager.Instance.GetPlayerByName(importantPlayersName[i]);
                player.SetBattlePos(playerStartCellList[i]);
                BattleGameManager.Instance.importantPlayerList.Add(player);
                battlePlayerList.Add(player);
            }
            for (int j=0; i < playerStartCellList.Length;j++)
            {
                if(!battlePlayerList.Contains(playerList[j]))
                {
                    playerList[j].SetBattlePos(playerStartCellList[i]);
                    battlePlayerList.Add(playerList[j]);
                    i++;
                }
            }
        }
       else
        {
            int count = battlePlayerList.Count;
            for (int i = 0;i<count; i++)
            {
                battlePlayerList[i].SetBattlePos(playerStartCellList[i]);
            }
        }
        for (int j = 0; j < enemyStartCellList.Length; j++)
        {
            enemyList[j].Init();
            enemyList[j].SetBattlePos(enemyStartCellList[j]);
        }

        RangeManager.Instance.InitSettingPieceRange(playerStartCellList);
        UIManager.Instance.GetPanel(UIPanelType.CombatantNumUI);
        //UIManager.Instance.SetCombatantNumUI();
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.BattleReadyUI));
        
        PieceQueueManager.Instance.ResetRealPieceTime();
        PieceQueueManager.Instance.SetPieceActionQueue();
        LevelManager.Instance.CurrentPiece = PieceQueueManager.Instance.TheFirstPiece();

        Select.Instance.SetPosSimple(playerList[0].currentCell);
        Select.Instance.Freeze();

        StartCoroutine(DoEvent());
       
    }

    IEnumerator DoEvent()
    {
        yield return new WaitForSeconds(0.1f);
        eventCondition = GetComponent<EventCondition>();
        if (eventCondition != null)
        {
            eventCondition.DoEvent();
        }
    }
}
