using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public string cellName;

    public int rowIndex;  //列数
    public int lineIndex;  //行数
    public int floorIndex;  //层数

    /// <summary>
    /// 用于显示范围的洪水的记数
    /// </summary>
    public int hCount=0;
    public int vCount=0;

    /// <summary>
    /// 当前格子能否移动到
    /// </summary>
    public bool canMove;

    public bool canAttack;

    /// <summary>
    /// 当前格子上的棋子
    /// </summary>
    public Piece currentPiece;

    /// <summary>
    /// 当前格子的显示用瓦片
    /// </summary>
    public ShowRange range;

    /// <summary>
    /// 用于显示范围时当前格子是否已检测过
    /// </summary>
    public bool isHide;

    /// <summary>
    /// 用于显示路径时，当前格子是否已经被检测过
    /// </summary>
    public bool hasPathChecked;

    /// <summary>
    /// 用于显示路径时，记录当前格子是被哪个格子检测的
    /// </summary>
    public Cell preCell;

    /// <summary>
    /// 用于显示路径时，连接路径的下一个格子
    /// </summary>
    public Cell nextCell;

    public Cell upHTransferCell;
    public Cell downHTransferCell;
    public Cell leftHTransferCell;
    public Cell rightHTransferCell;

    /// <summary>
    /// 显示路径用的物体
    /// </summary>
    public ShowRange pathObj;

    public ShowRange effectRange;

    public bool cantMove;

    public CellAttachment lowCellAttachment;
    public CellAttachment topCellAttachment;

    public Pocket pocket;

    [TextArea]
    public string cellInfo;

    public AttachmentPropertyType propertyType;

    public List<Enemy> riskEnemyList = new List<Enemy>();

    private void Start()
    {
        Init();
    }

   #if UNITY_EDITOR
    public void HideSelf()
    {
        if (isHide)
            return;
        isHide = true;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
        
        if (transform.childCount>0)
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
    }

    public void ShowSelf()
    {
        if (!isHide)
            return;
        GetComponent<SpriteRenderer>().color = Color.white;

        if (transform.childCount > 0)
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
    }
   #endif

    protected virtual void Init()
    {
        if(BattleGameManager.Instance.isInBattleScene)
        {
            CellManager.Instance.AddCell(new Vector2(rowIndex, lineIndex), this);   //把自身登记到CellManager上
            vCount = 0;
            hCount = 0;
            topCellAttachment = GetComponentInChildren<CellAttachment>();
            if (topCellAttachment != null)
                topCellAttachment.FirstSetCell(this);
        }       
    }

    public void ShowEnemyLine()
    {
        if (!canMove)
            return;

        foreach (var enemy in riskEnemyList)
        {
            enemy.SetHostilityLine(Select.Instance.transform.position + new Vector3(0, 0.3f, 0));
        }
    }

    public void RemoveRiskEnemy(Enemy enemy)
    {
        riskEnemyList.Remove(enemy);
    }

    
}
