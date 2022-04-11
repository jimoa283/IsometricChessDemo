using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EditorCell : MonoBehaviour
{
    private int floorIndex;
    public int rowIndex;
    public int lineIndex;
    private SpriteRenderer sr;
    public Cell cellObj;
    [SerializeField]private GameObject cellAttachmentObj;

    private void InitSetObjShow(GameObject obj,int floorIndexUp,Vector3 Offset,int num)
    {
        obj.SetActive(true);
        obj.transform.position = transform.position + Offset;
        SpriteRenderer tipCellSr = obj.GetComponent<SpriteRenderer>();
        SortingLayerSlover.GetSortingLayerName(tipCellSr, floorIndex+floorIndexUp, num);
        tipCellSr.sortingOrder = (int)(obj.transform.position.y * -10);
    }

    public void AddCellObj(Cell cell)
    {
        cellObj = cell;
        if (cellObj.transform.childCount > 0)
        {
            cellAttachmentObj = cellObj.transform.GetChild(0).gameObject;
        }

    }

    public void ReSetEditorCell()
    {
        cellObj = null;
        cellAttachmentObj = null;
    }

    public void InitSetCellShow(GameObject obj, int floorIndexUp, Vector3 Offset)
    {
        if (cellObj != null)
            return;

        InitSetObjShow(obj, floorIndexUp, Offset,0);
    }

    public void InitCellAttachmentShow(GameObject obj, int floorIndexUp, Vector3 Offset)
    {
        if (cellObj == null)
            return;
        InitSetObjShow(obj, floorIndexUp, Offset,1);
    }


    public void InitSetCell(GameObject setCell,Transform floor)
    {
        /*if(cellObj!=null)
            return;*/

        cellObj = Instantiate(setCell, floor).GetComponent<Cell>();
        InitSetObjShow(cellObj.gameObject, 0, Vector3.up * 0.6f,0);
        cellObj.rowIndex = rowIndex;
        cellObj.lineIndex = lineIndex;
        cellObj.floorIndex = floorIndex;
    }

    public void InitCellAttachment(GameObject cellAttachment)
    {
        if(cellObj==null||cellAttachmentObj!=null)
            return;

        cellAttachmentObj = Instantiate(cellAttachment, cellObj.transform);
        InitSetObjShow(cellAttachmentObj, 1, Vector3.up * 1.2f,1);
    }

/*    public void InitExploreSetCell(GameObject setCell,Transform floor)
    {
        if (cellObj != null)
            return;
        cellObj= Instantiate(setCell, floor);
        InitSetObjShow(cellObj,0,Vector3.up*0.6f);      
    }*/

    public void Init(int floorIndex,int rowIndex,int lineIndex)
    {
        this.floorIndex = floorIndex;
        this.rowIndex = rowIndex;
        this.lineIndex = lineIndex;

        sr = GetComponent<SpriteRenderer>();
        SortingLayerSlover.GetSortingLayerName(sr, floorIndex, 0);
        sr.sortingOrder = (int)(transform.position.y * -10)-6;
    }
}
