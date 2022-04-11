using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System;

public enum LookDirection
{ 
   Up,Down,Left,Right

}


public class Piece : MonoBehaviour
{
    public int floorIndex;
    public Cell currentCell;
    public Cell preCell;

    protected SpriteRenderer sr;

    protected Animator anim;

    protected UnityAction animAction;

    public PieceActionQueue pieceActionQueue;

    //public bool hasAction;
    public int oriActionCount;
    public int actionCount;
    public bool hasMove;
    public int moveStepCount;
    public int counterAttackCount;

    protected PieceHealthBar pieceHealthBar;
    protected PieceTimeProcessor pieceTimeProcessor;

    protected ExtraAttackTip extraAttackTip;

    public PieceStatus pieceStatus;

    protected PieceBUFFList pieceBUFFList;

    public Func<Cell,Piece,bool> moveCheckFunc;
    public MoveType moveType;

    public PieceTimeProcessor PieceTimeProcessor { get => pieceTimeProcessor; }
    public PieceBUFFList PieceBUFFList { get => pieceBUFFList;}
    public List<Piece> Neighbours { get => neighbours;}

    [SerializeField]private List<Piece> neighbours=new List<Piece>();

    public Piece supportPiece;

    public virtual void SetBattlePos(Cell cell)
    {
        gameObject.SetActive(true);
        pieceActionQueue = new PieceActionQueue();
        
        /* currentCell = cell;
         cell.currentPiece = this;*/
        neighbours.Clear();
        SetPos(cell);
        SetIdleDirection(pieceStatus.lookDirection);
        actionCount = oriActionCount;
        PieceQueueManager.Instance.AddPiece(this);
    }

    public virtual void Init()
    {
        //获取组件      
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        pieceHealthBar = TransformHelper.GetChildTransform(transform, "PieceHealth").GetComponent<PieceHealthBar>();
        pieceTimeProcessor = GetComponent<PieceTimeProcessor>();
        extraAttackTip = TransformHelper.GetChildTransform(transform, "ExtraAttackTip").GetComponent<ExtraAttackTip>();
        pieceStatus = GetComponent<PieceStatus>();
        pieceBUFFList = GetComponent<PieceBUFFList>();
        moveCheckFunc = PieceMoveCheckFunc.GetMoveCheckFunc(moveType);

        //初始化组件
        pieceTimeProcessor.Init(this);
        pieceStatus.Init(this);
        extraAttackTip.Init();    
        pieceHealthBar.Init(this);
        pieceBUFFList.Init(this);
    }

    public virtual void SetPos(Cell cell)
    {
        if (currentCell!=null)
        {
            preCell = currentCell;
            currentCell.currentPiece = null;        
        }
        currentCell = cell;
        cell.currentPiece = this;
        transform.position = cell.transform.position + Vector3.up * (pieceStatus.isFly+1) * 0.6f;
        floorIndex = pieceStatus.isFly + cell.floorIndex;
        SortingLayerSlover.GetSortingLayerName(sr, floorIndex+1,0);
        OwnerSetNeighbourPieces();
    }

    public void ShowExtraAttackTip()
    {
        extraAttackTip.ShowExtraAttack();
    }

    public void CloseExtraAttackTip()
    {
        extraAttackTip.CloseExtraAttack();
    }


    /// <summary>
    /// 对外的运动接口
    /// </summary>
    /// <param name="cell"></param>
    public virtual void PieceMove(Cell cell)
    {
        StartCoroutine(DoPieceMove(cell,pieceActionQueue.DoPieceAction));
    }

    /// <summary>
    /// 人物的移动
    /// </summary>
    /// <param name="cell">终点格子</param>
    /// <returns></returns>
    protected  IEnumerator DoPieceMove(Cell cell,UnityAction action)
    {
        BattleManager.Instance.CancelExtraAttackPieceShow();
        currentCell.currentPiece = null;
        float speed = 2f;
        while (currentCell!=cell&&moveStepCount<pieceStatus.HMove)      
        {
            RunDirectorChange(currentCell.nextCell);
            Vector3 temp2 = currentCell.nextCell.transform.position + Vector3.up * (pieceStatus.isFly+1) * 0.6f;
            SortingLayerSlover.GetSortingLayerName(sr,currentCell.nextCell.floorIndex+1+pieceStatus.isFly, 0);
            if (currentCell.nextCell.floorIndex!= currentCell.floorIndex)
                transform.DOMoveY(temp2.y, 0.1f);
            while (Vector2.Distance(transform.position, temp2) > 0.01f)
            {
                yield return null;                
                transform.position = Vector2.MoveTowards(transform.position, temp2, Time.deltaTime * speed);               
            }
            transform.position = temp2;
            currentCell = currentCell.nextCell;
            if (currentCell.topCellAttachment != null)
            {
                currentCell.topCellAttachment.MoveOnEffect(this);
            }             
            floorIndex = currentCell.floorIndex + pieceStatus.isFly;
            moveStepCount++;
        };
        SetPos(currentCell);
        SetIdleDirection(pieceStatus.lookDirection);
        hasMove = true;
        PieceTimeProcessor.MovedTimeProcessorCheck(action);
    }

    public void SetNeigbourPieces()
    {
        neighbours = CellManager.Instance.GetPieceNeighbour(this);
    }

    public void OwnerSetNeighbourPieces()
    {
        foreach(var piece in neighbours)
        {
            piece.Neighbours.Remove(this);
        }
        SetNeigbourPieces();
        foreach(var piece in neighbours)
        {
            piece.neighbours.Add(this);
        }
    }

    /// <summary>
    /// 人物的运动动画的转变
    /// </summary>
    /// <param name="target"></param>
    protected void RunDirectorChange(Cell target)
    {
        anim.SetTrigger("Run");
        anim.SetFloat("Horizontal", target.rowIndex - currentCell.rowIndex);
        anim.SetFloat("Vertical", target.lineIndex - currentCell.lineIndex);
    }

    /// <summary>
    /// 用于idle状态的动画判断
    /// </summary>
    public void SetIdleDirection(LookDirection lookDirection)
    {
        pieceStatus.lookDirection = lookDirection;
        anim.SetTrigger("Idle");
        SetLookDirectionAction(lookDirection);
    }

    protected void SetLookDirectionAction(LookDirection lookDirection)
    {
        switch (lookDirection)
        {
            case LookDirection.Up:
                anim.SetFloat("Horizontal", 0);
                anim.SetFloat("Vertical", 1);
                break;
            case LookDirection.Down:
                anim.SetFloat("Horizontal", 0);
                anim.SetFloat("Vertical", -1);
                break;
            case LookDirection.Left:
                anim.SetFloat("Horizontal", -1);
                anim.SetFloat("Vertical", 0);
                break;
            case LookDirection.Right:
                anim.SetFloat("Horizontal", 1);
                anim.SetFloat("Vertical", 0);
                break;
            default:
                break;
        }
    }

    protected void SetFriendMove(Cell cell,bool AOR)
    {      
        if(currentCell.currentPiece!=null)
        {
            int temp = AOR ? 1 : -1;
            if (pieceStatus.lookDirection == LookDirection.Up || pieceStatus.lookDirection == LookDirection.Down)
            {
                currentCell.currentPiece.transform.position += new Vector3(0.25f, 0.15f, 0) * temp;
            }               
            else
            {
                currentCell.currentPiece.transform.position += new Vector3(0.25f, -0.15f, 0) * temp;
            }
        }
    }

    public void ReturnToIdle()
    {
        SetIdleDirection(pieceStatus.lookDirection);
    }

    public void PieceActionAnimEvent()
    {
        animAction?.Invoke();
    }

    public void DoPieceAnim(string animName,UnityAction action)
    {
        animAction = action;     
        anim.SetTrigger(animName);
    }



    protected void PieceHealthDown(int changeValue)
    {
        HealthChangeNumText temp = PoolManager.Instance.GetObj("HealthChangeNumText").GetComponent<HealthChangeNumText>();
        temp.Init(true, this, -changeValue);

        if (changeValue<-10000)
        {
            changeValue += 10000;
        }

        if (pieceStatus.CurrentHealth > -changeValue)
        {
            PieceTimeProcessor.AfterBeAttackTimeProcessorCheck();
        }    

        if (pieceStatus.CurrentHealth< -changeValue)
            changeValue = -pieceStatus.CurrentHealth;

        pieceHealthBar.SetHealthDown(pieceStatus.CurrentHealth, changeValue, pieceStatus.maxHealth);
    }

    protected void PieceHealthUp(int changeValue)
    {
        HealthChangeNumText temp = PoolManager.Instance.GetObj("HealthChangeNumText").GetComponent<HealthChangeNumText>();
        temp.Init(false, this, changeValue);

        changeValue = pieceStatus.CurrentHealth + changeValue > pieceStatus.maxHealth ? pieceStatus.maxHealth-pieceStatus.CurrentHealth: changeValue;
        pieceHealthBar.SetHealthUp(pieceStatus.CurrentHealth, changeValue, pieceStatus.maxHealth);
    }

    public void PieceHealthChange(int changeValue)
    {
        if (changeValue<0)
            PieceHealthDown(changeValue);
        else
            PieceHealthUp(changeValue);
    }

    public virtual void PieceDie()
    {
        StartCoroutine(DoPieceDie());
    }

    protected IEnumerator DoPieceDie()
    {
        currentCell.currentPiece = null;
        PieceQueueManager.Instance.RemoveDiePiece(this);
        foreach (var piece in neighbours)
        {
            piece.Neighbours.Remove(this);
        }
        transform.GetComponent<SpriteRenderer>().DOFade(0, 0.3f);
        yield return new WaitForSeconds(0.4f);
        gameObject.SetActive(false);
    }

    public void PieceActionCountDown()
    {
        actionCount--;
    }

}
