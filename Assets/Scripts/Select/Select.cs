using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum SelectStateID
{
    SetPieceState,
    ChangePlaySettingPosState,
    ChooseActionState,
    //FreezeState,
    ChooseActionPosState,
    ChooseOnlyMoveState,
    HideState
}

public class Select : MSingleton<Select>
{
    [SerializeField] private int rowIndex = 0;
    [SerializeField] private int lineIndex = 0;
    public int floorIndex = 0;

    /// <summary>
    /// 选择框当前的格子
    /// </summary>
    public Cell currentCell;

    public SelectState currentSelectState;
   
    /// <summary>
    /// 生成的临时棋子复制品
    /// </summary>
    public GameObject selectClonePiece;
    public GameObject selectCloneObj;

    private PieceActionSelectUI pieceActionSelectUI;

    private List<SelectState> selectStateList;

    private SpriteRenderer sr;

    /// <summary>
    /// 选择框能否移动
    /// </summary>
    public bool canMove;

    public bool hasSelectedPos;

    private float waitTimer;
    public float waitTime;

    private Piece changePiece;

    public int RowIndex
    {
        get => rowIndex;
        set
        {
            Cell temp = CellManager.Instance.GetCellByRL(value, lineIndex);
            if (temp != null)
            {
                currentSelectState.SetPos(temp);
            }

        }
    }

    public int LineIndex
    {
        get => lineIndex;
        set
        {
            Cell temp = CellManager.Instance.GetCellByRL(rowIndex, value);
            if (temp!=null)
            {
                currentSelectState.SetPos(temp);
            }
        }
    }

    public Piece ChangePiece { get => changePiece; }
    public PieceActionSelectUI PieceActionSelectUI { get => pieceActionSelectUI; }

    protected override void Init()
    {
        gameObject.SetActive(true);
        sr = GetComponent<SpriteRenderer>();
        selectStateList = new List<SelectState>();
        selectClonePiece = transform.Find("SelectClonePiece").gameObject;
        selectCloneObj = transform.Find("SelectCloneObj").gameObject;
        pieceActionSelectUI = transform.Find("SelectCanvas").GetComponent<PieceActionSelectUI>();
        pieceActionSelectUI.Init();

        selectStateList.Add(new SetPieceState(SelectStateID.SetPieceState, pieceActionSelectUI));
        selectStateList.Add(new ChooseActionState(SelectStateID.ChooseActionState, pieceActionSelectUI));
        selectStateList.Add(new ChooseActionPosState(SelectStateID.ChooseActionPosState, pieceActionSelectUI));
        selectStateList.Add(new HideState(SelectStateID.HideState, pieceActionSelectUI));
        selectStateList.Add(new ChangePlayerSettingPosState(SelectStateID.ChangePlaySettingPosState, pieceActionSelectUI));
    }

    private void Update()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.S))
                SelectMove(true, false);
            if (Input.GetKey(KeyCode.W))
                SelectMove(true, true);
            if (Input.GetKey(KeyCode.D))
                SelectMove(false, true);
            if (Input.GetKey(KeyCode.A))
                SelectMove(false, false);

            if (Input.GetKeyDown(KeyCode.J))
            {
                currentSelectState.ActionOnJ();
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                currentSelectState.ActionOnK();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                currentSelectState.ActionOnL();
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                currentSelectState.ActionOnI();
            }
        }     
            
    }


    public void ChangeSelectState(SelectStateID selectStateID)
    {
        foreach (var state in selectStateList)
        {
            if (state.SelectStateID == selectStateID)
            {
                Debug.Log(selectStateID);
                currentSelectState = state;
                state.OnEnter();
                return;
            }
        }
    }

    

    public void InitExtraPieceObj()
    {
        if (currentCell.currentPiece != null)
            return;

        Piece piece = LevelManager.Instance.CurrentPiece;
        SpriteRenderer sr = piece.GetComponent<SpriteRenderer>();

        ExtraFunction.CloneObj(piece.gameObject, selectClonePiece);
        ExtraFunction.CloneObj(gameObject, selectCloneObj);

        /*Vector3 temp = piece.transform.position;
        Vector3 temp2 = piece.currentCell.transform.position;
        Vector3 temp3= piece.pieceStatus.isFly?currentCell.transform.position + Vector3.up * 2 * 0.6f:currentCell.transform.position + Vector3.up  * 0.6f;
        piece.transform.position = temp3;

        if (piece.pieceStatus.isFly)
             SortingLayerSlover.GetSortingLayerName(sr,currentCell.floorIndex + 2,0);
        else
             SortingLayerSlover.GetSortingLayerName(sr,currentCell.floorIndex + 1,0);*/

        Vector3 temp = piece.transform.position;
        Vector3 temp2 = piece.currentCell.transform.position;

        piece.SetPos(currentCell);


        selectClonePiece.transform.position = temp;
        selectCloneObj.transform.position = temp2;

        selectCloneObj.transform.SetParent(null);
        selectClonePiece.transform.SetParent(selectCloneObj.transform);

        piece.pieceActionQueue.AddPieceAction(MoveConfirm);

        OpenSelectAction(true);
    }

    public void OpenSelectAction(bool isContinue)
    {
        Piece piece = LevelManager.Instance.CurrentPiece;

        /*piece.preCell = piece.currentCell;
        piece.currentCell = currentCell;
        piece.preCell.currentPiece = null;
        piece.currentCell.currentPiece = piece;*/
        if(!isContinue)
           piece.SetPos(currentCell);

        piece.GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 0.3f);

        RangeManager.Instance.CloseMoveRange();

        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.PieceActionUI));      

        //ChangeSelectState(SelectStateID.FreezeState);
        Freeze();
    }

   /* public void OpenPieceInfo()
    {
        canMove = false;
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.PieceInformationUI));
        pieceActionSelectUI.gameObject.SetActive(false);
    }*/

    private void ActionFinshOri()
    {
        PathManager.Instance.ClearPath();
        LevelManager.Instance.ChangeLevelState(LevelStateID.SetPieceDirection);
        gameObject.SetActive(false);
    }

    public void ActionFinishFirst()
    {     
        RangeManager.Instance.CloseMoveRange();
        ActionFinshOri();
    }

    public void ActionFinishOnActionUI()
    {      
        RangeManager.Instance.CloseAttackRange();
        UIManager.Instance.PopPanel(UIPanelType.PieceActionUI);
        LevelManager.Instance.CurrentPiece.GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 0);
        ActionFinshOri();
    }

    public void ExitActionSelect()
    {
        LevelManager.Instance.CurrentPiece.GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 0);
        RangeManager.Instance.CloseAttackRange();
    }

    private void CancelObj()
    {
        Piece piece = LevelManager.Instance.CurrentPiece;
        //Cell cell = piece.currentCell;

        /*piece.currentCell.currentPiece = null;
        piece.currentCell = piece.preCell;
        piece.currentCell.currentPiece = piece;
        piece.preCell = cell;
        

        piece.transform.position = piece.pieceStatus.isFly ? piece.currentCell.transform.position + Vector3.up * 2 * 0.6f : piece.currentCell.transform.position + Vector3.up * 0.6f;*/
        //cell.currentPiece = null;
        piece.SetPos(piece.preCell);
        //piece.preCell = cell;


        hasSelectedPos = false;      

        selectClonePiece.SetActive(false);
        selectCloneObj.SetActive(false);

        selectCloneObj.transform.SetParent(transform);
        selectClonePiece.transform.SetParent(transform);

        ExitActionSelect();
    }

    public void ReturnObj()
    {
        CancelObj();
        LevelManager.Instance.CurrentPiece.pieceActionQueue.RemovePieceAction();
        SetPos(LevelManager.Instance.CurrentPiece.currentCell);
        PathManager.Instance.ClearPath();
        RangeManager.Instance.ShowPlayerMoveRange();
        ChangeSelectState(SelectStateID.ChooseActionState);
    }

    public void MoveConfirm()
    {
        CancelObj();
        PathManager.Instance.ClearPath();
        LevelManager.Instance.CurrentPiece.PieceMove(LevelManager.Instance.CurrentPiece.preCell);
        gameObject.SetActive(false);
        //gameObject.SetActive(false);     
        //Freeze();
    }

    public void MoveOnly()
    {
        RangeManager.Instance.CloseMoveRange();
        PathManager.Instance.ClearPath();
        LevelManager.Instance.CurrentPiece.PieceMove(currentCell);
    }

    private void SelectMove(bool UOR,bool AOS)
    {
        if(waitTimer<Time.time)
        {
            if (UOR)
            {
                if (AOS)
                    LineIndex++;
                else
                    LineIndex--;
            }
            else
            {
                if (AOS)
                    RowIndex++;
                else
                    RowIndex--;
            }
            waitTimer = Time.time + waitTime;
        }
    }

  

    public void SetPosSimple(Cell cell)
    {
        currentCell = cell;
        rowIndex = cell.rowIndex;
        lineIndex = cell.lineIndex;
        transform.position = cell.transform.position;
        floorIndex = currentCell.floorIndex + 1;
        SortingLayerSlover.GetSortingLayerName(sr, floorIndex-1,0);
        sr.sortingOrder = (int)(sr.gameObject.transform.position.y * -10) + 4;
        EventCenter.Instance.EventTrigger<int>("ShowSelectPieceUIPlace", PieceQueueManager.Instance.GetPieceActionIndex(cell.currentPiece));
        EventCenter.Instance.EventTrigger<Cell>("ShowCellInfo",currentCell);
    }

    public void ChangePieceDirection()
    {
        Piece piece = LevelManager.Instance.CurrentPiece;
        if(currentCell.rowIndex==piece.currentCell.rowIndex)
        {
            if (currentCell.lineIndex > piece.currentCell.lineIndex&&piece.pieceStatus.lookDirection!=LookDirection.Up)
            {
                piece.SetIdleDirection(LookDirection.Up);
            }
            else if(currentCell.lineIndex<piece.currentCell.lineIndex&&piece.pieceStatus.lookDirection!=LookDirection.Down)
            {
                piece.SetIdleDirection(LookDirection.Down);
            }
        }
        else if(currentCell.lineIndex==piece.currentCell.lineIndex)
        {
            if (currentCell.rowIndex > piece.currentCell.rowIndex && piece.pieceStatus.lookDirection != LookDirection.Right)
            {
                piece.SetIdleDirection(LookDirection.Right);
            }
            else if (currentCell.rowIndex < piece.currentCell.rowIndex && piece.pieceStatus.lookDirection != LookDirection.Left)
            {
                piece.SetIdleDirection(LookDirection.Left);
            }
        }
    }

    public void SetPosBeforeBattle(Cell cell)
    {
        SetPosSimple(cell);
        //pieceActionSelectUI.SetActiveSelectBeforeBattle(cell);
    }

    /// <summary>
    /// 设置当前选择框的位置和当前格子等等
    /// </summary>
    /// <param name="temp"></param>
    public void SetPos(Cell temp)
    {
        SetPosSimple(temp);
        //pieceActionSelectUI.SetActiveSelect(currentCell);
        //pieceActionSelectUI.SetActiveSelect(temp);
        ChangePieceDirection();
        PathManager.Instance.TheShortestPath(LevelManager.Instance.CurrentPiece.currentCell, currentCell,LevelManager.Instance.CurrentPiece);
    }

    public void StartAction()
    {
        ExitActionSelect();
        BattleManager.Instance.CancelExtraAttackPieceShow();
        EffectRangeManager.Instance.ResigterAction();
        LevelManager.Instance.CurrentPiece.pieceActionQueue.AddPieceAction(BattleManager.Instance.StartBattle);
        LevelManager.Instance.CurrentPiece.pieceActionQueue.DoPieceAction();
        //EffectRangeManager.Instance.ClearEffectRangeDisplay();
        EffectRangeManager.Instance.ClearEffectRange();
        gameObject.SetActive(false);
    }

    public void SetPosForAction(Cell temp)
    {
        SetPosSimple(temp);
        //pieceActionSelectUI.SetActiveSelect(currentCell);
        ChangePieceDirection();
        EffectRangeManager.Instance.CreateEffectRange(currentCell, LevelManager.Instance.CurrentPiece);
        pieceActionSelectUI.SetActiveSelect(temp);
    }

    public void WakeUp()
    {
        canMove = true;
        pieceActionSelectUI.gameObject.SetActive(true);
    }

    public void Freeze()
    {
        canMove = false;
        pieceActionSelectUI.gameObject.SetActive(false);
    }

    public void SetChangePlayer()
    {
        changePiece = currentCell.currentPiece;
        //selectCloneObj.SetActive(true);
        ExtraFunction.CloneObj(gameObject, selectCloneObj);
        selectCloneObj.transform.position = currentCell.transform.position;
        selectCloneObj.transform.SetParent(null);
        ChangeSelectState(SelectStateID.ChangePlaySettingPosState);
    }

    public void CancelChangePlayerPos()
    {
        changePiece = null;
        selectCloneObj.SetActive(false);
        ChangeSelectState(SelectStateID.SetPieceState);
    }

    public void ChangePlayerPos()
    {
        Piece beChangedpiece = currentCell.currentPiece;
        Cell oriCell = changePiece.currentCell;
        changePiece.SetPos(currentCell);
        beChangedpiece.SetPos(oriCell);
        CancelChangePlayerPos();
    }
}
