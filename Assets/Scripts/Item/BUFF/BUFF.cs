using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BUFF 
{
    public int ID;
    public string Name;
    public Sprite BUFFSprite;
    public int AliveTime;
    public int Power;
    public int HitRate;
    public int Defense;
    public int Speed;
    public int Magic;
    public int Avoid;
    public int MagicDefense;
    public int VMove;
    public int Lucky;
    public int HMove;
    public int FireResistance;
    public int IceResistance;
    public int WindResistance;
    public int ThunderResistance;
    public int LightResistance;
    public int DarkResistance;
    public string Info;
    public string BUFFEffectObjName;
    public TriggerTimeType TriggerTimeType;
    public IPassiveEffect BUFFEffect;

    public BUFF(int id,string name,Sprite buffSprite,int aliveTime,int power, int hitRate, int defense, int speed, int magic, int avoid, int magicDefense, int vMove, 
                int lucky, int hMove,int fireResistance,int iceResistance,int windResistance,int thunderResistance,int lightResistance,int darkResistance,string info,string buffEffectObjName,TriggerTimeType triggerTimeType, IPassiveEffect buffEffect)
    {
        ID = id;
        Name = name;
        BUFFSprite = buffSprite;
        AliveTime = aliveTime;
        Power = power;
        HitRate = hitRate;
        Defense = defense;
        Speed = speed;
        Magic = magic;
        Avoid = avoid;
        MagicDefense = magicDefense;
        VMove = vMove;
        Lucky = lucky;
        HMove = hMove;
        FireResistance = fireResistance;
        IceResistance = iceResistance;
        WindResistance = windResistance;
        ThunderResistance = thunderResistance;
        LightResistance = lightResistance;
        DarkResistance = darkResistance;
        Info = info;
        BUFFEffectObjName = buffEffectObjName;
        TriggerTimeType = triggerTimeType;
        BUFFEffect = buffEffect;
    }
}
