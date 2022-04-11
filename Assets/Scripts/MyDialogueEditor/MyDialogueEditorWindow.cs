using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor.Callbacks;
using System;

public class MyDialogueEditorWindow : EditorWindow
{
    private MyDialogueContainerSO currentDialogueContainerSO;
    private MyDialogueGraphView graphView;
    private MyDialogueSaveAndLoad saveAndLoad;

    private Label nameOfDialogueContainer;
    private ObjectField dialogueContainerField;
    private string editorWindowStyleSheet="MyUSS/EditorWindow/MyEditorWindowStyleSheet";

    /// <summary>
    /// 使双击对话资源能直接打开对话编辑器
    /// </summary>
    /// <param name="instanceId"></param>
    /// <param name="line"></param>
    /// <returns></returns>
    [OnOpenAsset(1)]    //using UnityEditor.Callbacks
    public static bool ShowWindow(int instanceId,int line)                    //使用OnOpenAsset限制，参数必须是（int，int），且返回值要是bool
    {
        UnityEngine.Object item = EditorUtility.InstanceIDToObject(instanceId);        // 获得双击的资源

        if(item is MyDialogueContainerSO)
        {
            MyDialogueEditorWindow window =(MyDialogueEditorWindow)GetWindow(typeof(MyDialogueEditorWindow));
            window.titleContent = new GUIContent("My Dialogue Editor");
            window.currentDialogueContainerSO = item as MyDialogueContainerSO;
            window.minSize = new Vector2(500, 500);
            window.Load();
        }

        return false;
    }

    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolbar();
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(graphView);
    }

    /// <summary>
    /// 创建视图
    /// </summary>
    private void ConstructGraphView()
    {
        graphView = new MyDialogueGraphView(this);
        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);

        saveAndLoad = new MyDialogueSaveAndLoad(graphView);
    }

    /// <summary>
    /// 创建顶部栏
    /// </summary>
    private void GenerateToolbar()
    {
        StyleSheet styleSheet = Resources.Load<StyleSheet>(editorWindowStyleSheet);        
        rootVisualElement.styleSheets.Add(styleSheet);                                       //EditorWindow的StyleSheet的使用注意点

        Toolbar bar = new Toolbar();

        {
            Button saveBtn = new Button()
            {
                text = "Save",
            };
            saveBtn.clicked += Save;
            bar.Add(saveBtn);
        }

        {
            Button loadBtn = new Button()
            {
                text = "Load",
            };
            loadBtn.clicked += Load;
            bar.Add(loadBtn);
        }

        dialogueContainerField = new ObjectField()
        {
            objectType = typeof(MyDialogueContainerSO),
            allowSceneObjects = false,
            value=currentDialogueContainerSO,
        };
        dialogueContainerField.RegisterValueChangedCallback(value =>
        {
            currentDialogueContainerSO = value.newValue as MyDialogueContainerSO;
            Load();
        });
        bar.Add(dialogueContainerField);

        nameOfDialogueContainer = new Label("");
        bar.Add(nameOfDialogueContainer);
        nameOfDialogueContainer.AddToClassList("nameofDialogueContainer");

        rootVisualElement.Add(bar);
    }

    private void Save()
    {
        saveAndLoad.Save(currentDialogueContainerSO);
    }

    private void Load()
    {
        if(currentDialogueContainerSO!=null)
        {
            dialogueContainerField.SetValueWithoutNotify(currentDialogueContainerSO);
            nameOfDialogueContainer.text = "Name: " + currentDialogueContainerSO.name;
            saveAndLoad.Load(currentDialogueContainerSO);
        }       
    }
}
