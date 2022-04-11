using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public  class DamageCalculationFunc 
{

    public static int DamageCalculationFuncModel(Piece owner,Piece target,BaseActionData baseActionData,
                                                  Func<Piece,Piece,BaseActionData,int> hitCalculationFunc,
                                                  Func<Piece,Piece,BaseActionData,int> damageCalculationFunc,
                                                  Func<Piece,Piece,BaseActionData,int> criticalCalculationFunc,
                                                  Func<Piece,Piece,BaseActionData,int,int> otherEffectDamageCalculationFunc)
    {
        int hitRate = hitCalculationFunc(owner, target, baseActionData);
        int temp1 = RandomNumManager.Instance.GetBattleRandomNum();
        int damage;
        if (temp1<=hitRate)
        {
            damage = damageCalculationFunc(owner, target, baseActionData);
            damage = (int)(damage * target.pieceStatus.damageRate);
            damage = otherEffectDamageCalculationFunc(owner, target, baseActionData, damage);
            damage = owner.PieceTimeProcessor.AttackTimeProcessorCheck(target, damage);
            damage = target.PieceTimeProcessor.BeAttackTimeProcessorCheck(owner, damage);   
            int criticalRate = criticalCalculationFunc(owner, target, baseActionData);
            int temp2 = RandomNumManager.Instance.GetBattleRandomNum();
            if (temp2 <= criticalRate)
                damage = damage * 2 + 10000;
        }
        else
        {
            damage = -1;
        }
        return damage;
    }

    public static int SimulationDamageCaculationFunc(Piece owner, Piece target, BaseActionData baseActionData,
                                                  Func<Piece, Piece, BaseActionData, int> hitCalculationFunc,
                                                  Func<Piece, Piece, BaseActionData, int> damageCalculationFunc,
                                                  Func<Piece, Piece, BaseActionData, int> criticalCalculationFunc,
                                                  Func<Piece, Piece, BaseActionData, int, int> otherEffectDamageCalculationFunc)
    {
        int hitRate= hitCalculationFunc(owner, target, baseActionData);
        int criticalRate = criticalCalculationFunc(owner, target, baseActionData);
        int damage = damageCalculationFunc(owner, target, baseActionData);

        int realDamage = hitRate * (damage + damage * criticalRate);

        realDamage = otherEffectDamageCalculationFunc(owner, target, baseActionData, realDamage);

        return realDamage;
    }

    public static Func<Piece, Piece, BaseActionData,int> GetHitCalculationFunc(string funcName)
    {
        switch (funcName)
        {
            case "Simple":
                return SimpleHitCalculation;
            case "AllHit":
                return AllHitCaculation;
            default:
                return null;
        }
    }

    public static Func<Piece,Piece,BaseActionData,int> GetDamageCalculationFunc(string funcName)
    {
        switch (funcName)
        {
            case "Physics":
                return SimplePhysicsDamageCalculationFunc;
            case "Magic":
                return SimpleMagicDamageCalculation;
            case "Treat":
                return SimpleTreatCalculationFunc;
            default:
                return null;
        }
    }

    public static Func<Piece, Piece, BaseActionData, int> GetCriticalCalculationFunc(string funcName)
    {
        switch (funcName)
        {
            case "Simple":
                return SimpleCriticalCalculation;
            case "NoCritical":
                return NoCriticalCalculation;
            default:
                return null;
        }
    }

    public static Func<Piece, Piece, BaseActionData, int, int> GetOtherEffectDamageCalculationFunc(string funcName)
    {
        switch (funcName)
        {
            case "Simple":
                return SimpleOtherEffectDamageCalculation;
            case "SimpleTreat":
                return SimpleOtherEffectTreatCalculation;
            default:
                return null;
        }
    }

    public static int SimplePhysicsDamageCalculationFunc(Piece owner,Piece target,BaseActionData baseActionData)
    {
        float elementResistance;
        switch (baseActionData.ActionElementType)
        {
            case ActionElementType.Fire:
                elementResistance = target.pieceStatus.FireResistance;
                break;
            case ActionElementType.Ice:
                elementResistance = target.pieceStatus.IceResistance;
                break;
            case ActionElementType.Wind:
                elementResistance = target.pieceStatus.WindResistance;
                break;
            case ActionElementType.Thunder:
                elementResistance = target.pieceStatus.ThunderResistance;
                break;
            case ActionElementType.Light:
                elementResistance = target.pieceStatus.LightResistance;
                break;
            case ActionElementType.Dark:
                elementResistance = target.pieceStatus.DarkResistance;
                break;
            case ActionElementType.Null:
                elementResistance = 0;
                break;
            default:
                elementResistance = 0;
                break;
        }
        int damage = (int)((baseActionData.RealPower(baseActionData,owner)- target.pieceStatus.Defense)*Mathf.Max(0,(1-elementResistance/100)));
        return damage;
    }

    public static int SimpleMagicDamageCalculation(Piece owner,Piece target,BaseActionData baseActionData)
    {
        float elementResistance;
        switch (baseActionData.ActionElementType)
        {
            case ActionElementType.Fire:
                elementResistance = target.pieceStatus.FireResistance;
                break;
            case ActionElementType.Ice:
                elementResistance = target.pieceStatus.IceResistance;
                break;
            case ActionElementType.Wind:
                elementResistance = target.pieceStatus.WindResistance;
                break;
            case ActionElementType.Thunder:
                elementResistance = target.pieceStatus.ThunderResistance;
                break;
            case ActionElementType.Light:
                elementResistance = target.pieceStatus.LightResistance;
                break;
            case ActionElementType.Dark:
                elementResistance = target.pieceStatus.DarkResistance;
                break;
            case ActionElementType.Null:
                elementResistance = 0;
                break;
            default:
                elementResistance = 0;
                break;
        }
        int damage =(int)(baseActionData.RealPower(baseActionData,owner) - target.pieceStatus.MagicDefense * Mathf.Max(0, (1 - elementResistance / 100)));
        return damage;
    }


    public static int SimpleOtherEffectDamageCalculation(Piece owner,Piece target,BaseActionData baseActionData,int damage)
    {
        return damage;
    }

    public static int SimpleHitCalculation(Piece owner,Piece target,BaseActionData baseActionData)
    {
        int hitLimitValue = owner.pieceStatus.HitRate + baseActionData.HitRate - target.pieceStatus.Avoid;
        hitLimitValue = Mathf.Clamp(hitLimitValue, 0, 100);
        return hitLimitValue;
    }

    public static int AllHitCaculation(Piece owner,Piece target,BaseActionData baseActionData)
    {
        return 100;
    }

    public static int SimpleCriticalCalculation(Piece owner,Piece target,BaseActionData baseActionData)
    {
        if(owner.pieceStatus.lookDirection==target.pieceStatus.lookDirection)
        {
            return 100;
        }
        else
        {
            int critical = baseActionData.Critical + owner.pieceStatus.critical + owner.pieceStatus.Lucky / 2;
            return critical;
        }
    }

    public static int PureCriticalCalculation(Piece owner,Piece target,BaseActionData baseActionData)
    {
        int critical= baseActionData.Critical + owner.pieceStatus.critical + owner.pieceStatus.Lucky / 2;
        critical = Mathf.Clamp(critical, 0, 100);
        return critical;
    }


    public static int OnlyBackCriticalCalculation(Piece owner,Piece target,BaseActionData baseActionData)
    {
        if (owner.pieceStatus.lookDirection == target.pieceStatus.lookDirection)
        {
            return 100;
        }

        return -1;
    }

    public static int NoCriticalCalculation(Piece owner, Piece target, BaseActionData baseActionData)
    {
        return -1;
    }

    public static int AllCriticalCalculation(Piece owner,Piece target,BaseActionData baseActionData)
    {
        return 100;
    }


    public static int TreatCalculationFuncModel(Piece owner, Piece target, BaseActionData baseActionData,
                                               Func<Piece, Piece, BaseActionData, int> treatCalculationFunc,
                                               Func<Piece, Piece, BaseActionData, int, int> otherEffectTreatCalcualtionFunc)
    {
        int treat = treatCalculationFunc(owner, target, baseActionData);
        treat = otherEffectTreatCalcualtionFunc(owner, target, baseActionData, treat);

        return treat;
    }

    public static int SimpleTreatCalculationFunc(Piece owner, Piece target, BaseActionData baseActionData)
    {
        int treat = baseActionData.RealPower(baseActionData, owner);
        return treat;
    }

    public static int SimpleOtherEffectTreatCalculation(Piece owner, Piece target, BaseActionData baseActionData, int treat)
    {
        return treat;
    }
}
