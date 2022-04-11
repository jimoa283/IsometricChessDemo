using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEventManager :Singleton<LevelEventManager>
{
    public Dictionary<int, List<LevelEventInfo>> levelEventDic;

    public List<int> hasShowLevelEventList;

    public LevelEventManager()
    {
        levelEventDic = new Dictionary<int, List<LevelEventInfo>>();
        hasShowLevelEventList = new List<int>();
        ParseLevelEventJson();
    }

    private void ParseLevelEventJson()
    {
        TextAsset textLevelEvent = Resources.Load<TextAsset>("JSON/LevelEventJSON");
        JSONObject jSONObject = new JSONObject(textLevelEvent.text);

        foreach(var obj in jSONObject.list)
        {
            int level = (int)obj["Level"].n;

            int id = (int)obj["ID"].n;

            float posX = obj["PosX"].f;
            float posY = obj["PosY"].f;
            Vector2 pos = new Vector2(posX, posY);

            string placeName = obj["PlaceName"].str;
            string eventInfo = obj["EventInfo"].str;
            eventInfo = eventInfo.Replace('#', '\n');
            string placeInfo = obj["PlaceInfo"].str;
            placeInfo = placeInfo.Replace('#', '\n');
            Sprite placeSprite = Resources.Load<GameObject>("Sprite/" + obj["SpritePath"].str).GetComponent<SpriteRenderer>().sprite;
            LevelEventType levelEventType = (LevelEventType)System.Enum.Parse(typeof(LevelEventType), obj["LevelEventType"].str);
            LevelEventActionType levelEventActionType = (LevelEventActionType)System.Enum.Parse(typeof(LevelEventActionType), obj["LevelEventActionType"].str);
            string sceneIndex =obj["SceneName"].str;

            LevelEventInfo levelEventInfo = new LevelEventInfo(level,id,pos, placeName, eventInfo, placeInfo, placeSprite, levelEventType,levelEventActionType,sceneIndex);

            if (!levelEventDic.ContainsKey(level))
            {
                levelEventDic.Add(level, new List<LevelEventInfo>());
            }

            levelEventDic[level].Add(levelEventInfo);
        }
    }

    public List<LevelEventInfo> GetLevelEventInfoList(int level)
    {
        return levelEventDic[level];
    }
}

public enum LevelEventType
{
    Main,
    Side
}

public enum LevelEventActionType
{
    Simple,
    Battle,
    PlayerGet,
    Special
}

public class LevelEventInfo
{
    public int Level;
    public int ID;
    public Vector2 Pos;
    public string PlaceName;
    public string EventInfo;
    public string PlaceInfo;
    public Sprite PlaceSprite;
    public LevelEventType LevelEventType;
    public LevelEventActionType LevelEventActionType;
    public string SceneName;

    public LevelEventInfo(int level,int id,Vector2 pos, string placeName, string eventInfo, string placeInfo, Sprite placeSprite, LevelEventType levelEventType,LevelEventActionType levelEventActionType,string sceneName)
    {
        Level = level;
        ID = id;
        Pos = pos;
        PlaceName = placeName;
        EventInfo = eventInfo; 
        PlaceInfo = placeInfo;
        PlaceSprite = placeSprite;
        LevelEventType = levelEventType;
        LevelEventActionType = levelEventActionType;
        SceneName = sceneName;
    }
}
