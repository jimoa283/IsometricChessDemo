using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

public class GetSkillUI: MonoBehaviour
{
    private Text getSkillText;
    private Image bg;

    public void Init()
    {
        getSkillText = TransformHelper.GetChildTransform(transform, "GetSkillText").GetComponent<Text>();
        bg = TransformHelper.GetChildTransform(transform, "GetSkillWindow").GetComponent<Image>();
        EventCenter.Instance.AddEventListener<string, string>("ShowPieceGetSkill", ShowPieceGetSkill);
        gameObject.SetActive(false);
    }

    public void ShowPieceGetSkill(string pieceName,string skillName)
    {
        gameObject.SetActive(true);
        getSkillText.text = pieceName + " 已学会 “" + skillName + "”";
        StartCoroutine(DoShowPieceGetSkill());
    }

    IEnumerator DoShowPieceGetSkill()
    {
        bg.transform.DOScaleX(1, 0.3f);
        bg.DOFade(0.6f, 0.3f);
        getSkillText.DOFade(1, 0.3f);
        yield return new WaitForSeconds(1);
        bg.transform.DOScaleX(0.7f, 0.3f);
        bg.DOFade(0, 0.3f);
        getSkillText.DOFade(0, 0.3f);
        yield return new WaitForSeconds(0.5f);
        //action?.Invoke();
        BattleManager.Instance.GetSkillCheck();
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEvent<string, string>("ShowPieceGetSkill", ShowPieceGetSkill);
    }
}
