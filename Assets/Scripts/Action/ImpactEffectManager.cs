using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ImpactEffectManager 
{
   public static IImpactEffect GetEffect(EffectType effectType)
    {
        /*switch (effectName)
        {
            case "Damage":
                return new DamageEffect();
            case "UseItemTreat":
                return new UseItemTreat();
            case "SkillTreat":
                return new SkillTreat();
            case "SkillCost":
                return new SkillCost();
            case "UseItemCost":
                return new UseItemCost();
            case "SetBlock":
                return new SetBlockEffect();
            case "MoveTarget":
                return new MoveTarget();
            default:
                return null;
        }*/
        switch (effectType)
        {
            case EffectType.Damage:
                return new DamageEffect();
            case EffectType.Treat:
                return new TreatEffect();
            case EffectType.SkillCost:
                return new SkillCost();
            case EffectType.UseItemCost:
                return new UseItemCost();
            case EffectType.SetBlock:
                return new SetBlockEffect();
            case EffectType.MoveTarget:
                return new MoveTarget();
            case EffectType.AddBuff:
                return new AddBuffEffect();
            default:
                return null;
        }
    }
}
