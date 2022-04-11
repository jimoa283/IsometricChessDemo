using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class EndNode : BaseNode
{
    private EndData endData;
    public EndData EndData { get => endData; set => endData = value; }

    public EndNode() { }

    public EndNode(Vector2 position,MyDialogueEditorWindow editorWindow,MyDialogueGraphView graphView)
    {
        this.editorWindow = editorWindow;
        this.graphView = graphView;

        StyleSheet styleSheet = Resources.Load<StyleSheet>("MyUSS/Node/EndNodeStyleSheet");
        styleSheets.Add(styleSheet);

        title = "End";
        SetPosition(new Rect(position, defaultNodeSize));
        nodeGuid = Guid.NewGuid().ToString();

        AddInputPort("Input", Port.Capacity.Multi);

        RefreshExpandedState();
        RefreshPorts();
    }
}
