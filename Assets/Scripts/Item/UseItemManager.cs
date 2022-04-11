using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class UseItemManager : Singleton<UseItemManager>
{
    private Dictionary<int, UseItem> useItemDic;

    public UseItemManager()
    {
        useItemDic = new Dictionary<int, UseItem>();
        ParseUseItemJson();
    }

    private void ParseUseItemJson()
    {
        TextAsset textUseItem = Resources.Load<TextAsset>("JSON/UseItemJson");
        JSONObject jSONObject = new JSONObject(textUseItem.text);
        foreach (var obj in jSONObject.list)
        {
            int id = (int)obj["ID"].n;
            string _name = obj["Name"].str;
            string _elementType = obj["ElementType"].str;
            ElementType elementType = (ElementType)System.Enum.Parse(typeof(ElementType), _elementType);
            string info = obj["Info"].str;
            string spritePath = obj["SpritePath"].str;
            Sprite sprite = Resources.Load<GameObject>(spritePath).GetComponent<SpriteRenderer>().sprite;

            string _itemType = obj["ItemType"].str;
            ItemType itemType = (ItemType)System.Enum.Parse(typeof(ItemType), _itemType);

            int buyPrice = (int)obj["BuyPrice"].n;
            int sellPrice = (int)obj["SellPrice"].n;

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
            Func<Cell,Piece,bool> checkTargetFunc = CheckTargetFunc.GetCheckFunc(checkTargetType);

            float firstWaitTime = obj["FirstWaitTime"].f;
            float secondWaitTime = obj["SecondWaitTime"].f;

            string animName = obj["AnimName"].str;

            string _targetBaseType = obj["TargetBaseType"].str;
            TargetBaseType targetBaseType = (TargetBaseType)Enum.Parse(typeof(TargetBaseType), _targetBaseType);

            string _launchType = obj["LaunchType"].str;
            LaunchType launchType = (LaunchType)Enum.Parse(typeof(LaunchType), _launchType);

            string _rangeType = obj["RangeType"].str;
            RangeType rangeType = (RangeType)Enum.Parse(typeof(RangeType), _rangeType);
            Func<BaseActionData, Piece, Cell, bool, List<Cell>> getTargetCellsFunc;
            Func<BaseActionData, Piece, Cell, Func<Cell, Piece, bool>, List<Piece>> getTargetPiecesFunc;
            if (rangeType == RangeType.Circle)
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
            if(obj["BuffIDList"]!=null)
            {
                for (int i = 0; i < obj["BuffIDList"].Count; i++)
                {
                    int buffID = (int)obj["BuffIDList"][i].n;
                    buffIDList.Add(buffID);
                }
            }
            

            string _setBattlePieceFunc = obj["SetBattlePieceFunc"].str;
            UnityAction<Piece, List<Piece>, BaseActionData> setBattlePieceFunc = SetBattlPieceFunc.GetSetBattlePieceFunc(_setBattlePieceFunc);

            BaseActionData baseActionData = new BaseActionData(_name, power,hitRate,critical ,realPower,hitCalculationFunc, damageCalculationFunc,
                    criticalCalculationFunc, otherEffectDamageCalculationFunc,actionElementType,attackType,hMinRange, hMaxRange,
                vMinRange, vMaxRange,firstWaitTime,secondWaitTime,animName,targetBaseType ,checkTargetFunc,
                launchType, rangeType,getTargetCellsFunc,getTargetPiecesFunc ,vfxObjName,effectNameList ,effectTypes,
                rangePosUp, rangePosDown, rangePosLeft, rangePosRight, 
                cellAttachmentName,maxMoveNum,buffIDList,setBattlePieceFunc);

            UseItem useItem = new UseItem(id, _name, elementType, info, sprite, itemType,buyPrice,sellPrice,baseActionData);
            useItemDic.Add(id, useItem);

        }   
    }

    public UseItem GetUseItem(int id)
    {
        if (useItemDic.ContainsKey(id))
            return CloneUseItem(useItemDic[id]);
        return null;
    }

    public UseItem GetNumUseItem(int id,int num)
    {
        UseItem item = GetUseItem(id);
        if (item != null)
            item.Number += num;
        return item;
    }

    private UseItem CloneUseItem(UseItem ori)
    {
        return new UseItem(ori.ID, ori.Name, ori.Type, ori.Info, ori.Sprite, ori.ItemType,ori.BuyPrice,ori.SellPrice,ori.BaseActionData);
    }
}
