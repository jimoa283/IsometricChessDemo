using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class BattleVictoryUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private UnityAction action;

   public void Init(UnityAction action)
    {
        canvasGroup = GetComponent<CanvasGroup>();
        this.action = action;
    }

    public void ShowBattleVictory()
    {
        gameObject.SetActive(true);
        StartCoroutine(DoShowBattleVictory());
    }

    IEnumerator DoShowBattleVictory()
    {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 3f);
        yield return new WaitForSeconds(3f);
        canvasGroup.DOFade(0, 1f);
        yield return new WaitForSeconds(1.05f);
        gameObject.SetActive(false);
    }
}
