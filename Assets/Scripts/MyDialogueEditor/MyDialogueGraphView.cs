using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class MyDialogueGraphView : GraphView
{
    private string graphViewStyleSheet="MyUSS/GraphView/MyGraphViewStyleSheet";
    private MyDialogueEditorWindow editorWindow;
    private MyDialogueNodeSearchWindow searchWindow;

    public MyDialogueGraphView(MyDialogueEditorWindow editorWindow)
    {
        this.editorWindow = editorWindow;

        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        StyleSheet styleSheet = Resources.Load<StyleSheet>(graphViewStyleSheet);
        styleSheets.Add(styleSheet);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        this.AddManipulator(new FreehandSelector());

        // 添加网格背景
        GridBackground background = new GridBackground();
        Insert(0, background);
        background.StretchToParentSize();

        AddSearchWindow();
    }

    private void AddSearchWindow()
    {
        searchWindow = ScriptableObject.CreateInstance<MyDialogueNodeSearchWindow>();
        searchWindow.Init(editorWindow, this);
        nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
    }

    /// <summary>
    /// 重写GetCompatiblePorts，添加节点连接的限制规则
    /// </summary>
    /// <param name="startPort"></param>
    /// <param name="nodeAdapter"></param>
    /// <returns></returns>
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        List<Port> compatiblePorts = new List<Port>();
        Port startPortView = startPort;

        // 不能连接自己
        // 不能和同一个节点连接
        // 入口不能和入口连，出口不能和出口连
        ports.ForEach((port) =>
        {
            Port portView = port;

        if (startPortView != portView && startPortView.node != portView.node&&startPortView.direction!=portView.direction&&startPortView.portColor==portView.portColor)
            {
                compatiblePorts.Add(port);
            }
        });

        return compatiblePorts;
    }

    public StartNode CreateStartNode(Vector2 position)
    {
        StartNode node = new StartNode(position, editorWindow, this);
        return node;
    }

    public EndNode CreateEndNode(Vector2 position)
    {
        EndNode node = new EndNode(position, editorWindow, this);
        return node;
    }

    public BranchNode CreateBranchNode(Vector2 position)
    {
        BranchNode node = new BranchNode(position, editorWindow, this);
        return node;
    }

    public DialogueNode CreateDialogueNode(Vector2 position)
    {
        DialogueNode node = new DialogueNode(position, editorWindow, this);
        return node;
    }

    public ChoiceNode CreateChoiceNode(Vector2 position)
    {
        ChoiceNode node = new ChoiceNode(position, editorWindow, this);
        return node;
    }

    public EventNode CreateEventNode(Vector2 position)
    {
        EventNode node = new EventNode(position, editorWindow, this);
        return node;
    }
}
