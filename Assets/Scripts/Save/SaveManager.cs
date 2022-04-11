using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : Singleton<SaveManager>
{
    private Save[] saveList;
    private Save currentSave;
    
    public SaveManager()
    {
        saveList = new Save[5];
        ParseSaveJson();
    }

    public Save[] SaveList { get => saveList; }
    public Save CurrentSave { get => currentSave;}

    private void ParseSaveJson()
    {
        if (!Directory.Exists(Application.dataPath + "/saveData"))
            return;
        for (int i = 0; i < 5; i++)
        {
            if (File.Exists(Application.dataPath + "/saveData/Save0" + i + ".txt"))
            {
                StreamReader sr = new StreamReader(Application.dataPath + "/saveData/Save0" + i + ".txt");
                string jsonString = sr.ReadToEnd();
                sr.Close();
                Save save = JsonUtility.FromJson<Save>(jsonString);
                saveList[i] = save;
            }
            else
                saveList[i] = null;
        }
    }

    public Save AddSave(int index,string sceneName)
    {
        Save save = new Save(0,sceneName);
        save.isTheNewest = true;
        foreach(var _save in saveList)
        {
            if (_save != null && _save != save)
                _save.isTheNewest = false;
        }
        saveList[index] = save;
        currentSave = save;
        string jsonString = JsonUtility.ToJson(save);
        if(!Directory.Exists(Application.dataPath + "/saveData"))
            Directory.CreateDirectory(Application.dataPath + "/saveData");
        if (File.Exists(Application.dataPath + "/saveData/Save0" + index + ".txt"))
            File.Delete(Application.dataPath + "/saveData/Save0" + index + ".txt");
        StreamWriter sw = new StreamWriter(Application.dataPath + "/saveData/Save0"+index+".txt");
        sw.Write(jsonString);
        sw.Close();
        return save;
    }

    public void LoadSave(int index)
    {
        if (File.Exists(Application.dataPath + "/saveData/Save0" + index + ".txt"))
        {
            StreamReader sr = new StreamReader(Application.dataPath + "/saveData/Save0" + index + ".txt");
            string jsonString = sr.ReadToEnd();
            sr.Close();
            Save save = JsonUtility.FromJson<Save>(jsonString);

            GameManager.Instance.money = save.Money;

            MainPlayerController.Instance.passWord = save.passWord;

            BattleGameManager.Instance.playerList.Clear();
            BattleGameManager.Instance.battlePlayerList.Clear();
            if(BattleGameManager.Instance.playerListT!=null)
                 GameObject.Destroy(BattleGameManager.Instance.transform.GetChild(0));
            BattleGameManager.Instance.playerListT = new GameObject("PlayerList").transform;
            BattleGameManager.Instance.playerListT.SetParent(BattleGameManager.Instance.transform);

            ItemManager.Instance.AllUseItemBag.Clear();
            ItemManager.Instance.AllEquipmentBag.Clear();
            for (int i = 0; i < save.AllItemIDList.Count; i++)
            {
                Item item = ItemManager.Instance.GetNumItem(save.AllItemIDList[i], save.AllItemNumList[i]);
                ItemManager.Instance.InsertItem(item);
            }

            ItemShopListModel temp = new ItemShopListModel();
            for (int i = 0; i < save.ItemShopIDList.Count; i++)
            {
                Item item = ItemManager.Instance.GetNumItem(save.ItemShopIDList[i], save.ItemShopNumList[i]);
                temp.AddItem(item);
            }
            ItemShopManager.Instance.CurrentShopItemList = temp;

            for (int i = 0; i < save.PlayerList.Count; i++)
            {
                Player player =GameObject.Instantiate(Resources.Load<GameObject>("Piece/Player/"+save.PlayerList[i]),BattleGameManager.Instance.playerListT).GetComponent<Player>();
                PlayerStatus playerStatus = player.GetComponent<PlayerStatus>();
                playerStatus.weaponID = save.PlayerWeaponList[i];
                playerStatus.armorID = save.PlayerArmorList[i];
                playerStatus.ornamentID = save.PlayerOrnamentList[i];
                playerStatus.Level = 0;
                player.Init();
                player.pieceStatus.Level = save.PlayerLevelList[i];
                player.pieceStatus.CurrentExp = save.PlayerCurrentExpList[i];
                BattleGameManager.Instance.playerList.Add(player);
                if (save.BattlePlayerList.Contains(save.PlayerList[i]))
                    BattleGameManager.Instance.battlePlayerList.Add(player);              
            }

            BattleGameManager.Instance.battleLevelExploreList = save.BattleExploreLevelExploreList;

            LevelEventManager.Instance.hasShowLevelEventList = new List<int>(save.HasShowLevelEventList);
            LearningManager.Instance.LoadLearningWindowOpenList(save.LearingWindowOpenList);

            InteractiveActionManager.Instance.LoadInteractiveDic(save.InteractiveActionDicKeys, save.InteractiveActionDicValues);

            EventCenter.Instance.EventTrigger("BigSceneChangeFadeOut", save.SceneName);
            //BattleGameManager.Instance.BigSceneChangeEvent(save.SceneName);
        }
    }

   public void RemoveSave(Save save)
    {
        for (int i = 0; i < saveList.Length; i++)
        {
            if(save==saveList[i])
            {
                saveList[i] = null;
                if (File.Exists(Application.dataPath + "/saveData/Save0" + i + ".txt"))
                    File.Delete(Application.dataPath + "/saveData/Save0" + i + ".txt");
                return;
            }
        }
    }
}
