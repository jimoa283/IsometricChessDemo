using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyDialogue/New Dialogue")]
[System.Serializable]
public class MyDialogueContainerSO : ScriptableObject
{
    public List<NodeLinkData> NodeLinkDatas = new List<NodeLinkData>();
    public List<StartData> StartDatas = new List<StartData>();
    public List<EndData> EndDatas = new List<EndData>();
    public List<BranchData> BranchDatas = new List<BranchData>();
    public List<DialogueData> DialogueDatas = new List<DialogueData>();
    public List<ChoiceData> ChoiceDatas = new List<ChoiceData>();
    public List<EventData> EventDatas = new List<EventData>();

    public List<BaseData> AllDatas
    {
        get
        {
            List<BaseData> tmp = new List<BaseData>();
            tmp.AddRange(StartDatas);
            tmp.AddRange(EndDatas);
            tmp.AddRange(BranchDatas);
            tmp.AddRange(DialogueDatas);
            tmp.AddRange(ChoiceDatas);
            tmp.AddRange(EventDatas);
            return tmp;
        }
    }
}

[System.Serializable]
public class NodeLinkData
{
    public string BaseNodeGuid;
    public string BasePortName;
    public string TargetNodeGuid;
    public string TargetPortName;   
}
