using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class StartNode : BaseNode
{
    private StartData startData;

    public StartData StartData { get => startData; set => startData = value; }

    public StartNode() { }

    public StartNode(Vector2 position,MyDialogueEditorWindow editorWindow,MyDialogueGraphView graphView)
    {
        this.editorWindow = editorWindow;
        this.graphView = graphView;

        StyleSheet styleSheet = Resources.Load<StyleSheet>("MyUSS/Node/StartNodeStyleSheet");
        styleSheets.Add(styleSheet);

        title = "Start";
        SetPosition(new Rect(position, defaultNodeSize));
        nodeGuid = Guid.NewGuid().ToString();

        AddOutPutPort("OutPut",Port.Capacity.Single);

        RefreshExpandedState();
        RefreshPorts();
    }
}
