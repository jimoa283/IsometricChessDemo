using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public enum TargetBaseType
{
    Piece,
    Cell
}

public enum ActionType
{
    Skill,
    UseItem
}

public enum ActionElementType
{
    Fire,
    Ice,
    Wind,
    Thunder,
    Light,
    Dark,
    Null
}

public enum AttackType
{
    Hit,
    Slash,
    Arrow,
    Magic,
    Other
}

[Serializable]
public class BaseActionData 
{
    public string Name;

    public ActionType ActionType;

    public Piece owner;

    public Func<BaseActionData,Piece,int> RealPower;

    public int Power;
    public int HitRate;
    public int Critical;

    public Func<Piece, Piece, BaseActionData,int> HitCalculationFunc;
    public Func<Piece, Piece, BaseActionData, int> DamageCalculationFunc;
    public Func<Piece, Piece, BaseActionData, int> CriticalCalculationFunc;
    public Func<Piece, Piece, BaseActionData, int, int> OtherEffectDamageCalculationFunc;

    public ActionElementType ActionElementType;
    public AttackType AttackType;

    public int HMinRange;
    public int HMaxRange;
    public int VMinRange;
    public int VMaxRange;
    public float FirstWaitTime;
    public float SecondWaitTime;
    public string AnimName;
    public TargetBaseType TargetBaseType;
    public Func<Cell,Piece, bool> CheckTargetFunc;
    public LaunchType LaunchType;
    public RangeType RangeType;
    public Func<BaseActionData,Piece,Cell,bool, List<Cell>> GetTargetCellsFunc;
    public Func<BaseActionData,Piece, Cell, Func<Cell, Piece, bool>,List<Piece>> GetTargetPiecesFunc;
    public string VFXObjName;

    public List<EffectType> EffectNameList;
    public List<IImpactEffect> Effects;

    public List<Vector2> RangePosUp;
    public List<Vector2> RangePosDown;
    public List<Vector2> RangePosRight;
    public List<Vector2> RangePosLeft;

    public string CellAttachmentName;
    public int MaxMoveNum;
    public List<int> BuffIDList;
    public UnityAction<Piece,List<Piece>,BaseActionData> SetBattlePieceFunc;

    public BaseActionData(string name,int power,int hitRate,int critical,Func<BaseActionData, Piece, int> realPower,Func<Piece, Piece, BaseActionData, int> hitCalculationFunc,
                        Func<Piece, Piece, BaseActionData, int> damageCalculationFunc,Func<Piece, Piece, BaseActionData, int> criticalCalculationFunc,
                        Func<Piece, Piece, BaseActionData, int, int> otherEffectDamageCalculationFunc,ActionElementType actionElementType,AttackType attackType,int hMinRange, int hMaxRange, int vMinRange, 
                        int vMaxRange,float firstWaitTime ,float secondWaitTime,string animName, TargetBaseType targetBaseType,Func<Cell,Piece ,bool> checkTargetFunc, 
                        LaunchType launchType, RangeType rangeType, Func<BaseActionData, Piece, Cell, bool, List<Cell>> getTargetCellsFunc,
                        Func<BaseActionData, Piece, Cell, Func<Cell, Piece, bool>, List<Piece>> getTargetPiecesFunc,string vFXObjName,
                        List<EffectType> effectNameList ,List<IImpactEffect> effects, List<Vector2> rangePosUp, List<Vector2> rangePosDown,
                        List<Vector2> rangePosLeft, List<Vector2> rangePosRight,string cellAttachmentName,
                        int maxMoveNum,List<int> buffIDList,UnityAction<Piece, List<Piece>, BaseActionData> setBattlePieceFunc)
    {
        Name = name;
        Power = power;
        HitRate = hitRate;
        Critical = critical;

        RealPower = realPower;

        HitCalculationFunc = hitCalculationFunc;
        DamageCalculationFunc = damageCalculationFunc;
        CriticalCalculationFunc = criticalCalculationFunc;
        OtherEffectDamageCalculationFunc = otherEffectDamageCalculationFunc;

        ActionElementType = actionElementType;
        AttackType = attackType;

        HMinRange = hMinRange;
        HMaxRange = hMaxRange;
        VMinRange = vMinRange;
        VMaxRange = vMaxRange;
        FirstWaitTime = firstWaitTime;
        SecondWaitTime = secondWaitTime;
        AnimName = animName;
        TargetBaseType = targetBaseType;
        CheckTargetFunc = checkTargetFunc;
        LaunchType = launchType;

        RangeType = rangeType;
        GetTargetCellsFunc = getTargetCellsFunc;
        GetTargetPiecesFunc = getTargetPiecesFunc;

        VFXObjName = vFXObjName;

        EffectNameList = effectNameList;
        Effects = effects;

        RangePosUp = rangePosUp;
        RangePosDown = rangePosDown;
        RangePosLeft = rangePosLeft;
        RangePosRight = rangePosRight;
        CellAttachmentName = cellAttachmentName;
        MaxMoveNum = maxMoveNum;
        BuffIDList = buffIDList;
        SetBattlePieceFunc = setBattlePieceFunc;
    }

    public BaseActionData(BaseActionData baseActionData)
    {
        Name = baseActionData.Name;
        Power = baseActionData.Power;
        HitRate = baseActionData.HitRate;
        Critical = baseActionData.Critical;

        RealPower = baseActionData.RealPower;

        HitCalculationFunc = baseActionData.HitCalculationFunc;
        DamageCalculationFunc = baseActionData.DamageCalculationFunc;
        CriticalCalculationFunc = baseActionData.CriticalCalculationFunc;
        OtherEffectDamageCalculationFunc= baseActionData.OtherEffectDamageCalculationFunc;

        ActionElementType = baseActionData.ActionElementType;
        AttackType = baseActionData.AttackType;

        HMinRange = baseActionData.HMinRange;
        HMaxRange = baseActionData.HMaxRange;
        VMinRange = baseActionData.VMinRange;
        VMaxRange = baseActionData.VMaxRange;
        FirstWaitTime = baseActionData.FirstWaitTime;
        SecondWaitTime = baseActionData.SecondWaitTime;
        AnimName = baseActionData.AnimName;
        TargetBaseType = baseActionData.TargetBaseType;
        CheckTargetFunc = baseActionData.CheckTargetFunc;

        LaunchType = baseActionData.LaunchType;
        RangeType = baseActionData.RangeType;
        GetTargetCellsFunc = baseActionData.GetTargetCellsFunc;
        GetTargetPiecesFunc = baseActionData.GetTargetPiecesFunc;

        VFXObjName = baseActionData.VFXObjName;

        EffectNameList = new List<EffectType>(baseActionData.EffectNameList);
        Effects = new List<IImpactEffect>( baseActionData.Effects);
        RangePosUp = new List<Vector2>(baseActionData.RangePosUp);
        RangePosDown = new List<Vector2>( baseActionData.RangePosDown);
        RangePosLeft = new List<Vector2>( baseActionData.RangePosLeft);
        RangePosRight = new List<Vector2>( baseActionData.RangePosRight);
        CellAttachmentName =baseActionData.CellAttachmentName;
        MaxMoveNum = baseActionData.MaxMoveNum;
        BuffIDList = baseActionData.BuffIDList;
        SetBattlePieceFunc = baseActionData.SetBattlePieceFunc;
    }
}
