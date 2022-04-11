using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class UseItem : Item
{
    /* public int Power;
     public int HMinRange;
     public int HMaxRange;
     public int VMinRange;
     public int VMaxRange;
     public Func<Cell, bool> CheckTargetFunc;
     public string AnimName;
     public LaunchType LaunchType;
     public RangeType RangeType;
     public string VFXObjName;
     public List<IImpactEffect> Effects;
     public List<Vector2> RangePos;*/

    public BaseActionData BaseActionData;

    public UseItem(int iD, string name, ElementType type, string info, Sprite sprite,ItemType itemType,int buyPrice,
                   int sellprice ,BaseActionData baseActionData) : base(iD, name, type, info ,sprite,itemType,buyPrice,sellprice)
    {
        /*Power = power;
        HMinRange = hMinRange;
        HMaxRange = hMaxRange;
        VMinRange = vMinRange;
        VMaxRange = vMaxRange;
        IsFriend = isFriend;
        AnimName = animName;
        LaunchType = launchType;
        RangeType = rangeType;
        VFXObjName = vfxObjName;
        //EffectTypes = effectTypes;
        Effects = effects;
        RangePos = rangePos;*/
        BaseActionData = baseActionData;
        BaseActionData.ActionType = ActionType.UseItem;
    }

   /* public UseItem(UseItem ori)
    {
        ID = ori.ID;
        Name = ori.Name;
        Type = ori.Type;
        Info = ori.Info;
        Sprite = ori.Sprite;
        ItemType = ori.ItemType;
        Power = ori.Power;
        HMinRange = ori.HMinRange;
        HMaxRange = ori.HMaxRange;
        VRange = ori.VRange;
    }*/
}
