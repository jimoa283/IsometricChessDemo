using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketManager : Singleton<PocketManager>
{
    public Dictionary<int, PocketData> pocketDataDic;

    public PocketManager()
    {
        pocketDataDic = new Dictionary<int, PocketData>();
        ParsePocketDataJSON();
    }

    private void ParsePocketDataJSON()
    {
        TextAsset textPocketData = Resources.Load<TextAsset>("JSON/PocketDataJSON");
        JSONObject jSONObject = new JSONObject(textPocketData.text);

        foreach(var obj in jSONObject.list)
        {
            int id = (int)obj["ID"].n;
            int money = (int)obj["Money"].n;
            int itemID = (int)obj["ItemID"].n;
            int itemNum = (int)obj["ItemNum"].n;
            int level = (int)obj["Level"].n;

            //Item item = ItemManager.Instance.GetNumItem(itemID, itemNum);

            Sprite sprite = Resources.Load<GameObject>("Pocket/PocketSprite" + level).GetComponent<SpriteRenderer>().sprite;

            PocketData pocketData = new PocketData(money,itemID,itemNum, sprite);

            pocketDataDic.Add(id, pocketData);
        }
    }

    public PocketData GetPocketData(int id)
    {
        if(pocketDataDic.ContainsKey(id))
        {
            PocketData pocketData = pocketDataDic[id];
            return new PocketData(pocketData.money, pocketData.itemID, pocketData.itemNum, pocketData.pocketSprite);
            
        }

        return null;
    }
}
