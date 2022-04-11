using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkillSlot : MonoBehaviour
{
    private Image skillImage;
    private Text skillName;
    private GameObject blackMask;
    public Skill skill;
    private Image bg;

    public void Init()
    {
        skillImage = transform.Find("Image").GetComponent<Image>();
        skillName = transform.Find("Name").GetComponent<Text>();
        blackMask = transform.Find("BlackMask").gameObject;
        bg = GetComponent<Image>();
    }

    public void SetSkillSlot(Skill skill)
    {
        this.skill = skill;
        if (skill.SkillType == SkillType.Active)
            skillImage.gameObject.SetActive(true);
        else
            skillImage.gameObject.SetActive(false);
        skillName.gameObject.SetActive(true);
        skillName.text = skill.Name;
    }

    public void ClearSlot()
    {
        skill = null;
        skillImage.gameObject.SetActive(false);
        skillName.gameObject.SetActive(false);
    }

    public void SelectSlot()
    {
        bg.color = new Color(1, 1, 1, 0.6f);
        skillName.color = Color.black;
        transform.DOLocalMoveX(-3, 0.1f);
    }

    public void CancelSelect()
    {
        bg.color = new Color(0, 0, 0, 0.6f);
        skillName.color = Color.white;
        transform.DOLocalMoveX(0, 0.1f);
    }
}
