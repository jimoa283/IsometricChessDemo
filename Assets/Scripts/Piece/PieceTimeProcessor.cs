using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PieceTimeProcessor : MonoBehaviour
{
    private Piece piece;
    /*private List<UnityAction<Piece>> turnStartProcessor;
    private List<UnityAction<Piece>> moveProcessor;
    private List<UnityAction<Piece>> afterActionProcessor;
    private List<UnityAction<Piece>> beAttackProcessor;
    private List<UnityAction<Piece>> afterBeAttackProcessor;
    private List<UnityAction<Piece>> attackProcessor;
    private List<UnityAction<Piece>> turnEndProcessor;*/

    private List<AMPassiveEffect> alwaysProcessor = new List<AMPassiveEffect>(); 
    private List<IPassiveEffect> turnStartProcessor = new List<IPassiveEffect>();
    private List<IPassiveEffect> moveProcessor = new List<IPassiveEffect>();
    private List<IPassiveEffect> afterActionProcessor = new List<IPassiveEffect>();
    private List<DamagePassiveEffect> beAttackProcessor = new List<DamagePassiveEffect>();
    private List<IPassiveEffect> afterBeAttackProcessor = new List<IPassiveEffect>();
    private List<DamagePassiveEffect> attackProcessor = new List<DamagePassiveEffect>();
    private List<IPassiveEffect> turnEndProcessor = new List<IPassiveEffect>();
    private List<ActionRangePassiveEffect> showActionProcessor = new List<ActionRangePassiveEffect>();
    private List<IPassiveEffect> selectActionProcessor = new List<IPassiveEffect>();

    public void Init(Piece piece)
    {
        this.piece = piece;
        /* moveProcessor = new List<UnityAction<Piece>>();
         turnStartProcessor = new List<UnityAction<Piece>>();
         afterActionProcessor = new List<UnityAction<Piece>>();
         beAttackProcessor = new List<UnityAction<Piece>>();
         afterBeAttackProcessor = new List<UnityAction<Piece>>();
         attackProcessor = new List<UnityAction<Piece>>();
         turnEndProcessor = new List<UnityAction<Piece>>();*/

        /*moveProcessor = new List<IPassiveEffect>();
        turnStartProcessor = new List<IPassiveEffect>();
        afterActionProcessor = new List<IPassiveEffect>();
        beAttackProcessor = new List<IPassiveEffect>();
        afterBeAttackProcessor = new List<IPassiveEffect>();
        attackProcessor = new List<IPassiveEffect>();
        turnEndProcessor = new List<IPassiveEffect>();*/

        moveProcessor.Clear();
        turnStartProcessor.Clear();
        afterActionProcessor.Clear();
        beAttackProcessor.Clear();
        afterActionProcessor.Clear();
        attackProcessor.Clear();
        turnEndProcessor.Clear();
        showActionProcessor.Clear();
    }

    public void AddTimeProcessorAction(TriggerTimeType triggerTimeType,IPassiveEffect effect)
    {
        switch (triggerTimeType)
        {
            case TriggerTimeType.Always:
                AMPassiveEffect temp = effect as AMPassiveEffect;
                temp.AddEffect(piece);
                alwaysProcessor.Add(temp);
                break;
            case TriggerTimeType.TurnStart:
                turnStartProcessor.Add(effect);              
                break;
            case TriggerTimeType.Move:
                moveProcessor.Add(effect);
                break;
            case TriggerTimeType.AfterAction:
                afterActionProcessor.Add(effect);
                break;
            case TriggerTimeType.BeAttack:
                beAttackProcessor.Add(effect as DamagePassiveEffect);
                break;
            case TriggerTimeType.AfterBeAttack:
                afterBeAttackProcessor.Add(effect);
                break;
            case TriggerTimeType.Attack:
                attackProcessor.Add(effect as DamagePassiveEffect);
                break;
            case TriggerTimeType.TurnEnd:
                break;
            case TriggerTimeType.ShowAction:
                showActionProcessor.Add(effect as ActionRangePassiveEffect);
                break;
            case TriggerTimeType.SelectAction:
                selectActionProcessor.Add(effect);
                break;
            default:
                break;
        }
    }

    public void RemoveTimeProcessorAction(TriggerTimeType triggerTimeType,IPassiveEffect passiveEffect)
    {
        switch (triggerTimeType)
        {
            case TriggerTimeType.Always:
                AMPassiveEffect temp = passiveEffect as AMPassiveEffect;
                temp.RemoveEffect(piece);
                alwaysProcessor.Remove(temp);
                break;
            case TriggerTimeType.TurnStart:
                turnStartProcessor.Remove(passiveEffect);
                break;
            case TriggerTimeType.Move:
                moveProcessor.Remove(passiveEffect);
                break;
            case TriggerTimeType.AfterAction:
                afterActionProcessor.Remove(passiveEffect);
                break;
            case TriggerTimeType.BeAttack:
                beAttackProcessor.Remove(passiveEffect as DamagePassiveEffect);
                break;
            case TriggerTimeType.AfterBeAttack:
                afterBeAttackProcessor.Remove(passiveEffect);
                break;
            case TriggerTimeType.Attack:
                attackProcessor.Remove(passiveEffect as DamagePassiveEffect);
                break;
            case TriggerTimeType.TurnEnd:
                break;
            case TriggerTimeType.ShowAction:
                showActionProcessor.Remove(passiveEffect as ActionRangePassiveEffect);
                break;
            case TriggerTimeType.SelectAction:
                selectActionProcessor.Remove(passiveEffect);
                break;
            default:
                break;
        }
    }

    public void TurnStartTimeProcessorCheck(UnityAction action=null)
    {
        StartCoroutine(DoProcessorAction(turnStartProcessor, action));
    }

    public int AttackTimeProcessorCheck(Piece target,int damage)
    {
        return DoDamageProcessorAction(attackProcessor, damage, target);
    }

    public int BeAttackTimeProcessorCheck(Piece target,int damage)
    {
        return DoDamageProcessorAction(beAttackProcessor, damage, target);
    }

    public void AfterActionTimePricessorCheck(UnityAction action=null)
    {
        StartCoroutine(DoProcessorAction(afterActionProcessor, action));
    }

    public void MovedTimeProcessorCheck(UnityAction action=null)
    {
        StartCoroutine(DoProcessorAction(moveProcessor, action));
    }

    public void AfterBeAttackTimeProcessorCheck(UnityAction action=null)
    {
        StartCoroutine(DoProcessorAction(afterBeAttackProcessor, action));
    }

    public int ShowActionTimeProcessorCheck(BaseActionData baseActionData,int range)
    {
        return ActionRangeProcessorAction(showActionProcessor, baseActionData, range);
    }

    public void SelectActionTimeProcessorCheck(UnityAction action=null)
    {
       StartCoroutine(DoProcessorAction(selectActionProcessor, action));
    }

    IEnumerator DoProcessorAction(List<IPassiveEffect> list,UnityAction unityAction=null)
    {
        foreach (var effect in list)
        {
            
            effect.DoEffect(piece);
            yield return new WaitForSeconds(0.1f);
        }

        unityAction?.Invoke();

    }

    private void DoProcessorActionNoWait(List<IPassiveEffect> list,UnityAction action=null)
    {
        foreach(var effect in list)
        {
            effect.DoEffect(piece);
        }

        action?.Invoke();
    }

    private int DoDamageProcessorAction(List<DamagePassiveEffect> list, int damage,Piece target)
    {
        foreach(var effect in list)
        {
            damage = effect.DoDamageEffect(damage,piece, target);
        }

        return damage;
    }

    private int ActionRangeProcessorAction(List<ActionRangePassiveEffect> list,BaseActionData baseActionData,int range)
    {
        foreach(var effect in list)
        {
            range = effect.ActionRangeChangeEffect(baseActionData, range);
        }

        return range;
    }
}
