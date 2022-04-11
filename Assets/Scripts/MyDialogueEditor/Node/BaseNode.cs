using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System;

public class BaseNode : Node
{
    protected MyDialogueEditorWindow editorWindow;
    protected MyDialogueGraphView graphView;
    private string nodeStyleSheet="MyUSS/Node/BaseNodeStyleSheet";
    protected string nodeGuid;
    protected Vector2 defaultNodeSize = new Vector2(200, 250);

    public string NodeGuid { get => nodeGuid; set => nodeGuid = value; }

    public BaseNode()
    {
        StyleSheet styleSheet = Resources.Load<StyleSheet>(nodeStyleSheet);
        styleSheets.Add(styleSheet);
    }

    /// <summary>
    /// Get a new Label
    /// </summary>
    /// <param name="labelName">Text in the label</param>
    /// <param name="USS01">USS class add to the UI element</param>
    /// <param name="USS02">USS class add to the UI element</param>
    /// <returns></returns>
    protected Label GetNewLabel(string labelName, string USS01 = "", string USS02 = "")
    {
        Label label_texts = new Label(labelName);

        //Set uss class for stylesheet.
        label_texts.AddToClassList(USS01);
        label_texts.AddToClassList(USS02);

        return label_texts;
    }

    protected IntegerField GetNewIntegerField(Container_Int container, string USS01 = "", string USS02 = "")
    {
        IntegerField integerField = new IntegerField();

        integerField.RegisterValueChangedCallback(value =>
        {
            container.Value = value.newValue;
        });
        integerField.SetValueWithoutNotify(container.Value);

        integerField.AddToClassList(USS01);
        integerField.AddToClassList(USS02);

        return integerField;
    }

    protected Button GetNewButton(string btnText,string USS01="",string USS02="")
    {
        Button btn = new Button()
        {
            text = btnText,
        };

        btn.AddToClassList(USS01);
        btn.AddToClassList(USS02);

        return btn;
    }

    protected FloatField GetNewFloatField(Container_Float container,string USS01="",string USS02="")
    {
        FloatField floatField = new FloatField();

        floatField.RegisterValueChangedCallback(value =>
        {
            container.Value = value.newValue;
        });
        floatField.SetValueWithoutNotify(container.Value);

        floatField.AddToClassList(USS01);
        floatField.AddToClassList(USS02);

        return floatField;
    }

    protected Image GetNewImage(Container_Sprite container,string USS01="",string USS02="")
    {
        Image image = new Image();

        image.AddToClassList(USS01);
        image.AddToClassList(USS02);

        return image;
    }

    protected ObjectField GetNewObjectField_Audio(AudioClip audioClip, string USS01 = "", string USS02 = "")
    {
        ObjectField objectField = new ObjectField()
        {
            objectType = typeof(AudioClip),
            allowSceneObjects = false,
            value = audioClip,
        };

        objectField.RegisterValueChangedCallback(value =>
        {
            audioClip = value.newValue as AudioClip;
        });
        objectField.SetValueWithoutNotify(audioClip);

        objectField.AddToClassList(USS01);
        objectField.AddToClassList(USS02);

        return objectField;
    }

    protected TextField GetNewTextField(Container_String container,string placeholderText,string USS01="",string USS02="")
    {
        TextField textField = new TextField();

        textField.RegisterValueChangedCallback(value =>
        {
            container.Value = value.newValue;
        });
        textField.SetValueWithoutNotify(container.Value);

        textField.AddToClassList(USS01);
        textField.AddToClassList(USS02);

        SetPlaceholderText(textField, placeholderText);

        return textField;
    }

    protected void SetPlaceholderText(TextField textField,string placeholder)
    {
        string placeholderClass = TextField.ussClassName + "__placeholder";

        CheckForText();
        OnFocusOut();
        textField.RegisterCallback<FocusInEvent>(evt => OnFocusIn());
        textField.RegisterCallback<FocusOutEvent>(evt => OnFocusOut());

        void OnFocusIn()
        {
            if(textField.ClassListContains(placeholderClass))
            {
                textField.value = string.Empty;
                textField.RemoveFromClassList(placeholderClass);
            }
        }

        void OnFocusOut()
        {
            if(string.IsNullOrEmpty(textField.text))
            {
                textField.value = placeholder;
                textField.AddToClassList(placeholderClass);
            }
        }

        void CheckForText()
        {
            if(!string.IsNullOrEmpty(textField.text))
            {
                textField.RemoveFromClassList(placeholderClass);
            }
        }
    }

    protected ObjectField GetNewObjectField_Sprite(Sprite sprite,Image imagePreview,string USS01 = "", string USS02 = "")
    {
        ObjectField objectField = new ObjectField()
        {
            objectType = typeof(Sprite),
            allowSceneObjects = false,
            value=sprite,
        };

        objectField.RegisterValueChangedCallback(value =>
        {
            sprite = value.newValue as Sprite;
            imagePreview.image = sprite != null ? sprite.texture: null; 
        });
        objectField.SetValueWithoutNotify(sprite);
        imagePreview.image = sprite != null ? sprite.texture : null;

        objectField.AddToClassList(USS01);
        objectField.AddToClassList(USS02);

        return objectField;
    }

    protected ObjectField GetNewObjectField_ConditionCheck(Container_ConditionCheckSO container, string USS01 = "", string USS02 = "")
    {
        ObjectField objectField = new ObjectField()
        {
            objectType = typeof(ConditionCheckSO),
            allowSceneObjects = false,
            value = container.Value,
        };

        objectField.RegisterValueChangedCallback(value =>
        {
            container.Value = value.newValue as ConditionCheckSO;
        });
        objectField.SetValueWithoutNotify(container.Value);

        objectField.AddToClassList(USS01);
        objectField.AddToClassList(USS02);

        return objectField;
    }

    protected EnumField GetNewEnumField_ConditionAddType(Container_ConditionAddType container, Action action, string USS01 = "", string USS02 = "")
    {
        EnumField enumField = new EnumField()
        {
            value = container.Value
        };
        enumField.Init(container.Value);

        enumField.RegisterValueChangedCallback(value =>
        {
            container.Value = (ConditionAddType)value.newValue;
            action?.Invoke();
        });
        enumField.SetValueWithoutNotify(container.Value);

        enumField.AddToClassList(USS01);
        enumField.AddToClassList(USS02);

        container.EnumField = enumField;

        return enumField;
    }

    public void AddConditionCheck(List<Container_ConditionCheck> conditionChecks,Container_ConditionCheck container=null)
    {
        Container_ConditionCheck newConditionCheck = new Container_ConditionCheck();
        if(container!=null)
        {
            newConditionCheck.Container_ConditionCheckSO.Value= container.Container_ConditionCheckSO.Value;
            newConditionCheck.Container_ConditionAddType.Value = container.Container_ConditionAddType.Value;
        }
        conditionChecks.Add(newConditionCheck);

        Box conditionBox = new Box();
        Box firstBox = new Box();
        Box secondBox = new Box();

        firstBox.AddToClassList("ConditionCheckFirstBox");

        newConditionCheck.ObjectField = GetNewObjectField_ConditionCheck(newConditionCheck.Container_ConditionCheckSO, "ConditionCheckField");

        Button removeBtn = GetNewButton("X", "removeBtn");
        removeBtn.clicked += () =>
        {
            conditionChecks.Remove(newConditionCheck);

            if(conditionChecks.Count>0)
            {
                conditionChecks[conditionChecks.Count - 1].Container_ConditionAddType.EnumField.AddToClassList("Hide");
            }

            DeleteBox(conditionBox);
        };

        firstBox.Add(newConditionCheck.ObjectField);
        firstBox.Add(removeBtn);

        EnumField enumField = GetNewEnumField_ConditionAddType(newConditionCheck.Container_ConditionAddType, null);
        secondBox.Add(enumField);

        conditionBox.Add(firstBox);
        conditionBox.Add(secondBox);

        mainContainer.Add(conditionBox);

        RefreshExpandedState();
    }

    public Port AddInputPort(string name,Port.Capacity capacity=Port.Capacity.Multi)
    {
        Port inputPort = GetPortInstance(Direction.Input, capacity);
        inputPort.portName = name;
        inputContainer.Add(inputPort);

        return inputPort;
    }

    public Port AddOutPutPort(string name,Port.Capacity capacity=Port.Capacity.Single)
    {
        Port outputPort = GetPortInstance(Direction.Output, capacity);
        outputPort.portName = name;
        outputContainer.Add(outputPort);

        return outputPort;
    }

    public Port GetPortInstance(Direction nodeDirection,Port.Capacity capacity=Port.Capacity.Single)
    {
        return InstantiatePort(Orientation.Horizontal, nodeDirection, capacity,typeof(float));
    }

    public void DeleteBox(Box container)
    {
        mainContainer.Remove(container);
        RefreshExpandedState();
    }

    public virtual void LoadValueInField()
    {

    }
}
