using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : Equipment
{
    public ActiveSkill ActiveSkill;

    public Weapon(int iD, string name, ElementType type, string info, Sprite sprite, ItemType itemType,int buyPrice,int sellPrice ,EquipmentType equipmentType,
        int power, int hitRate, int defense, int speed, int magic, int avoid, int magicDefense,int vMove, int lucky,int hMove, int critical,int fireResistance, 
        int iceResistance, int windResistance, int thunderResistance,int lightResistance, int darkResistance, ActiveSkill activeSkill) 
        : base(iD, name, type, info, sprite, itemType,buyPrice,sellPrice ,equipmentType, power,hitRate,defense,speed,magic,avoid,magicDefense,vMove,lucky,hMove,critical,
        fireResistance,iceResistance,windResistance,thunderResistance,lightResistance,darkResistance)
    {
        ActiveSkill = activeSkill;
    }
}
