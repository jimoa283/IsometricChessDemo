using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace KasperDev.DialogueEditor
{

    [CreateAssetMenu(menuName = "Dialogue/New Dialogue")]
    [System.Serializable]
    public class DialogueContainerSO : ScriptableObject
    {
        public List<NodeLinkData> NodeLinkDatas = new List<NodeLinkData>();

        public List<DialogueData> DialogueDatas = new List<DialogueData>();
        public List<StartData> StartDatas = new List<StartData>();
        public List<EndData> EndDatas = new List<EndData>();
        public List<EventData> EventDatas = new List<EventData>();
        public List<BranchData> BranchDatas = new List<BranchData>();
        public List<ChoiceData> ChoiceDatas = new List<ChoiceData>();

        public List<BaseData> AllDatas
        {
            get
            {
                List<BaseData> tmp = new List<BaseData>();
                tmp.AddRange(DialogueDatas);
                tmp.AddRange(EndDatas);
                tmp.AddRange(StartDatas);
                tmp.AddRange(EventDatas);
                tmp.AddRange(BranchDatas);
                tmp.AddRange(ChoiceDatas);
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

    public enum LanguageType
    {
        English=1,
        Chinese=2
    }
}
