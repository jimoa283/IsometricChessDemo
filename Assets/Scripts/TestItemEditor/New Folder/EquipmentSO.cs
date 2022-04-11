using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Element/New Equipment")]
public class EquipmentSO : ItemSO
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
}
