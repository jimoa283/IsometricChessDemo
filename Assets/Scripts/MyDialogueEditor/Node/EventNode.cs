using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class EventNode : BaseNode
{
    private EventData eventData=new EventData();

    public EventData EventData { get => eventData; set => eventData = value; }

    public EventNode() { }

    public EventNode(Vector2 position,MyDialogueEditorWindow editorWindow,MyDialogueGraphView graphView)
    {
        this.editorWindow = editorWindow;
        this.graphView = graphView;

        StyleSheet styleSheet = Resources.Load<StyleSheet>("MyUSS/Node/EventNodeStyleSheet");
        styleSheets.Add(styleSheet);

        title = "Event";
        SetPosition(new Rect(position, defaultNodeSize));
        nodeGuid = Guid.NewGuid().ToString();

        AddInputPort("Input", Port.Capacity.Multi);
        AddOutPutPort("Output", Port.Capacity.Single);

        eventData.EventField = GetNewObjectField_Event(eventData.Container_Event);
        mainContainer.Add(eventData.EventField);

        RefreshExpandedState();
    }

    public ObjectField GetNewObjectField_Event(Container_Event container,string USS01="",string USS02="")
    {
        ObjectField objectField = new ObjectField()
        {
            objectType = typeof(EventSO),
            allowSceneObjects = false,
            value = container.Value,
        };

        objectField.RegisterValueChangedCallback(value =>
        {
            container.Value = value.newValue as EventSO;
        });
        objectField.SetValueWithoutNotify(container.Value);

        return objectField;
    }

    public override void LoadValueInField()
    {
        eventData.EventField.SetValueWithoutNotify(eventData.Container_Event.Value);
    }
}
