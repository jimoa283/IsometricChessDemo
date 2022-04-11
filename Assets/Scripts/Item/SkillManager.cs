using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class SkillManager : Singleton<SkillManager>
{
    private Dictionary<int, ActiveSkill> activeSkillDic;
    private Dictionary<int, PassiveSkill> passiveSkillDic;

    public SkillManager()
    {
        activeSkillDic = new Dictionary<int, ActiveSkill>();
        passiveSkillDic = new Dictionary<int, PassiveSkill>();
        ParseSkillJson();
    }

    private void ParseSkillJson()
    {
        TextAsset textSkill = Resources.Load<TextAsset>("JSON/SkillJSON");
        JSONObject jSONObject = new JSONObject(textSkill.text);
        foreach (var obj in jSONObject.list)
        {
            int id = (int)obj["ID"].n;
            string _name = obj["Name"].str;
            string _elementType = obj["ElementType"].str;
            ElementType elementType = (ElementType)Enum.Parse(typeof(ElementType), _elementType);
            string info = obj["Info"].str;
            string spritePath = obj["SpritePath"].str;
            Sprite sprite = Resources.Load<GameObject>(spritePath).GetComponent<SpriteRenderer>().sprite;

            string _skillType = obj["SkillType"].str;
            SkillType skillType = (SkillType)Enum.Parse(typeof(SkillType), _skillType);

            if(skillType==SkillType.Active)
            {
                int power = (int)obj["Power"].n;
                int hitRate = (int)obj["HitRate"].n;
                int critical = (int)obj["Critical"].n;

                string powerTypeName = obj["PowerType"].str;
                PowerType powerType = (PowerType)Enum.Parse(typeof(PowerType), powerTypeName);
                Func<BaseActionData, Piece, int> realPower = PowerCalculationFunc.GetPowerCalculationFunc(powerType);

                string hitFuncName = obj["HitCalculationFuncName"].str;
                Func<Piece, Piece, BaseActionData, int> hitCalculationFunc = DamageCalculationFunc.GetHitCalculationFunc(hitFuncName);

                string damageFuncName = obj["DamageCalculationFuncName"].str;
                Func<Piece, Piece, BaseActionData, int> damageCalculationFunc = DamageCalculationFunc.GetDamageCalculationFunc(damageFuncName);

                string criticalFuncName = obj["CriticalCalculationFuncName"].str;
                Func<Piece, Piece, BaseActionData, int> criticalCalculationFunc = DamageCalculationFunc.GetCriticalCalculationFunc(criticalFuncName);

                string otherEffectFuncName = obj["OtherEffectDamageCalculationFuncName"].str;
                Func<Piece, Piece, BaseActionData, int, int> otherEffectDamageCalculationFunc = DamageCalculationFunc.GetOtherEffectDamageCalculationFunc(otherEffectFuncName);

                ActionElementType actionElementType = (ActionElementType)Enum.Parse(typeof(ActionElementType), obj["ActionElementType"].str);
                AttackType attackType = (AttackType)Enum.Parse(typeof(AttackType), obj["AttackType"].str);

                int hMinRange = (int)obj["HMinRange"].n;
                int hMaxRange = (int)obj["HMaxRange"].n;
                int vMinRange = (int)obj["VMinRange"].n;
                int vMaxRange = (int)obj["VMaxRange"].n;

                string checkTargetType = obj["CheckTargetFunc"].str;
                Func<Cell,Piece, bool> checkTargetFunc = CheckTargetFunc.GetCheckFunc(checkTargetType);

                float firstWaitTime = obj["FirstWaitTime"].f;
                float secondWaitTime = obj["SecondWaitTime"].f;

                string animName = obj["AnimName"].str;

                string _targetBaseType = obj["TargetBaseType"].str;
                TargetBaseType targetBaseType =(TargetBaseType)Enum.Parse(typeof(TargetBaseType), _targetBaseType);

                string _launchType = obj["LaunchType"].str;
                LaunchType launchType = (LaunchType)Enum.Parse(typeof(LaunchType), _launchType);

                string _rangeType = obj["RangeType"].str;
                RangeType rangeType = (RangeType)Enum.Parse(typeof(RangeType), _rangeType);
                Func<BaseActionData, Piece, Cell, bool, List<Cell>> getTargetCellsFunc;
                Func<BaseActionData, Piece, Cell, Func<Cell, Piece, bool>, List<Piece>> getTargetPiecesFunc;
                if (rangeType==RangeType.Circle)
                {
                    getTargetCellsFunc = CellManager.Instance.GetAttackRangeByCircle;
                    getTargetPiecesFunc = CellManager.Instance.GetTargetsByCircle;
                }
                else
                {
                    getTargetCellsFunc = CellManager.Instance.GetAttackRangeByLine;
                    getTargetPiecesFunc = CellManager.Instance.GetTargetsByLine;
                }

                string vfxObjName = obj["VFXObjName"].str;

                List<IImpactEffect> effectTypes = new List<IImpactEffect>();
                List<EffectType> effectNameList = new List<EffectType>();
                for (int i = 0; i < obj["EffectType"].Count; i++)
                {
                    string temp = obj["EffectType"][i].str;
                    EffectType effectType = (EffectType)Enum.Parse(typeof(EffectType), temp);
                    effectNameList.Add(effectType);
                    IImpactEffect effect = ImpactEffectManager.GetEffect(effectType);
                    effectTypes.Add(effect);
                }

                List<Vector2> rangePosUp = new List<Vector2>();
                for (int i = 0; i < obj["RangePosUp"].Count; i = i + 2)
                {
                    float row = obj["RangePosUp"][i].n;
                    float line = obj["RangePosUp"][i + 1].n;
                    rangePosUp.Add(new Vector2(row, line));
                }

                List<Vector2> rangePosDown = new List<Vector2>();
                for (int i = 0; i < obj["RangePosDown"].Count; i = i + 2)
                {
                    float row = obj["RangePosDown"][i].n;
                    float line = obj["RangePosDown"][i + 1].n;
                    rangePosDown.Add(new Vector2(row, line));
                }

                List<Vector2> rangePosLeft = new List<Vector2>();
                for (int i = 0; i < obj["RangePosLeft"].Count; i = i + 2)
                {
                    float row = obj["RangePosLeft"][i].n;
                    float line = obj["RangePosLeft"][i + 1].n;
                    rangePosLeft.Add(new Vector2(row, line));
                }

                List<Vector2> rangePosRight = new List<Vector2>();
                for (int i = 0; i < obj["RangePosRight"].Count; i = i + 2)
                {
                    float row = obj["RangePosRight"][i].n;
                    float line = obj["RangePosRight"][i + 1].n;
                    rangePosRight.Add(new Vector2(row, line));
                }

                string cellAttachmentName = obj["CellAttachmentName"].str;

                int maxMoveNum = (int)obj["MaxMoveNum"].n;

                List<int> buffIDList = new List<int>();
                if (obj["BuffIDList"] != null)
                {
                    for (int i = 0; i < obj["BuffIDList"].Count; i++)
                    {
                        int buffID = (int)obj["BuffIDList"][i].n;
                        buffIDList.Add(buffID);
                    }
                }

                string _setBattlePieceFunc = obj["SetBattlePieceFunc"].str;
                UnityAction<Piece, List<Piece>, BaseActionData> setBattlePieceFunc = SetBattlPieceFunc.GetSetBattlePieceFunc(_setBattlePieceFunc);

                BaseActionData baseActionData = new BaseActionData(_name, power,hitRate,critical ,realPower,
                    hitCalculationFunc,damageCalculationFunc,criticalCalculationFunc,otherEffectDamageCalculationFunc,
                    actionElementType,attackType,hMinRange, hMaxRange,
                    vMinRange, vMaxRange,firstWaitTime,secondWaitTime,animName, targetBaseType,checkTargetFunc,
                    launchType, rangeType,getTargetCellsFunc,getTargetPiecesFunc, vfxObjName,effectNameList ,
                    effectTypes, rangePosUp,rangePosDown,rangePosLeft,rangePosRight,cellAttachmentName,
                    maxMoveNum,buffIDList,setBattlePieceFunc);

                ActiveSkill activeSkill = new ActiveSkill(id, _name, elementType, info, sprite, skillType, baseActionData);
                activeSkillDic.Add(id, activeSkill);
            }
          
            else
            {
                List<TriggerTimeType> triggerTimeTypes = new List<TriggerTimeType>();
                for (int i = 0; i < obj["TriggerTimeTypeList"].Count; i++)
                {
                    string temp = obj["TriggerTimeTypeList"][i].str;
                    TriggerTimeType triggerTimeType = (TriggerTimeType)Enum.Parse(typeof(TriggerTimeType), temp);
                    triggerTimeTypes.Add(triggerTimeType);
                }

                IPassiveEffect passiveSkillEffect = EffectManager.GetPassiveSkill(id);

                PassiveSkill passiveSkill = new PassiveSkill(id, _name, elementType, info, sprite, skillType,triggerTimeTypes,passiveSkillEffect);
                passiveSkillDic.Add(id, passiveSkill);
            }
            
        }
    }

    public void GetSkill(int id,Piece piece)
    {
        if(activeSkillDic.ContainsKey(id))
        {
           ActiveSkill activeSkill=  GetActiveSkill(id);
           piece.pieceStatus.skillList.Add(activeSkill);
            activeSkill.SetPiece(piece);
        }
        else if(passiveSkillDic.ContainsKey(id))
        {
            GetPassiveSkill(id, piece);
        }
    }

    public ActiveSkill GetActiveSkill(int id)
    {
        if (activeSkillDic.ContainsKey(id))
        {
            return CloneActiveSkill(activeSkillDic[id]);
        }
          

        return null;
   }

       public void GetPassiveSkill(int id,Piece piece)
    {
        PassiveSkill temp = ClonePassiveSkill(passiveSkillDic[id]);
        piece.pieceStatus.skillList.Add(temp);
        for (int i = 0; i < temp.TriggerTimeTypeList.Count; i++)
        {
            piece.PieceTimeProcessor.AddTimeProcessorAction(temp.TriggerTimeTypeList[i], temp.PassiveSkillEffect);
        }
    }

    private ActiveSkill CloneActiveSkill(ActiveSkill ori)
    {
        return new ActiveSkill(ori.ID, ori.Name, ori.Type, ori.Info, ori.Sprite, ori.SkillType,new BaseActionData(ori.BaseActionData));
    }

    private PassiveSkill ClonePassiveSkill(PassiveSkill ori)
    {
        return new PassiveSkill(ori.ID, ori.Name, ori.Type, ori.Info, ori.Sprite, ori.SkillType,ori.TriggerTimeTypeList,ori.PassiveSkillEffect);
    }

   /* public BaseActionData GetExtraAttack(BaseActionData data)
    {
        return new BaseActionData(data.Name, data.Power/2, data.HMinRange, data.HMaxRange, data.VMinRange, data.VMaxRange, data.IsNeedBattleCamera,
                             data.FirstWaitTime, data.SecondWaitTime, data.AnimName, data.TargetBaseType, data.)
    }*/
}
