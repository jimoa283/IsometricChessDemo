using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BUFFManager : Singleton<BUFFManager>
{
    private Dictionary<int, BUFF> buffDic;

    public BUFFManager()
    {
        buffDic = new Dictionary<int, BUFF>();
        ParseBUFFJSON();
    }

    private void ParseBUFFJSON()
    {
        TextAsset textBUFF = Resources.Load<TextAsset>("JSON/BUFFJSON");
        JSONObject jSONObject = new JSONObject(textBUFF.text);

        foreach(var obj in jSONObject.list)
        {
            int id = (int)obj["ID"].n;
            string _name = obj["Name"].str;
            Sprite buffSprite = Resources.Load<GameObject>(obj["SpritePath"].str).GetComponent<SpriteRenderer>().sprite;
            int aliveTime =(int)obj["AliveTime"].n;
            int power = (int)obj["Power"].n;
            int hitRate = (int)obj["HitRate"].n;
            int defense = (int)obj["Defense"].n;
            int speed = (int)obj["Speed"].n;
            int magic = (int)obj["Magic"].n;
            int avoid = (int)obj["Avoid"].n;
            int magicDefense = (int)obj["MagicDefense"].n;
            int vMove = (int)obj["VMove"].n;
            int lucky = (int)obj["Lucky"].n;
            int hMove = (int)obj["HMove"].n;

            int fireResistance = (int)obj["FireResistance"].n;
            int iceResistance = (int)obj["IceResistance"].n;
            int windResistance = (int)obj["WindResistance"].n;
            int thunderResistance = (int)obj["ThunderResistance"].n;
            int lightResistance = (int)obj["LightResistance"].n;
            int darkResistance = (int)obj["DarkResistance"].n;

            string info = obj["Info"].str;

            string buffEffectObjName = obj["BUFFEffectObjName"].str;

            string _triggerTimeType = obj["TriggerTimeType"].str;
            TriggerTimeType triggerTimeType = (TriggerTimeType)System.Enum.Parse(typeof(TriggerTimeType), _triggerTimeType);

            IPassiveEffect passiveEffect = EffectManager.GetPassiveSkill(id);

            BUFF buff = new BUFF(id, _name,buffSprite,aliveTime, power, hitRate, defense, speed, magic, avoid, magicDefense, vMove, lucky,
                                 hMove,fireResistance,iceResistance,windResistance,thunderResistance,lightResistance,darkResistance,
                                 info,buffEffectObjName ,triggerTimeType, passiveEffect);

            buffDic.Add(id, buff);
        }
    }


    public BUFF GetBUFF(int id)
    {
        if(buffDic.ContainsKey(id))
        {
            BUFF buff = CloneBUFF(buffDic[id]);
            return buff;
        }
        return null;
    }

    private BUFF CloneBUFF(BUFF ori)
    {
        return new BUFF(ori.ID, ori.Name,ori.BUFFSprite ,ori.AliveTime, ori.Power, ori.HitRate, ori.Defense, ori.Speed, ori.Magic, ori.Avoid, ori.MagicDefense,
                         ori.VMove, ori.Lucky, ori.HMove,ori.FireResistance,ori.IceResistance,ori.WindResistance,ori.ThunderResistance,ori.LightResistance,
                         ori.DarkResistance,ori.Info,ori.BUFFEffectObjName ,ori.TriggerTimeType, ori.BUFFEffect);
    }
}
