using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class DynamicSpeakUI : MonoBehaviour
{
    private Text speakerNameText;
    private Text speakContentText;
    private GameObject arrow;

    private bool canControll;

    public float waitTime;

    private UnityAction nextAction;

    RectTransform rt;

    private SpeakData speakData;

    private MyDialogueContainerSO dialogueContainerSO;

    private Vector3 speakerPos;

    private CanvasGroup canvasGroup;

    public void Init()
    {
        speakerNameText = TransformHelper.GetChildTransform(transform, "SpeakerNameText").GetComponent<Text>();
        speakContentText = TransformHelper.GetChildTransform(transform, "SpeakContent").GetComponent<Text>();
        arrow = TransformHelper.GetChildTransform(transform, "Arrow").gameObject;
        rt = speakContentText.GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        //gameObject.SetActive(false);
    }

    private void Update()
    {
        if (arrow.activeInHierarchy&&Input.GetKeyDown(KeyCode.J))
        {
            nextAction?.Invoke();
        }
    }

    public void StartSpeakByData(SpeakData speakData,Vector3 pos)
    {
        if(arrow==null)
        {
            Init();
        }
        canvasGroup.alpha = 1;
        this.speakData = speakData;
        gameObject.SetActive(true);
        speakerPos = pos;
        transform.position = pos+new Vector3(0.3f,0.3f);
        arrow.SetActive(false);
        speakerNameText.text = speakData.SpeakerName;
        AdjustSpeakContentText(speakData.SpeakContent);
        StartCoroutine(DoShowSpeak(speakData.SpeakContent));
        switch (speakData.SpeakDataType)
        {
            case SpeakDataType.SimpleSpeak:
                nextAction = SimpleSpeak;
                break;
            case SpeakDataType.ShowIntroduceSpeak:
                nextAction = ShowPieceIntroduce;
                break;
            case SpeakDataType.ItemGetSpeak:
                break;
            case SpeakDataType.SelectAnswerSpeak:
                nextAction = ShowSelectAnswer;
                break;
            case SpeakDataType.IntelligenceGetSpeak:
                nextAction = IntelligenceGetSpeak;
                break;
            default:
                break;
        }
    }

    public void StartSpeakBySO(MyDialogueContainerSO dialogueContainerSO,Vector3 pos)
    {
        if (arrow == null)
            Init();

        canvasGroup.alpha = 1;
        this.dialogueContainerSO = dialogueContainerSO;
        gameObject.SetActive(true);
        speakerPos = pos;
        transform.position = pos + new Vector3(0.3f, 0.3f);
        arrow.SetActive(false);
    }

    private void AdjustSpeakContentText(string speakContent)
    {
        int lineNum = speakContent.Split('#').Length;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, 170 * lineNum + 100);
    }

    IEnumerator DoShowSpeak(string speakContent)
    {
        speakContent = speakContent.Replace('#', '\n');
        speakContentText.text = "";
        transform.DOScale(new Vector2(0.001f, 0.001f), 0.3f);
        yield return new WaitForSeconds(0.3f);
        foreach(var letter in speakContent.ToCharArray())
        {
            speakContentText.text += letter;
            yield return new WaitForSeconds(waitTime);
        }
        arrow.SetActive(true);
        /*if(speakData.SpeakDataType==SpeakDataType.SelectAnswerSpeak)
        {
            ShowSelectAnswer();
        }*/           
    }

    private void ShowSelectAnswer()
    {
        arrow.gameObject.SetActive(false);
        canvasGroup.alpha = 0.7f;
        SelectAnswerUI selectAnswerUI = PoolManager.Instance.GetObj("SelectAnswerUI").GetComponent<SelectAnswerUI>();
        selectAnswerUI.SetAnswer(speakData as SelectAnswerSpeak,ContinueSelectSpeak);
    }

    private void ShowPieceIntroduce()
    {
        DirectExit(() =>
        {
            ShowIntroduceSpeak showIntroduceSpeak = speakData as ShowIntroduceSpeak;
            PieceIntroduceManager.Instance.SetPieceIntroduce(showIntroduceSpeak.PieceIntroduceName, ContinueSpeak);
        });       
    }

    private void ContinueSpeak()
    {
        SpeakManager.Instance.SetSpeakById(speakData.NextSpeakIndex, speakerPos);
    }

    private void SimpleSpeak()
    {
        DirectExit(() =>
        {
            ContinueSpeak();
        });       
    }

    private void ContinueSelectSpeak(SingleSelectAnswer singleSelectAnswer)
    {
        DirectExit(() =>
        {
            SpeakManager.Instance.SetSpeakById(singleSelectAnswer.NextSpeakIndex, speakerPos);
        });      
    }

    private void IntelligenceGetSpeak()
    {
        DirectExit(() =>
        {
            BriefIntelligenceManager.Instance.ShowBriefIntelligence((speakData as IntelligenceGetSpeak).IntelligenceID);
            ContinueSpeak();
        });
    }

    private void DirectExit(UnityAction action)
    {
        StartCoroutine(DoDirectExit(action));
    }

    IEnumerator DoDirectExit(UnityAction action)
    {
        transform.DOScale(0, 0.3f);
        yield return new WaitForSeconds(0.35f);
        action?.Invoke();
        PoolManager.Instance.PushObj("SpeakUI", gameObject);      
    }
}




