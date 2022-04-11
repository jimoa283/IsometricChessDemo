using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class MyDialogueNodeSearchWindow : ScriptableObject, ISearchWindowProvider
{
    private MyDialogueEditorWindow editorWindow;
    private MyDialogueGraphView graphView;

    private Texture2D iconImage;

    public void Init(MyDialogueEditorWindow editorWindow,MyDialogueGraphView graphView)
    {
        this.editorWindow = editorWindow;
        this.graphView = graphView;

        iconImage = new Texture2D(1, 1);
        iconImage.SetPixel(0, 0, new Color(0, 0, 0, 0));
        iconImage.Apply();

    }

    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> tree = new List<SearchTreeEntry>
        {
            new SearchTreeGroupEntry(new GUIContent("Dialogue Editor"),0),
            new SearchTreeGroupEntry(new GUIContent("Dialogue Node"),1),

            AddNodeSearch("Start",new StartNode()),
            AddNodeSearch("End",new EndNode()),
            AddNodeSearch("Branch",new BranchNode()),
            AddNodeSearch("Dialogue",new DialogueNode()),
            AddNodeSearch("Choice",new ChoiceNode()),
            AddNodeSearch("Event",new EventNode()),
        };

        return tree;
    }

    private SearchTreeEntry AddNodeSearch(string name,BaseNode baseNode)
    {
        SearchTreeEntry tmp = new SearchTreeEntry(new GUIContent(name, iconImage))
        {
            level = 2,
            userData = baseNode
        };

        return tmp;
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        Vector2 mousePosition = editorWindow.rootVisualElement.ChangeCoordinatesTo
           (
               editorWindow.rootVisualElement.parent, context.screenMousePosition - editorWindow.position.position
           );

        Vector2 graphMousePosition = graphView.contentViewContainer.WorldToLocal(mousePosition);

        return CheckNodeType(SearchTreeEntry, graphMousePosition);
    }

    private bool CheckNodeType(SearchTreeEntry searchTreeEntry,Vector2 position)
    {
        switch(searchTreeEntry.userData)
        {
            case StartNode node:
               graphView.AddElement(graphView.CreateStartNode(position));
                return true;
            case EndNode node:
               graphView.AddElement(graphView.CreateEndNode(position));
                return true;
            case BranchNode node:
               graphView.AddElement(graphView.CreateBranchNode(position));
                return true;
            case DialogueNode node:
               graphView.AddElement(graphView.CreateDialogueNode(position));
                return true;
            case ChoiceNode node:
               graphView.AddElement(graphView.CreateChoiceNode(position));
                return true;
            case EventNode node:
               graphView.AddElement(graphView.CreateEventNode(position));
                return true;
            default:
                break;
        }
        return false;
    }
}
