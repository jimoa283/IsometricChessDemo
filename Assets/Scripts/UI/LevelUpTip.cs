using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelUpTip : MonoBehaviour
{
    private Text getNewSkillText;
    private Text levelUpText;

    private void Init()
    {
        getNewSkillText = TransformHelper.GetChildTransform(transform, "GetNewSkillText").GetComponent<Text>();
        levelUpText = TransformHelper.GetChildTransform(transform, "LevelUpText").GetComponent<Text>();
    }

    public void ShowLevelUpTip(Vector2 pos,bool isGetSkill)
    {
        Init();
        transform.position = pos;
        if(isGetSkill)
           getNewSkillText.color = new Color(1, 1, 1, 1);
        levelUpText.color = new Color(1, 1, 1, 1);
        StartCoroutine(DoShowLevelUpTip(isGetSkill));
    }

    IEnumerator DoShowLevelUpTip(bool isGetSkill)
    {
        //levelUpText.transform.DOPunchScale(new Vector3(1.1f, 1.1f), 0.2f, 1, 0);
        levelUpText.transform.DOScale(1.05f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        levelUpText.transform.DOScale(1, 0.2f);
        yield return new WaitForSeconds(0.2f);
        if (isGetSkill)
            getNewSkillText.DOFade(0, 0.2f);

        levelUpText.DOFade(0, 0.2f);
    }
}
