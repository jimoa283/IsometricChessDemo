using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SimplePieceInformationShow : MonoBehaviour
{
    private Text pieceName;
    private Text pieceLevel;
    private Text pieceHealthValue;
    private Image healthBarR;
    private Image healthBarG;
    private Image healthBarW;
    private GameObject hitIcon;
    private Text hitValue;

    private PieceBuffShowSlot[] pieceBuffShowSlots;

    public void Init()
    {
        pieceName = TransformHelper.GetChildTransform(transform, "Name").GetComponent<Text>();
        pieceLevel = TransformHelper.GetChildTransform(transform, "Level").GetComponent<Text>();
        pieceHealthValue = TransformHelper.GetChildTransform(transform, "HealthValue").GetComponent<Text>();
        healthBarG = TransformHelper.GetChildTransform(transform, "HealthBarG").GetComponent<Image>();
        healthBarR = TransformHelper.GetChildTransform(transform, "HealthBarR").GetComponent<Image>();
        healthBarW = TransformHelper.GetChildTransform(transform, "HealthBarW").GetComponent<Image>();
        hitIcon = TransformHelper.GetChildTransform(transform, "HitIcon").gameObject;
        hitValue = TransformHelper.GetChildTransform(transform, "HitValue").GetComponent<Text>();

        pieceBuffShowSlots = GetComponentsInChildren<PieceBuffShowSlot>();
        foreach (var slot in pieceBuffShowSlots)
        {
            slot.Init();
        }
    }

    public void ShowPieceBUFF(PieceBUFFList pieceBUFFList)
    {
        int i = 0;
        for (i = 0; i < pieceBuffShowSlots.Length; i++)
        {
            if (i == pieceBUFFList.BuffList.Count)
                break;
            pieceBuffShowSlots[i].ShowPieceBuff(pieceBUFFList.BuffList[i]);
        }
        for (; i < pieceBuffShowSlots.Length; i++)
        {
            pieceBuffShowSlots[i].gameObject.SetActive(false);
        }
    }

    public void ShowPieceSimpleInformation(Piece piece)
    {
        gameObject.SetActive(true);
        hitIcon.SetActive(false);
        pieceHealthValue.text = piece.pieceStatus.CurrentHealth.ToString();
        healthBarG.gameObject.SetActive(false);
        healthBarR.gameObject.SetActive(false);
        healthBarW.fillAmount = (float)piece.pieceStatus.CurrentHealth / piece.pieceStatus.maxHealth;
        ShowPieceBUFF(piece.PieceBUFFList);
    }

    public void ShowPieceActionEffectInformation(Piece piece)
    {
        BaseActionData baseActionData = EffectRangeManager.Instance.BaseActionData;
        if(baseActionData.EffectNameList.Contains(EffectType.Damage)&&baseActionData.CheckTargetFunc(piece.currentCell,LevelManager.Instance.CurrentPiece))
        {
            ShowPieceAttackEffect(piece, LevelManager.Instance.CurrentPiece, baseActionData);
        }
        else if(baseActionData.EffectNameList.Contains(EffectType.Treat)&& baseActionData.CheckTargetFunc(piece.currentCell,LevelManager.Instance.CurrentPiece))
        {
            ShowPieceTreatEffect(piece, LevelManager.Instance.CurrentPiece, baseActionData);
        }
        else
        {
            ShowPieceSimpleInformation(piece);
        }
    }

     private  void ShowPieceAttackEffect(Piece piece,Piece owner,BaseActionData baseActionData)
    {
        gameObject.SetActive(true);
        hitIcon.SetActive(true);
        ShowPieceBUFF(piece.PieceBUFFList);
        healthBarG.gameObject.SetActive(false);
        healthBarR.gameObject.SetActive(true);
        hitValue.text = baseActionData.HitCalculationFunc(owner,piece,baseActionData)+"%";
        int damage = DamageCalculationFunc.DamageCalculationFuncModel(owner, piece, baseActionData, baseActionData.HitCalculationFunc,
                                                                  baseActionData.DamageCalculationFunc, DamageCalculationFunc.OnlyBackCriticalCalculation,
                                                                  baseActionData.OtherEffectDamageCalculationFunc);

        if (damage > 10000)
            damage -= 10000;
        float afterHealth = piece.pieceStatus.CurrentHealth - damage;
        if (afterHealth < 0)
            afterHealth = 0;

        if(BattleManager.Instance.extraAttackPiece==null)
        {
            pieceHealthValue.text = piece.pieceStatus.CurrentHealth + "→" + afterHealth;
            healthBarW.fillAmount = afterHealth / piece.pieceStatus.maxHealth;
        }       
        else
        {
            Piece extraPiece = BattleManager.Instance.extraAttackPiece;
            BaseActionData extraBaseActionData = extraPiece.pieceStatus.Weapon.ActiveSkill.BaseActionData;
            int extraDamge = DamageCalculationFunc.DamageCalculationFuncModel(extraPiece, piece, extraBaseActionData,extraBaseActionData.HitCalculationFunc,
                                                                            extraBaseActionData.DamageCalculationFunc, DamageCalculationFunc.OnlyBackCriticalCalculation,
                                                                            baseActionData.OtherEffectDamageCalculationFunc);

            if (extraDamge > 10000)
                extraDamge -= 10000;
            float afterExtraAttackHealth = afterHealth - extraDamge;
            if (afterExtraAttackHealth < 0)
                afterExtraAttackHealth = 0;

            pieceHealthValue.text = piece.pieceStatus.CurrentHealth + "→" + afterHealth + "→" + afterExtraAttackHealth;
            healthBarW.fillAmount = afterExtraAttackHealth / piece.pieceStatus.maxHealth;
        }       
        healthBarR.fillAmount = (float)piece.pieceStatus.CurrentHealth / piece.pieceStatus.maxHealth;
    }

    private void ShowPieceTreatEffect(Piece piece,Piece owner,BaseActionData baseActionData)
    {
        gameObject.SetActive(true);
        hitIcon.SetActive(true);
        ShowPieceBUFF(piece.PieceBUFFList);
        healthBarG.gameObject.SetActive(true);
        healthBarR.gameObject.SetActive(false);

        hitValue.text = "100%";

        int treat = DamageCalculationFunc.TreatCalculationFuncModel(owner, piece, baseActionData, baseActionData.DamageCalculationFunc,
                                                                  baseActionData.OtherEffectDamageCalculationFunc);

        float afterHealth = piece.pieceStatus.CurrentHealth + treat;
        if (afterHealth > piece.pieceStatus.maxHealth)
            afterHealth = piece.pieceStatus.maxHealth;

        pieceHealthValue.text = piece.pieceStatus.CurrentHealth + "→" + afterHealth;

        healthBarW.fillAmount = (float)piece.pieceStatus.CurrentHealth / piece.pieceStatus.maxHealth;
        healthBarG.fillAmount = afterHealth / piece.pieceStatus.maxHealth;
    }
}
