using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class ChoiceNode : BaseNode
{
    private ChoiceData choiceData=new ChoiceData();
    public ChoiceData ChoiceData { get => choiceData; set => choiceData = value; }

    public ChoiceNode() { }

    public ChoiceNode(Vector2 position,MyDialogueEditorWindow editorWindow,MyDialogueGraphView graphView)
    {
        this.editorWindow = editorWindow;
        this.graphView = graphView;

        StyleSheet styleSheet = Resources.Load<StyleSheet>("MyUSS/Node/ChoiceNodeStyleSheet");
        styleSheets.Add(styleSheet);

        title = "Choice";
        SetPosition(new Rect(position, default));
        nodeGuid = Guid.NewGuid().ToString();

        Port input= AddInputPort("Input", Port.Capacity.Multi);      
        input.portColor = Color.yellow;

        AddOutPutPort("Output", Port.Capacity.Single);

        TopButton();

        Textline();

        RefreshExpandedState();
        RefreshPorts();
    }

    private void TopButton()
    {
        Button btn = new Button()
        {
            text = "Add Condition",
        };

        btn.clicked += () => { AddChoiceCondition(); };

        titleButtonContainer.Add(btn);
    }

    private void Textline()
    {
        Box boxContainer = new Box();

        choiceData.TextField = GetNewTextField(choiceData.Text,"Text", "TextBox");
        boxContainer.Add(choiceData.TextField);

        choiceData.AudioClipField = GetNewObjectField_Audio(choiceData.AudioClip);
        boxContainer.Add(choiceData.AudioClipField);

        mainContainer.Add(boxContainer);
    }

    public void AddChoiceCondition(Container_ConditionCheck container = null)
    {
        AddConditionCheck(choiceData.ConditionChecks, container);

        int temp = choiceData.ConditionChecks.Count;

        if (temp==1)
        {
            choiceData.ConditionChecks[0].Container_ConditionAddType.EnumField.AddToClassList("Hide");
        }
        else
        {
            choiceData.ConditionChecks[temp - 1].Container_ConditionAddType.EnumField.AddToClassList("Hide");
            choiceData.ConditionChecks[temp - 2].Container_ConditionAddType.EnumField.RemoveFromClassList("Hide");
        }
    }

    public override void LoadValueInField()
    {
        choiceData.TextField.SetValueWithoutNotify(choiceData.Text.Value);
        choiceData.AudioClipField.SetValueWithoutNotify(choiceData.AudioClip);

        SetPlaceholderText(choiceData.TextField, "Text");
    }
}
