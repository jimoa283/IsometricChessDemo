using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save 
{
    public float GameTime;
    public int GameLevel;
    public int RealTime;
    public string SceneName;
    public int Money;
    public int passWord;
    public string realTime;
    //public Sprite SceneSprite;
    public List<string> BattlePlayerList=new List<string>();
    public List<string> PlayerList=new List<string>();
    public List<int> PlayerLevelList = new List<int>();
    public List<int> PlayerCurrentExpList = new List<int>();
    public List<int> PlayerWeaponList = new List<int>();
    public List<int> PlayerArmorList = new List<int>();
    public List<int> PlayerOrnamentList = new List<int>();
    public List<int> AllItemIDList = new List<int>();
    public List<int> AllItemNumList = new List<int>();
    public List<int> ItemShopIDList=new List<int>();
    public List<int> ItemShopNumList = new List<int>();
    public List<bool> SceneInteractiveActiveList = new List<bool>();

    public List<int> HasShowLevelEventList;
    public List<bool> LearingWindowOpenList;

    public List<string> InteractiveActionDicKeys = new List<string>();
    public List<bool> InteractiveActionDicValues = new List<bool>();
    public List<bool> BattleExploreLevelExploreList = new List<bool>();

    public bool isTheNewest;

    public Save(int gameLevel,string sceneName)
    {
        GameTime = Time.time;
        string[] nowTime = System.DateTime.Now.ToString("yyyy:MM:dd").Split(new char[] { ':' });
        realTime = nowTime[0] + "/" + nowTime[1] + "/" + nowTime[2];
        Money = GameManager.Instance.money;
        passWord = MainPlayerController.Instance.passWord;
        foreach(var player in BattleGameManager.Instance.playerList)
        {
            PlayerList.Add(player.pieceStatus.pieceName);
            PlayerLevelList.Add(player.pieceStatus.Level);
            PlayerCurrentExpList.Add(player.pieceStatus.CurrentExp);
            PlayerWeaponList.Add(player.pieceStatus.weaponID);
            PlayerArmorList.Add(player.pieceStatus.armorID);
            PlayerOrnamentList.Add(player.pieceStatus.ornamentID);
        }
        foreach(var player in BattleGameManager.Instance.battlePlayerList)
        {
            BattlePlayerList.Add(player.pieceStatus.pieceName);
        }
        SceneName = sceneName;
        foreach(var item in ItemManager.Instance.AllItemBag)
        {
            AllItemIDList.Add(item.ID);
            AllItemNumList.Add(item.Number);
        }
        foreach(var item in ItemShopManager.Instance.CurrentShopItemList.AllItemList)
        {
            ItemShopIDList.Add(item.ID);
            ItemShopNumList.Add(item.Number);
        }

        HasShowLevelEventList = new List<int>(LevelEventManager.Instance.hasShowLevelEventList);
        LearingWindowOpenList = new List<bool>(LearningManager.Instance.LearingWindowOpenList);

        foreach(var action in InteractiveActionManager.Instance.InteractiveActionActiveDic)
        {
            InteractiveActionDicKeys.Add(action.Key);
            InteractiveActionDicValues.Add(action.Value);
        }

        foreach(bool check in BattleGameManager.Instance.battleLevelExploreList)
        {
            BattleExploreLevelExploreList.Add(check);
        }
    }
}
