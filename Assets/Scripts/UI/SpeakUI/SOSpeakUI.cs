using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class SOSpeakUI : MonoBehaviour
{
    private Text speakerNameText;
    private Text speakContentText;
    private GameObject arrow;

    private bool canControll;

    public float waitTime;

    private UnityAction nextAction;

    RectTransform rt;

    private MyDialogueContainerSO dialogueContainerSO;

    private Vector3 speakerPos;

    private CanvasGroup canvasGroup;

    private BaseData currentNodeData;

    private DialogueData currentDialogueNodeData;

    private UnityAction afterFinishAction;

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
        if (arrow.activeInHierarchy&&canControll&&Input.GetKeyDown(KeyCode.J))
        {
            nextAction?.Invoke();
        }
    }

    public void StartSpeakBySO(MyDialogueContainerSO dialogueContainerSO, Vector3 pos,UnityAction action)
    {
        if (arrow == null)
            Init();

        afterFinishAction = action;
        this.dialogueContainerSO = dialogueContainerSO;
        gameObject.SetActive(true);
        speakerPos = pos;
        transform.position = pos + new Vector3(0.3f, 0.3f);
        arrow.SetActive(false);
        currentNodeData = dialogueContainerSO.StartDatas[0];
        nextAction = CheckNodeType;
        CheckNodeType();
    }

    protected BaseData GetNodeByGuid(string targetNodeGuid)
    {
        return dialogueContainerSO.AllDatas.Find(node => node.NodeGuid == targetNodeGuid);
    }

    protected BaseData GetNodeByNodePort(DialogueData_Port nodePort)
    {
        return dialogueContainerSO.AllDatas.Find(node => node.NodeGuid == nodePort.inputGuid);
    }

    protected BaseData GetNextNode(BaseData baseNodeData)
    {
        NodeLinkData nodeLinkData = dialogueContainerSO.NodeLinkDatas.Find(edge => edge.BaseNodeGuid == baseNodeData.NodeGuid);

        return GetNodeByGuid(nodeLinkData.TargetNodeGuid);
    }

    private void CheckNodeType()
    {
        if(currentNodeData==null)
        {
            Debug.LogWarning("DialogueNode is null,please check your dialogueContainerSO");
            DirectExit(null, true);
            return;
        }

        switch(currentNodeData)
        {
            case StartData nodeData:
                RunNode(nodeData);
                break;
            case BranchData nodeData:
                RunNode(nodeData);
                break;
            case EventData nodeData:
                RunNode(nodeData);
                break;
            case EndData nodeData:
                RunNode(nodeData);
                break;
            case DialogueData nodeData:
                RunNode(nodeData);
                break;
            default:
                break;
        }
    }

    private void RunNode(StartData nodeData)
    {
        currentNodeData = GetNextNode(dialogueContainerSO.StartDatas[0]);
        CheckNodeType();
    }
        
    private void RunNode(BranchData nodeData)
    {
        bool checkBranch = true;
       
        foreach(Container_ConditionCheck item in nodeData.conditionCheckSOs)
        {
            if (!item.Container_ConditionCheckSO.Value.ChoiceCheck())
            {
                checkBranch = false;
                break;
            }              
        }

        string nextNode = (checkBranch ? nodeData.trueGuidNode : nodeData.falseGuidNode);
        currentNodeData = GetNodeByGuid(nextNode);
    }

    private void RunNode(EventData nodeData)
    {
        if(nodeData.Container_Event.Value==null)
        {
            Debug.LogWarning("Event is null,please check");
            currentNodeData = GetNextNode(nodeData);
            CheckNodeType();
            return;
        }

        nodeData.Container_Event.Value.DoEvent(()=> { currentNodeData = GetNextNode(nodeData);CheckNodeType(); });
        
    }

    private void RunNode(EndData nodeData)
    {
        DirectExit(null,true);
    }

    private void RunNode(DialogueData nodeData)
    {
        currentDialogueNodeData = nodeData;

        speakerNameText.text = currentDialogueNodeData.DialogueData_Name.Name.Value;
        string ContentText = currentDialogueNodeData.DialogueData_Text.Text.Value;
        AdjustSpeakContentText(ContentText);
        StartCoroutine(DoShowSpeak(ContentText));

        if(nodeData.DialogueData_Ports.Count>0)
        {
            nextAction = ShowSelectAnswer;
        }
        else
        {
            currentNodeData = GetNextNode(nodeData);
            nextAction = () => { DirectExit(CheckNodeType, false); };
        }
    }

    private void AdjustSpeakContentText(string speakContent)
    {
        int lineNum = speakContent.Split('#').Length;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, 170 * lineNum + 100);
    }

    IEnumerator DoShowSpeak(string speakContent)
    {
        canvasGroup.alpha = 1;
        speakContent = speakContent.Replace('#', ' ');
        speakContentText.text = "";
        transform.DOScale(new Vector2(0.001f, 0.001f), 0.3f);
        yield return new WaitForSeconds(0.3f);
        foreach (var letter in speakContent.ToCharArray())
        {
            speakContentText.text += letter;
            yield return new WaitForSeconds(waitTime);
        }
        arrow.SetActive(true);
        canControll = true;
    }

    private void ShowSelectAnswer()
    {
        arrow.gameObject.SetActive(false);
        canvasGroup.alpha = 0.7f;
        List<ChoiceData> choiceDatas=new List<ChoiceData>();
        foreach(DialogueData_Port port in currentDialogueNodeData.DialogueData_Ports)
        {
            choiceDatas.Add(GetNodeByGuid(port.inputGuid) as ChoiceData);
        }
        SOSelectAnswerUI selectAnswerUI = PoolManager.Instance.GetObj("SOSelectAnswerUI").GetComponent<SOSelectAnswerUI>();
        selectAnswerUI.SetAnswer(choiceDatas, (ChoiceData choiceData)=> { currentNodeData = GetNextNode(choiceData);DirectExit(CheckNodeType,false); });
    }

    private void DirectExit(UnityAction action,bool isFinished)
    {
        StartCoroutine(DoDirectExit(action,isFinished));
    }

    IEnumerator DoDirectExit(UnityAction action,bool isFinished)
    {
        transform.DOScale(0, 0.2f);
        yield return new WaitForSeconds(0.21f);

        action?.Invoke();

        if (isFinished)
        {
            afterFinishAction?.Invoke();
            PoolManager.Instance.PushObj("SOSpeakUI", gameObject);
        }
        else
            canControll = false;
    }
}

