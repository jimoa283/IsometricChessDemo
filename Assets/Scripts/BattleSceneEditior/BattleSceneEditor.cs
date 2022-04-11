using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum BattleSceneEditorMode
{
    Explore,
    Battle
}

public enum SetCellNumType
{
    Single,
    Range
}

public enum SetObjType
{
    Cell,
    CellAttachment
}

[ExecuteInEditMode]
public class BattleSceneEditor : EditorWindow
{
    [SerializeField] private  int floorIndex;
    [SerializeField] private  Vector3 startPos=new Vector3(0,-5.6f,0);
    [SerializeField] private  int maxRowNum;
    [SerializeField] private  int maxLineNum;
    [SerializeField] private SetObjType setObjType;

    private  Vector3 rowUpMove = new Vector3(-0.5f, 0.3f);
    private  Vector3 lineUpMove = new Vector3(0.5f, 0.3f);
    private  Vector3 floorUpMove = Vector3.up * 0.6f;

    private  GameObject editorCell;
    private  List<GameObject> editorCellList;
    private GameObject setObj;

    private string[] floorIndexs;

    private Transform ground;
    private Transform editorCellListT;
    public  GameObject tipCell;
    private EditorCell currentEditorCell;
    private bool isEditor;
    private Transform floor;
    private Dictionary<Vector3Int, Cell> cellDic;

    public int FloorIndex { get => floorIndex; 
       set
        {
            if(value!=floorIndex)
            {
                floorIndex = value;
                if (isEditor)
                    ChangeFloor();
            }
        }
    }

    public GameObject SetObj { get => setObj;
        set
        {
            if(value!=setObj)
            {
                if (setObjType == SetObjType.Cell && value != null)
                {
                    if (value.GetComponent<Cell>() == null)
                    {
                        Debug.LogError("Wrong Cell");
                        return;
                    }
                }
                ChangeSetCell(value);
                setObj = value;
            }
        }
    }

    [MenuItem("Window/BattleSceneEditor", false, 10000)]
    static void ShowWindow()
    {
        GetWindow<BattleSceneEditor>();
    }

    private void OnEnable()
    {
        titleContent.text = "BattleSceneEditor";
        minSize = new Vector2(100, 60);
        maxSize = new Vector2(4000, 60);
        editorCell = Resources.Load<GameObject>("Cell/EditorCell");
        
        isEditor = false;
        SceneView.duringSceneGui += OnSceneGUI;

        int length = SortingLayerSlover.sortingLayerName.Length;
        floorIndexs = new string[length];
        for (int i = 0; i < length; ++i)
        {
            floorIndexs[i] = i.ToString();
        }
    }

    [System.Obsolete]
    private void OnGUI()
    {
        using(new EditorGUILayout.VerticalScope())
        {
            FloorIndex = EditorGUILayout.Popup("FloorIndex", floorIndex, floorIndexs);

            setObjType = (SetObjType)EditorGUILayout.EnumPopup("SetObjType", setObjType);

            SetObj=(GameObject)EditorGUILayout.ObjectField("SetCell", setObj, typeof(GameObject));

            startPos = EditorGUILayout.Vector3Field("StartPos", startPos);
            maxRowNum = EditorGUILayout.IntField("MaxRowNum", maxRowNum);
            maxLineNum = EditorGUILayout.IntField("MaxLineNum", maxLineNum);

            if (GUILayout.Button("StartEditor"))
                StartEditor();

            if (GUILayout.Button("EndEditor"))
                EndEditor();

        }        
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (!isEditor||setObj==null)
            return;
        Event e = Event.current;
        
        if(e.type==EventType.MouseMove)
        {
            EditorCellCheck(sceneView, e);
        }
        
        if(e.type==EventType.MouseDown&&e.button==0)
        {
            switch (setObjType)
            {
                case SetObjType.Cell:
                    InitSingleSetCell(e);
                    break;
                case SetObjType.CellAttachment:
                    InitCellAttachment(e);
                    break;
                default:
                    break;
            }
        }       
    }



    private void StartEditor()
    {
        if (isEditor)
            return;
        isEditor = true;
        editorCellList = new List<GameObject>();
        GameObject temp = GameObject.Find("Ground");
        if (temp == null)
        {
            temp = new GameObject("Ground");
        }
        ground = temp.transform;

        cellDic = new Dictionary<Vector3Int,Cell>();

        Cell[] cellList = ground.GetComponentsInChildren<Cell>();

        foreach(var cell in cellList)
        {
            cellDic.Add(new Vector3Int(cell.rowIndex, cell.lineIndex, cell.floorIndex), cell);
        }

        ChangeFloor();
        ChangeSetCell(setObj);       
    }

    private void ChangeSetCell(GameObject obj)
    {
        if (!isEditor)
            return;
        if (tipCell != null)
            DestroyImmediate(tipCell);
        if (obj == null)
            return;
        tipCell = Instantiate(obj, editorCellListT);
        tipCell.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        tipCell.gameObject.SetActive(false);
    }

    private void ChangeFloor()
    {
        string floorName = SortingLayerSlover.floorIndexName[floorIndex];
        floor = ground.Find(floorName);
        if (floor == null)
        {
            floor = new GameObject(floorName).transform;
            floor.SetParent(ground);
        }
        

        editorCellListT = ground.Find("EditorCellList");
        if (editorCellListT == null)
        {
            editorCellListT = new GameObject("EditorCellList").transform;
            editorCellListT.SetParent(ground);
        }
        

        int sumNum = maxLineNum * maxRowNum;

        if (sumNum > editorCellList.Count)
        {
            for (int i = editorCellList.Count; i < sumNum; ++i)
            {
                editorCellList.Add(Instantiate(editorCell, editorCellListT));
            }
        }


        for (int i = 0; i < maxRowNum; ++i)
        {
            for (int j = 0; j < maxLineNum; ++j)
            {
                EditorCell tempEditorCell = editorCellList[i * maxLineNum + j].GetComponent<EditorCell>();
                tempEditorCell.ReSetEditorCell();
                Vector3Int tempPos = new Vector3Int(i, j, floorIndex);
                if (cellDic.ContainsKey(tempPos))
                    tempEditorCell.AddCellObj(cellDic[tempPos]);
                tempEditorCell.transform.position = startPos + j * rowUpMove + i * lineUpMove + floorIndex * floorUpMove;
                tempEditorCell.Init(floorIndex, i, j);
            }
        }
    }

    private void EndEditor()
    {
        if (!isEditor)
            return;
        isEditor = false;
        if(editorCellListT!=null)
           DestroyImmediate(editorCellListT.gameObject);
        tipCell = null;
        cellDic = null;
        editorCellList.Clear();
    }

    private void EditorCellCheck(SceneView sceneView,Event e)
    {
        if(e.type==EventType.MouseMove)
        {
            Vector2 mousePosition = e.mousePosition;
            float h = EditorGUIUtility.pixelsPerPoint;
            mousePosition.y = sceneView.camera.pixelHeight - mousePosition.y * h;
            mousePosition.x *= h;
            Vector2 mousePos = sceneView.camera.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if(hit.collider!=null&& hit.transform.CompareTag("EditorCell"))
            {
                currentEditorCell = hit.transform.GetComponent<EditorCell>();
                switch (setObjType)
                {
                    case SetObjType.Cell:
                      if(SetCellShowCheck(currentEditorCell))
                        {
                            //HideNeighbourCell();
                            currentEditorCell.InitSetCellShow(tipCell, 0, Vector3.up * 0.6f);
                        }                       
                        break;
                    case SetObjType.CellAttachment:
                      if(!cellDic.ContainsKey(new Vector3Int(currentEditorCell.rowIndex, currentEditorCell.lineIndex, floorIndex + 1)))
                        {
                            //HideNeighbourCell();
                            currentEditorCell.InitCellAttachmentShow(tipCell, 1, Vector3.up * 1.2f);
                        }                          
                        break;
                    default:
                        break;
                }                                     
            }
            else
            {
                currentEditorCell = null;
                tipCell.SetActive(false);
            }
        }
    }

    private bool SetCellShowCheck(EditorCell cell)
    {
        if (currentEditorCell.cellObj != null)
            return false;

        Vector3Int tempPos = new Vector3Int(cell.rowIndex, cell.lineIndex, floorIndex-1);
        if (cellDic.ContainsKey(tempPos))
        {
            if (cellDic[tempPos].transform.GetChild(0) != null)
                return false;
        }

        return true;
    }

   /* private void HideNeighbourCell()
    {
        foreach(var cell in cellDic)
        {
            if(cell.Key.z>floorIndex)
            {
                cell.Value.HideSelf();
            }
            else if (cell.Key.z == floorIndex&&(Mathf.Abs(cell.Key.x-currentEditorCell.rowIndex)<=1&&Mathf.Abs(cell.Key.y-currentEditorCell.lineIndex)<=1))
            {
                cell.Value.HideSelf();
            }
            else
            {
                cell.Value.ShowSelf();
            }
        }
    }*/

    private void InitSingleSetCell(Event e)
    {
        if(currentEditorCell==null||!SetCellShowCheck(currentEditorCell))
            return;
        currentEditorCell.InitSetCell(setObj, floor);
        cellDic.Add(new Vector3Int(currentEditorCell.rowIndex, currentEditorCell.lineIndex, floorIndex), currentEditorCell.cellObj);
    }

    private void InitCellAttachment(Event e)
    {
        if (currentEditorCell == null||(cellDic.ContainsKey(new Vector3Int(currentEditorCell.rowIndex,currentEditorCell.lineIndex,floorIndex+1))))
            return;
        currentEditorCell.InitCellAttachment(setObj);
    }

    private void OnDestroy()
    {
        EndEditor();
        SceneView.duringSceneGui -= OnSceneGUI;
    }
}
