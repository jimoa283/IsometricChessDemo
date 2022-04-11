using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;

public class SOSelectAnswerUI : MonoBehaviour
{
    private SOSelectAnswerSlot[] selectAnswerSlots;
    private int answerIndex;
    private int limitIndex;
    private List<ChoiceData> choiceDatas;
    private UnityAction<ChoiceData> action;
    private Text speakerNameText;

    public int AnswerIndex
    {
        get => answerIndex;
        set
        {
            selectAnswerSlots[answerIndex].CancelSelect();
            answerIndex = (value + limitIndex) % limitIndex;
            selectAnswerSlots[answerIndex].SelectSlot();
        }
    }

    public void Init()
    {
        speakerNameText = TransformHelper.GetChildTransform(transform, "SpeakerNameText").GetComponent<Text>();
        selectAnswerSlots = GetComponentsInChildren<SOSelectAnswerSlot>();
        foreach (var slot in selectAnswerSlots)
        {
            slot.Init();
        }
        answerIndex = 0;
        limitIndex = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            --AnswerIndex;

        if (Input.GetKeyDown(KeyCode.S))
            ++AnswerIndex;

        if (Input.GetKeyDown(KeyCode.J) && !selectAnswerSlots[answerIndex].hasLock)
        {
            StartCoroutine(DoHide(choiceDatas[answerIndex]));
        }

    }

    public void SetAnswer(List<ChoiceData> choiceDatas, UnityAction<ChoiceData> action)
    {
        if (speakerNameText == null)
        {
            Init();
        }
        this.choiceDatas = choiceDatas;
        this.action = action;
        speakerNameText.text = MainPlayerController.Instance.mainName;
        transform.position = MainPlayerController.Instance.transform.position - new Vector3(0.3f, 0.3f);
        transform.DOScale(new Vector3(0.001f, 0.001f, 1), 0.2f);
        int i = 0;
        for (; i < choiceDatas.Count; i++)
        {
            selectAnswerSlots[i].SetAnswer(choiceDatas[i]);
        }
        limitIndex = i;
        for (; i < selectAnswerSlots.Length; i++)
        {
            selectAnswerSlots[i].gameObject.SetActive(false);
        }
        AnswerIndex = 0;
    }


    IEnumerator DoHide(ChoiceData choiceData)
    {
        transform.DOScale(Vector3.zero, 0.2f);
        yield return new WaitForSeconds(0.2f);
        action?.Invoke(choiceData);
        PoolManager.Instance.PushObj("SOSelectAnswerUI", gameObject);
    }
}
