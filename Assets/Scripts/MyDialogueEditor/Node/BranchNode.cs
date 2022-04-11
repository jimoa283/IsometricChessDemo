using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class BranchNode : BaseNode
{
    private BranchData branchData=new BranchData();

    public BranchData BranchData { get => branchData; set => branchData = value; }

    public BranchNode() { }

    public BranchNode(Vector2 position,MyDialogueEditorWindow editorWindow,MyDialogueGraphView graphView)
    {
        this.editorWindow = editorWindow;
        this.graphView = graphView;

        StyleSheet styleSheet = Resources.Load<StyleSheet>("MyUSS/Node/BranchNodeStyleSheet");
        styleSheets.Add(styleSheet);

        title = "Branch";
        SetPosition(new Rect(position, defaultNodeSize));
        nodeGuid = Guid.NewGuid().ToString();

        AddInputPort("Input", Port.Capacity.Multi);
        AddOutPutPort("True", Port.Capacity.Single);
        AddOutPutPort("False", Port.Capacity.Single);

        TopButton();

        RefreshExpandedState();
        RefreshPorts();
    }

    private void TopButton()
    {
        Button btn = new Button()
        {
            text = "Add Condition",
        };

        btn.clicked += ()=> { AddBranchCondition(); };

        titleButtonContainer.Add(btn);
    }

    public void AddBranchCondition(Container_ConditionCheck container=null)
    {

        AddConditionCheck(branchData.conditionCheckSOs, container);

        int temp = branchData.conditionCheckSOs.Count;

        if (temp == 1)
        {
            branchData.conditionCheckSOs[0].Container_ConditionAddType.EnumField.AddToClassList("Hide");
        }
        else
        {
            branchData.conditionCheckSOs[temp - 1].Container_ConditionAddType.EnumField.AddToClassList("Hide");
            branchData.conditionCheckSOs[temp - 2].Container_ConditionAddType.EnumField.RemoveFromClassList("Hide");
        }
    }
}
