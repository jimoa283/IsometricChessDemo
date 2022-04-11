using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PassiveSkillShowSlot : MonoBehaviour
{
    private Image pieceImage;
    private Text passiveSkillName;
    //private Image bg;
    public bool isActive;
    //private UnityAction checkAction;
    private PassiveSkillShowPanel passiveSkillShowPanel;
    private CanvasGroup canvasGroup;

    public void Init(PassiveSkillShowPanel passiveSkillShowPanel)
    {
        //bg = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        pieceImage = TransformHelper.GetChildTransform(transform, "PieceImage").GetComponent<Image>();
        passiveSkillName = TransformHelper.GetChildTransform(transform, "PassiveSkillName").GetComponent<Text>();
        this.passiveSkillShowPanel = passiveSkillShowPanel;
        ResetSlot();
    }

    public void ShowPassiveSkill(Sprite sprite,string skillName)
    {
        //gameObject.SetActive(true);
        //bg.color = new Color(0, 0, 0, 0.3f);
        canvasGroup.alpha = 1;
        isActive = true;
        pieceImage.sprite = sprite;
        passiveSkillName.text = skillName;
        StartCoroutine(DoShowPassiveSkill());
    }

    public void ResetSlot()
    {
        //bg.color = new Color(0, 0, 0, 0.3f);
        transform.position = new Vector2(transform.position.x+321, transform.position.y);
        isActive = false;
        //pieceImage.color = Color.white;
        //passiveSkillName.color = Color.white;
        //gameObject.SetActive(false);
    }

    IEnumerator DoShowPassiveSkill()
    {
        transform.DOLocalMoveX(0,0.5f);
        yield return new WaitForSeconds(0.55f);
        /*bg.DOFade(0, 3f);
        pieceImage.DOFade(0,1.5f);
        passiveSkillName.DOFade(0,1.5f);*/
        yield return new WaitForSeconds(5f);
        canvasGroup.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.55f);
        ResetSlot();
        passiveSkillShowPanel.CheckSlotActive();
    }
}
