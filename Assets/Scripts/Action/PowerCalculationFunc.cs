using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PowerType
{
    SimplePhysics,
    SimpleMagic,
    NoAdditionPower,
    HalfMagic,
    QuarterMagic,
}

public class PowerCalculationFunc 
{
    public static Func<BaseActionData,Piece,int>  GetPowerCalculationFunc(PowerType powerType)
    {
        switch (powerType)
        {
            case PowerType.SimplePhysics:
                return SimplePhysicsPowerCalculationFunc;
            case PowerType.SimpleMagic:
                return SimpleMagicPowerCalculationFunc;
            case PowerType.NoAdditionPower:
                return NoAdditionPowerCalculationFunc;
            case PowerType.HalfMagic:
                return HalfMagicPowerCalculationFunc;
            case PowerType.QuarterMagic:
                return QuarterMagicPowerCalculationFunc;
            default:
                return null;
        }
    }

    public static int SimplePhysicsPowerCalculationFunc(BaseActionData baseActionData,Piece piece)
    {
        return piece.pieceStatus.Power + baseActionData.Power;
    }

    public static int SimpleMagicPowerCalculationFunc(BaseActionData baseActionData,Piece piece)
    {
        return piece.pieceStatus.Magic + baseActionData.Power;
    }

    public static int NoAdditionPowerCalculationFunc(BaseActionData baseActionData,Piece piece)
    {
        return baseActionData.Power;
    }

    public static int HalfMagicPowerCalculationFunc(BaseActionData baseActionData,Piece piece)
    {
        return piece.pieceStatus.Magic / 2 + baseActionData.Power;
    }

    public static int QuarterMagicPowerCalculationFunc(BaseActionData baseActionData,Piece piece)
    {
        return piece.pieceStatus.Magic / 4 + baseActionData.Power;
    }
}
