using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KasperDev.DialogueEditor
{
    public class DialogueGetData : MonoBehaviour
    {
        [SerializeField] protected DialogueContainerSO dialogueContainer;

        protected BaseData GetNodeByGuid(string _targetNodeGuid)
        {
            return dialogueContainer.AllDatas.Find(node => node.NodeGuid == _targetNodeGuid);
        }

        protected BaseData GetNodeByNodePort(DialogueData_Port _nodePort)
        {
            return dialogueContainer.AllDatas.Find(node => node.NodeGuid == _nodePort.InputGuid);
        }

        protected BaseData GetNextNode(BaseData _baseNodeData)
        {
            NodeLinkData nodeLinkData = dialogueContainer.NodeLinkDatas.Find(edge => edge.BaseNodeGuid == _baseNodeData.NodeGuid);

            return GetNodeByGuid(nodeLinkData.TargetNodeGuid);
        }


    }
}
