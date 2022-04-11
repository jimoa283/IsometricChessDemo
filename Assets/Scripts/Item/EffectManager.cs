using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EffectManager 
{

    public static IPassiveEffect GetPassiveSkill(int id)
    {
        switch (id)
        {
            case 3500:
                return new RushAttack();
            case 3501:
                return new CounterAttack();
            case 3502:
                return new ActionTwice();
            case 3503:
                return new ArrowWeakness();
            case 3504:
                return new WeaknessEffect();
            case 3505:
                return new Far_A_Bit_Medichine();
            case 3506:
                return new FireReinforcement();
            case 5001:
                return new BurnBUFFEffect();
            case 5002:
                return new CantMoveBUFFEffect();
            case 5003:
                return new DefenseValueUpBuff();
            case 5004:
                return new MagicDefenseValueUpBuff();
            default:
                return null;
        }

    }

  

}
