using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using System.Linq;

public class DialogueNode : BaseNode
{
    private DialogueData dialogueData=new DialogueData();

    public DialogueData DialogueData { get => dialogueData; set => dialogueData = value; }

    public DialogueNode() { }
    public DialogueNode(Vector2 position,MyDialogueEditorWindow editorWindow,MyDialogueGraphView graphView)
    {
        this.editorWindow = editorWindow;
        this.graphView = graphView;

        StyleSheet styleSheet = Resources.Load<StyleSheet>("MyUSS/Node/DialogueNodeStyleSheet");
        styleSheets.Add(styleSheet);

        title = "Dialogue";
        SetPosition(new Rect(position, defaultNodeSize));
        nodeGuid = Guid.NewGuid().ToString();

        AddInputPort("Input", Port.Capacity.Multi);
        AddOutPutPort("Continue", Port.Capacity.Single);

        TopButton();

        AddDialogueName();
        AddDialogueText();
        AddDialogueAudio();

        RefreshExpandedState();
        RefreshPorts();
    }

    private void TopButton()
    {
        Button btn = GetNewButton("Add Choice");

        btn.clicked += () => { AddChoicePort(this); };

        titleButtonContainer.Add(btn);
    }

    public void AddChoicePort(DialogueNode node,DialogueData_Port dialogueData_Port=null)
    {
        Port port = GetPortInstance(Direction.Output);
        DialogueData_Port newPort = new DialogueData_Port();

        if(dialogueData_Port!=null)
        {
            newPort.inputGuid = dialogueData_Port.inputGuid;
            newPort.outputGuid = dialogueData_Port.outputGuid;
            newPort.PortGuid = dialogueData_Port.PortGuid;
        }
        else
        {
            newPort.PortGuid = Guid.NewGuid().ToString();
        }
        dialogueData.DialogueData_Ports.Add(newPort);

        Button btn = GetNewButton("X");
        btn.clicked += () =>
         {
             DeletePort(node, port);
         };
        port.contentContainer.Add(btn);

        port.portName = newPort.PortGuid;
        Label portNameLabel = port.contentContainer.Q<Label>("type");
        portNameLabel.AddToClassList("PortName");

        port.portColor = Color.yellow;

        node.outputContainer.Add(port);

        RefreshPorts();
        RefreshExpandedState();
    }

    private void DeletePort(DialogueNode node,Port port)
    {
        DialogueData_Port tmp = dialogueData.DialogueData_Ports.Find(findPort => findPort.PortGuid == port.portName);
        dialogueData.DialogueData_Ports.Remove(tmp);

        IEnumerable<Edge> portEdge = graphView.edges.ToList().Where(edge => edge.output == port);

        if(portEdge.Any())
        {
            Edge edge = portEdge.First();
            edge.input.Disconnect(edge);
            edge.input.Disconnect(edge);
            graphView.RemoveElement(edge);
        }

        node.outputContainer.Remove(port);

        node.RefreshPorts();
        node.RefreshExpandedState();
    }

    public void AddDialogueName()
    {
        Box dialogueNameBox = new Box();

        AddLabel(dialogueNameBox, "Name");

        dialogueData.DialogueData_Name.textField = GetNewTextField(dialogueData.DialogueData_Name.Name, "Name");

        dialogueNameBox.Add(dialogueData.DialogueData_Name.textField);

        mainContainer.Add(dialogueNameBox);
    }

    public void AddDialogueText()
    {
        Box dialogueTextBox = new Box();

        AddLabel(dialogueTextBox, "Text");

        dialogueData.DialogueData_Text.textField = GetNewTextField(dialogueData.DialogueData_Text.Text, "Dialogue", "TextBox");
        dialogueData.DialogueData_Text.textField.multiline = true;

        dialogueTextBox.Add(dialogueData.DialogueData_Text.textField);

        mainContainer.Add(dialogueTextBox);
    }

    public void AddDialogueAudio()
    {
        Box dialogueAudioBox = new Box();

        AddLabel(dialogueAudioBox, "Audio");

        dialogueData.DialogueData_Audio.objectField = GetNewObjectField_Audio(dialogueData.DialogueData_Audio.AudioClip);

        dialogueAudioBox.Add(dialogueData.DialogueData_Audio.objectField);

        mainContainer.Add(dialogueAudioBox);
    }

    private void AddLabel(Box boxContainer,string labelName,string uniqueUSS="")
    {
        Box topBox = new Box();

        Label label_text = GetNewLabel(labelName,"Label");

        topBox.Add(label_text);

        boxContainer.Add(topBox);
    }

    public override void LoadValueInField()
    {
        dialogueData.DialogueData_Name.textField.SetValueWithoutNotify(dialogueData.DialogueData_Name.Name.Value);
        dialogueData.DialogueData_Text.textField.SetValueWithoutNotify(dialogueData.DialogueData_Text.Text.Value);
        dialogueData.DialogueData_Audio.objectField.SetValueWithoutNotify(dialogueData.DialogueData_Audio.AudioClip);

        SetPlaceholderText(dialogueData.DialogueData_Name.textField, "Name");
        SetPlaceholderText(dialogueData.DialogueData_Text.textField, "Dialogue");
    }
}
