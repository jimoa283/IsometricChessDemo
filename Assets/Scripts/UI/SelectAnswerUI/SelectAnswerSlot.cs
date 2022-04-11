using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SelectAnswerSlot : MonoBehaviour
{
    private Text answerText;
    private GameObject arrow;
    private GameObject LockTip;
    private CanvasGroup lockCanvasGroup;
    public bool isLock;

    public void Init()
    {
        answerText = TransformHelper.GetChildTransform(transform, "Answer").GetComponent<Text>();
        arrow = TransformHelper.GetChildTransform(transform, "Arrow").gameObject;
        LockTip = TransformHelper.GetChildTransform(transform, "LockTip").gameObject;
        lockCanvasGroup = LockTip.GetComponent<CanvasGroup>();
    }

    public void SetAnswer(SingleSelectAnswer singleSelectAnswer)
    {
        gameObject.SetActive(true);
        answerText.text = singleSelectAnswer.Answer;
        isLock = singleSelectAnswer.IsLock;
        arrow.SetActive(false);
        if(isLock)
        {
            LockTip.SetActive(true);
            lockCanvasGroup.alpha = 1;
            answerText.gameObject.SetActive(false);
            UnLock(singleSelectAnswer);
        }
        else
        {
            LockTip.SetActive(false);
            answerText.gameObject.SetActive(true);
        }
    }

    IEnumerator DoUnlock()
    {
        lockCanvasGroup.DOFade(0,1);
        yield return new WaitForSeconds(0.8f);
        answerText.gameObject.SetActive(true);
        isLock = false;
    }

    private void UnLock(SingleSelectAnswer singleSelectAnswer)
    {
        if (singleSelectAnswer.UnLockCheck())
            StartCoroutine(DoUnlock());
    }

    public void SelectSlot()
    {
        arrow.SetActive(true);
    }

    public void CancelSelect()
    {
        arrow.gameObject.SetActive(false);
    }

}
