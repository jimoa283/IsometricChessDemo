using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ActionSlot : MonoBehaviour
{
    private Image actionImage;
    private Text actionName;
    private Image sr;
    private Text actionNum;
    public Skill skill;
    public UseItem useItem;
    public BaseActionData baseActionData;

    public void Init()
    {
        actionImage = transform.Find("ActionImage").GetComponent<Image>();
        actionName = transform.Find("ActionName").GetComponent<Text>();
        actionNum = transform.Find("ActionNum").GetComponent<Text>();
        sr = GetComponent<Image>();
    }

    public void SetSkillAction(Skill skill)
    {
        if (skill.SkillType == SkillType.Active)
            baseActionData = (skill as ActiveSkill).BaseActionData;
        else
            baseActionData = null;
        useItem = null;
        actionImage.gameObject.SetActive(true);
        actionName.gameObject.SetActive(true);
        this.skill = skill;
        actionImage.sprite = skill.Sprite;
        actionName.text = skill.Name;
        actionNum.text = "";
    }

    public void SetUseItemAction(UseItem useItem)
    {
        baseActionData = useItem.BaseActionData;
        //skill = null;
        actionImage.gameObject.SetActive(true);
        actionName.gameObject.SetActive(true);
        actionNum.gameObject.SetActive(true);
        this.useItem = useItem;
        actionImage.sprite = useItem.Sprite;
        actionName.text = useItem.Name;
        actionNum.text = useItem.Number.ToString();
    }

    public void ResigterAction()
    {
        /*if (skill != null&&skill.SkillType==SkillType.Active)
            EffectRangeManager.Instance.ResigterBaseActionDate((skill as ActiveSkill).BaseActionData);
        else
        {
            EffectRangeManager.Instance.ResigterBaseActionDate(useItem.BaseActionData);
        }*/
        if(baseActionData!=null)
            EffectRangeManager.Instance.ResigterBaseActionDate(baseActionData);
    }

    public void SelectAction()
    {
        sr.color = new Color(1, 1, 1, 0.4f);
        actionName.color = Color.black;
        transform.DOLocalMoveX(-7, 0.05f);
        Piece piece = LevelManager.Instance.CurrentPiece;

        if(baseActionData!=null)
        {
            /*  if (baseActionData.RangeType == RangeType.Circle)
                  RangeManager.Instance.ShowAttackRangeByCircle(baseActionData, piece.floorIndex, Select.Instance.currentCell);
              else
                  RangeManager.Instance.ShowAttackRangeByLine(baseActionData, piece.floorIndex, LevelManager.Instance.CurrentPiece.currentCell);*/
            RangeManager.Instance.ShowPieceAttackRange(baseActionData, piece, piece.currentCell);

            int hMaxRange= LevelManager.Instance.CurrentPiece.PieceTimeProcessor.ShowActionTimeProcessorCheck(baseActionData, baseActionData.HMaxRange);
            baseActionData.SetBattlePieceFunc?.Invoke(piece, baseActionData.GetTargetPiecesFunc(baseActionData,piece, piece.currentCell, baseActionData.CheckTargetFunc),baseActionData);
        }
       else
        {
            RangeManager.Instance.CloseAttackRange();
        }      
    }

           

   public void ResetSlot()
    {
        sr.color = new Color(0, 0, 0, 0.4f);
        actionName.color = Color.white;
        transform.DOLocalMoveX(0, 0.05f);
        BattleManager.Instance.CancelExtraAttackPiece();
    }

    public void ClearSlot()
    {
        skill = null;
        useItem = null;
        actionImage.gameObject.SetActive(false);
        actionName.gameObject.SetActive(false);
        actionNum.gameObject.SetActive(false);
    }
}
