using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleObj 
{
    public Piece owner;
    public List<Cell> targetCells;
    public List<Piece> targetPieces;
    public LookDirection lookDirection;
    public BaseActionData baseActionData;
    public UnityAction exitAction;
    //public List<IImpactEffect> effects;
    //public List<int> damageList;
    //public List<int> treatList;
    public bool isPassiveSkill = false;
    public Piece theTopTarget;

    public BattleObj(Piece starter, List<Cell> targetCells, List<Piece> targetPieces, BaseActionData baseActionData,LookDirection lookDirection,bool isPassiveSkill)
    {
        owner = starter;
        this.lookDirection = lookDirection;
        this.targetCells = new List<Cell>(targetCells);
        this.targetPieces = new List<Piece>(targetPieces);       
        this.baseActionData = new BaseActionData(baseActionData);
        //effects = new Queue<IImpactEffect>(baseActionData.Effects); 
        //damageList = new List<int>();
        //treatList = new List<int>();
        this.isPassiveSkill = isPassiveSkill;
    }

    public void DoAction()
    {
        if(owner.pieceStatus.CurrentHealth>0)
        {
            if (baseActionData.TargetBaseType == TargetBaseType.Piece && BattleManager.Instance.CheckTargetInRange(this) == 0)
            {
                BattleManager.Instance.CheckBattleChange();
                return;
            }
            GetTopTarget();
            if(isPassiveSkill)
                EventCenter.Instance.EventTrigger<Sprite, string>("ShowPassiveSkill", owner.pieceStatus.pieceSprite, baseActionData.Name);
            BattleManager.Instance.SetExtraAttack();
            EventCenter.Instance.EventTrigger<string>("SetActionName", baseActionData.Name);
            owner.SetIdleDirection(lookDirection);
            //CameraController.Instance.SpecialBattleShot(owner, targetPieces, DoRealAction);
            DoRealAction();
        }
        else
        {
            BattleManager.Instance.CheckBattleChange();
        }
    }

    private void GetTopTarget()
    {
        int tempLevel=0;
        foreach(var piece in targetPieces)
        {
            if (piece.pieceStatus.Level > tempLevel)
                theTopTarget = piece;
        }
    }

    public void AddEffect(IImpactEffect impactEffect)
    {
        
    }

    private void DoRealAction()
    {
        if (baseActionData.TargetBaseType == TargetBaseType.Cell)
            owner.DoPieceAnim(baseActionData.AnimName, CreateVFXObjByCell);
        else
            owner.DoPieceAnim(baseActionData.AnimName, CreateVFXObjByPiece);
    }

    public void CreateVFXObjByCell()
    {
        VFXObjList.Instance.CreateVFXObjByCell(this);
    }

    public void CreateVFXObjByPiece()
    {
         VFXObjList.Instance.CreateVFXObjByPiece(this);
    }

    public void InsertBeforeEffect(EffectType effectType1,EffectType effectType2)
    {
        for (int i = 0; i < baseActionData.EffectNameList.Count; i++)
        {
            if (effectType1==baseActionData.EffectNameList[i])
            {
                baseActionData.EffectNameList.Insert(i, effectType2);
                baseActionData.Effects.Insert(i, ImpactEffectManager.GetEffect(effectType2));
            }
        }
    }

    public void CheckEffect()
    {
        if(baseActionData.Effects.Count>0)
        {
            IImpactEffect temp = baseActionData.Effects[0];
            baseActionData.Effects.RemoveAt(0);
            temp.DoEffect(this);
        }
        else
        {
            BattleManager.Instance.expUpEvents += ExpUpAction;
            VFXObjList.Instance.ExitVFXAction();
        }
    }


    public void ExpUpAction()
    {
        if(owner.pieceStatus.CurrentHealth>0&&owner.CompareTag("Player"))
        {
            ExpTip temp = PoolManager.Instance.GetObj("ExpTip").GetComponent<ExpTip>();
            int exp;
            if (LevelManager.Instance.CurrentPiece == owner)
                exp = ExpCalculation.GetExp(owner, theTopTarget);
            else
                exp = ExpCalculation.GetExtraAttackExp(owner, theTopTarget);
            temp.Init(exp, owner.transform.position);
            int oriLevel = owner.pieceStatus.Level;
            owner.pieceStatus.CurrentExp += exp;
            if (owner.pieceStatus.Level>oriLevel)
            {
                BattleManager.Instance.levelUpPieceList.Add(owner);
                if(owner.pieceStatus.LevelUpSkillList[owner.pieceStatus.Level]>0)
                {
                    BattleManager.Instance.getSkillPieceList.Enqueue(owner);
                }
            }
        }       
    }

    public void CloseThisBattleAction()
    {
        BattleManager.Instance.CheckBattleChange();
    }

}
