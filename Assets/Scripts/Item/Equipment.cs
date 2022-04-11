using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Ornament
}

public class Equipment : Item
{
    public EquipmentType EquipmentType;
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
    public int Critical;
    public int FireResistance;
    public int IceResistance;
    public int WindResistance;
    public int ThunderResistance;
    public int LightResistance;
    public int DarkResistance;
    public Piece Owner;

    public Equipment(int iD, string name, ElementType type, string info, Sprite sprite, ItemType itemType,int buyPrice,int sellPrice,
                      EquipmentType equipmentType,int power,int hitRate,int defense,int speed,int magic,int avoid,int magicDefense,
                      int vMove,int lucky,int hMove,int critical,int fireResistance,int iceResistance,int windResistance,int thunderResistance,
                      int lightResistance,int darkResistance) : base(iD, name, type, info ,sprite, itemType,buyPrice,sellPrice)
    {
        EquipmentType = equipmentType;
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
        Critical = critical;
        FireResistance = fireResistance;
        IceResistance = iceResistance;
        WindResistance = windResistance;
        ThunderResistance = thunderResistance;
        LightResistance = lightResistance;
        DarkResistance = darkResistance;
        Owner = null;
    }
}
