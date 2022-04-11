using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System.Linq;
using UnityEditor;

public class MyDialogueSaveAndLoad 
{
    private MyDialogueGraphView graphView;

    /// <summary>
    /// 获得所有的连线
    /// </summary>
    private List<Edge> edges =>graphView.edges.ToList().Where(x => x.input.node != null).ToList();

    /// <summary>
    /// 获得所有的节点
    /// </summary>
    private List<BaseNode> nodes => graphView.nodes.ToList().Where(node => node is BaseNode).Cast<BaseNode>().ToList();

    public MyDialogueSaveAndLoad(MyDialogueGraphView graphView)
    {
        this.graphView = graphView;
    }

    public void Save(MyDialogueContainerSO containerSO)
    {
        SaveNodes(containerSO);
        SaveEdges(containerSO);    

        EditorUtility.SetDirty(containerSO);
        AssetDatabase.SaveAssets();
    }

    private void SaveNodes(MyDialogueContainerSO containerSO)
    {
        containerSO.StartDatas.Clear();
        containerSO.EndDatas.Clear();
        containerSO.BranchDatas.Clear();
        containerSO.DialogueDatas.Clear();
        containerSO.ChoiceDatas.Clear();
        containerSO.EventDatas.Clear();

        nodes.ForEach(node =>
        {
            switch(node)
            {
                case StartNode startNode:
                    containerSO.StartDatas.Add(SaveNodeData(startNode));
                    break;
                case EndNode endNode:
                    containerSO.EndDatas.Add(SaveNodeData(endNode));
                    break;
                case BranchNode branchNode:
                    containerSO.BranchDatas.Add(SaveNodeData(branchNode));
                    break;
                case DialogueNode dialogueNode:
                    containerSO.DialogueDatas.Add(SaveNodeData(dialogueNode));
                    break;
                case ChoiceNode choiceNode:
                    containerSO.ChoiceDatas.Add(SaveNodeData(choiceNode));
                    break;
                case EventNode eventNode:
                    containerSO.EventDatas.Add(SaveNodeData(eventNode));
                    break;
                default:
                    break;
            }
        });
    }

    private StartData SaveNodeData(StartNode node)
    {
        StartData startData = new StartData()
        {
            NodeGuid = node.NodeGuid,
            Position = node.GetPosition().position,
        };

        return startData;
    }

    private EndData SaveNodeData(EndNode node)
    {
        EndData endData = new EndData()
        {
            NodeGuid = node.NodeGuid,
            Position = node.GetPosition().position,
        };

        return endData;
    }

    private BranchData SaveNodeData(BranchNode node)
    {
        Edge trueEdge = edges.FirstOrDefault(x => x.output.node == node && x.output.portName == "True");
        Edge falseEdge = edges.FirstOrDefault(x => x.output.node == node && x.output.portName == "False");

        BranchData branchData = new BranchData()
        {
            NodeGuid = node.NodeGuid,
            Position = node.GetPosition().position,
            trueGuidNode = trueEdge != null ? (trueEdge.input.node as BaseNode).NodeGuid : string.Empty,
            falseGuidNode=falseEdge!=null?(falseEdge.input.node as BaseNode).NodeGuid : string.Empty,
        };

        foreach(var container in node.BranchData.conditionCheckSOs)
        {
            Container_ConditionCheck newTmp = new Container_ConditionCheck();
            newTmp.Container_ConditionCheckSO.Value = container.Container_ConditionCheckSO.Value;
            newTmp.Container_ConditionAddType.Value = container.Container_ConditionAddType.Value;

            branchData.conditionCheckSOs.Add(newTmp);
        }

        return branchData;
    }

    private DialogueData SaveNodeData(DialogueNode node)
    {
        DialogueData dialogueData = new DialogueData()
        {
            NodeGuid = node.NodeGuid,
            Position = node.GetPosition().position,
        };

        dialogueData.DialogueData_Name.Name.Value = node.DialogueData.DialogueData_Name.Name.Value;
        dialogueData.DialogueData_Text.Text.Value = node.DialogueData.DialogueData_Text.Text.Value;
        dialogueData.DialogueData_Audio.AudioClip = node.DialogueData.DialogueData_Audio.AudioClip;

        foreach(var port in node.DialogueData.DialogueData_Ports)
        {
            DialogueData_Port newPort = new DialogueData_Port();
            newPort.PortGuid = port.PortGuid;
            newPort.inputGuid = string.Empty;
            newPort.outputGuid = string.Empty;

            foreach(var edge in edges)
            {
                if(edge.output.portName==port.PortGuid)
                {
                    newPort.inputGuid = (edge.input.node as BaseNode).NodeGuid;
                    newPort.outputGuid = (edge.output.node as BaseNode).NodeGuid;
                }
            }

            dialogueData.DialogueData_Ports.Add(newPort);
        }

        return dialogueData;
    }

    private ChoiceData SaveNodeData(ChoiceNode node)
    {
        ChoiceData choiceData = new ChoiceData()
        {
            NodeGuid = node.NodeGuid,
            Position = node.GetPosition().position,
        };

        choiceData.Text.Value = node.ChoiceData.Text.Value;
        choiceData.AudioClip = node.ChoiceData.AudioClip;

        foreach (var container in node.ChoiceData.ConditionChecks)
        {
            Container_ConditionCheck newTmp = new Container_ConditionCheck();
            newTmp.Container_ConditionCheckSO.Value = container.Container_ConditionCheckSO.Value;
            newTmp.Container_ConditionAddType.Value = container.Container_ConditionAddType.Value;

            choiceData.ConditionChecks.Add(newTmp);
        }

        return choiceData;
    }

    private EventData SaveNodeData(EventNode node)
    {
        EventData eventData = new EventData()
        {
            NodeGuid = node.NodeGuid,
            Position = node.GetPosition().position,
        };

        eventData.Container_Event.Value = node.EventData.Container_Event.Value;

        return eventData;
    }

    private void SaveEdges(MyDialogueContainerSO containerSO)
    {
        containerSO.NodeLinkDatas.Clear();

        Edge[] connectEdge = edges.ToArray();

        for (int i = 0; i < connectEdge.Length; i++)
        {
            BaseNode inputNode = connectEdge[i].input.node as BaseNode;
            BaseNode outputNode = connectEdge[i].output.node as BaseNode;

            containerSO.NodeLinkDatas.Add(new NodeLinkData()
            {
                BaseNodeGuid=outputNode.NodeGuid,
                BasePortName=connectEdge[i].output.portName,
                TargetNodeGuid=inputNode.NodeGuid,
                TargetPortName=connectEdge[i].input.portName
            });
        }
    }

    public void Load(MyDialogueContainerSO containerSO)
    {
        ClearGraph();
        GenerateNodes(containerSO);
        ConnectNodes(containerSO);
    }

    private void ClearGraph()
    {
        edges.ForEach(edge => graphView.RemoveElement(edge));

        foreach(var node in nodes)
        {
            graphView.RemoveElement(node);
        }
    }

    private void GenerateNodes(MyDialogueContainerSO containerSO)
    {
        foreach(var data in containerSO.StartDatas)
        {
           StartNode node=graphView.CreateStartNode(data.Position);
            node.NodeGuid = data.NodeGuid;

            graphView.AddElement(node);
        }

        foreach(var data in containerSO.EndDatas)
        {
            EndNode node = graphView.CreateEndNode(data.Position);
            node.NodeGuid = data.NodeGuid;

            node.LoadValueInField();

            graphView.AddElement(node);
        }

        foreach(var data in containerSO.BranchDatas)
        {
            BranchNode node = graphView.CreateBranchNode(data.Position);
            node.NodeGuid = data.NodeGuid;

            foreach(var item in data.conditionCheckSOs)
            {
                node.AddBranchCondition(item);
            }

            node.LoadValueInField();

            graphView.AddElement(node);
        }

        foreach(var data in containerSO.DialogueDatas)
        {
            DialogueNode node = graphView.CreateDialogueNode(data.Position);
            node.NodeGuid = data.NodeGuid;

            node.DialogueData.DialogueData_Name.Name.Value = data.DialogueData_Name.Name.Value;
            node.DialogueData.DialogueData_Text.Text.Value = data.DialogueData_Text.Text.Value;
            node.DialogueData.DialogueData_Audio.AudioClip = data.DialogueData_Audio.AudioClip;

            foreach(var item in data.DialogueData_Ports)
            {
                node.AddChoicePort(node,item);
            } 

            node.LoadValueInField();
            graphView.AddElement(node);
        }

        foreach(var data in containerSO.ChoiceDatas)
        {
            ChoiceNode node = graphView.CreateChoiceNode(data.Position);
            node.NodeGuid = data.NodeGuid;

            node.ChoiceData.Text.Value = data.Text.Value;
            node.ChoiceData.AudioClip = data.AudioClip;

            foreach(var item in data.ConditionChecks)
            {
                node.AddChoiceCondition(item);
            }

            node.LoadValueInField();
            graphView.AddElement(node);
        }

        foreach(var data in containerSO.EventDatas)
        {
            EventNode node = graphView.CreateEventNode(data.Position);
            node.NodeGuid = data.NodeGuid;

            node.EventData.Container_Event.Value = data.Container_Event.Value;

            node.LoadValueInField();
            graphView.AddElement(node);
        }
    }

    private void ConnectNodes(MyDialogueContainerSO containerSO)
    {
        for(int i=0;i<nodes.Count;i++)
        {
            List<NodeLinkData> connections = containerSO.NodeLinkDatas.Where(edge => edge.BaseNodeGuid == nodes[i].NodeGuid).ToList();

            List<Port> allOutputPorts = nodes[i].outputContainer.Children().Where(x => x is Port).Cast<Port>().ToList();

            for (int j = 0; j < connections.Count; j++)
            {
                string targetNodeGuid = connections[j].TargetNodeGuid;
                BaseNode targetNode = nodes.First(x => x.NodeGuid == targetNodeGuid);

                if (targetNode == null)
                    return;

                foreach(var port in allOutputPorts)
                {
                    if(port.portName==connections[j].BasePortName)
                    {
                        LinkTogether(port, (Port)targetNode.inputContainer[0]);
                    }
                }
            }
        }
    }

    private void LinkTogether(Port outputPort,Port inputPort)
    {
        Edge edge = new Edge()
        {
            output = outputPort,
            input = inputPort,
        };

        edge.input.Connect(edge);
        edge.output.Connect(edge);
        graphView.Add(edge);
    }
}
