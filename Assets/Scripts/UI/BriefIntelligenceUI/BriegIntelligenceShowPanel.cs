using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BriegIntelligenceShowPanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private RectTransform rt;
    private Text briefIntelligenceText;

    public void Init()
    {
        rt = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        briefIntelligenceText = TransformHelper.GetChildTransform(transform, "BriefIntelligenceText").GetComponent<Text>();  
        gameObject.SetActive(false);
    }

    public void ShowBriefIntelligence(BriefIntelligence briefIntelligence)
    {
        gameObject.SetActive(true);
        if(!briefIntelligence.HasGet)
        {
            briefIntelligenceText.text = "取得了" + briefIntelligence.Name + "\n的情报";
            briefIntelligence.HasGet = true;
        }
        else
        {
            briefIntelligenceText.text = "因取得" +briefIntelligence.Name+"\n的情报，选项解锁了";
        }

        rt.DOLocalMoveX(265, 0.2f);
        canvasGroup.alpha = 1;
        canvasGroup.DOFade(1, 0.2f);
        StartCoroutine(DoHide());
    }

    IEnumerator DoHide()
    {
        yield return new WaitForSeconds(5f);
        rt.DOLocalMoveX(540, 0.2f);
        canvasGroup.DOFade(0, 0.2f);
        yield return new WaitForSeconds(0.25f);
        gameObject.SetActive(false);
    }

   
}
