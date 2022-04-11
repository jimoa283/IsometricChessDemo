using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SOSelectAnswerSlot : MonoBehaviour
{
    private Text answerText;
    private GameObject arrow;
    private GameObject LockTip;
    private CanvasGroup lockCanvasGroup;
    public bool hasLock;


    public void Init()
    {
        answerText = TransformHelper.GetChildTransform(transform, "Answer").GetComponent<Text>();
        arrow = TransformHelper.GetChildTransform(transform, "Arrow").gameObject;
        LockTip = TransformHelper.GetChildTransform(transform, "LockTip").gameObject;
        lockCanvasGroup = LockTip.GetComponent<CanvasGroup>();
    }

    public void SetAnswer(ChoiceData choiceData)
    {
        gameObject.SetActive(true);
        answerText.text = choiceData.Text.Value;
        hasLock = choiceData.ConditionChecks.Count > 0;
        arrow.SetActive(false);
        if (hasLock)
        {
            LockTip.SetActive(true);
            lockCanvasGroup.alpha = 1;
            answerText.gameObject.SetActive(false);
            UnLock(choiceData);
        }
        else
        {
            LockTip.SetActive(false);
            answerText.gameObject.SetActive(true);
        }
    }

    private bool CheckIsLock(ChoiceData choiceData)
    {
        bool isLock = false;
        int size = choiceData.ConditionChecks.Count;
        for (int i=0;i<size;++i)
        {
            bool currentConditionCheck= choiceData.ConditionChecks[i].Container_ConditionCheckSO.Value.ChoiceCheck(); 
            if (i!=0)
            {
                if (choiceData.ConditionChecks[i - 1].Container_ConditionAddType.Value == ConditionAddType.And)
                    isLock &= currentConditionCheck;
                else
                    isLock |= currentConditionCheck;
            }
            else
            {
                isLock = currentConditionCheck;
            }
        }
        return isLock;
    }

    IEnumerator DoUnlock()
    {
        lockCanvasGroup.DOFade(0, 1);
        yield return new WaitForSeconds(0.8f);
        answerText.gameObject.SetActive(true);
        hasLock = false;
    }

    private void UnLock(ChoiceData choiceData)
    {
        if (CheckIsLock(choiceData))
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
