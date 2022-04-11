using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum AttachmentFloorType
{
    Top,
    Low
}

public class CellAttachment : MonoBehaviour
{
    public Cell cell;
    public int oriTime;
    [SerializeField]private int time;
    private SpriteRenderer sr;
    public string attachmentName;
    public bool isEnetrnity;
    public bool canRemove;
    public AttachmentPropertyType propertyType;
    public AttachmentFloorType floorType;

    [TextArea]
    public string attachmenInfo;

    public int attachmentPower;
    public int attachmentHitRate;
    public int attachmentDefense;
    public int attachmentSpeed;
    public int attachmentMagic;
    public int attachmentAvoid;
    public int attachmentMagicDefense;
    public int attachmentVMove;
    public int attachmentLucky;
    public int attachmentHMove;
    public int attachmentCritical;
    public int attachmentFireResistance;
    public int attachmentIceResistance;
    public int attachmentWindResistance;
    public int attachmentThunderResistance;
    public int attachmentLightResistance;
    public int attachmentDarkResistance;

    public virtual void Init()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.white;
    }

    public virtual void FirstSetCell(Cell cell)
    {
        Init();
        this.cell = cell;
        transform.SetParent(cell.transform);
        transform.position = cell.transform.position + Vector3.up * 0.6f;
        time = oriTime;
        ChangeToTop();
    }

    public virtual void InitByFloorType(Cell cell)
    {

    }

    public virtual void ChangeToTop()
    {
        AddEffect(cell.currentPiece);
        cell.topCellAttachment = this;
        SortingLayerSlover.GetSortingLayerName(sr, cell.floorIndex + 1, 0);
        CellManager.Instance.CellAttachmentList.Add(this);
    }

    public virtual void ChangeToLow()
    {
        RemoveEffect(cell.currentPiece);
        cell.lowCellAttachment = this;
        SortingLayerSlover.GetSortingLayerName(sr, cell.floorIndex + 1, 0);
        CellManager.Instance.CellAttachmentList.Remove(this);
    }

    public virtual void TurnStartEffect(Piece piece)
    {

    }

    public virtual void TimeCountDown()
    {
        TurnStartEffect(cell.currentPiece);
        if(!isEnetrnity)
        {
            time--;
            if (time == 0)
            {
                CancelBlock();
            }
        }   
    }

    public virtual void MoveOnEffect(Piece piece)
    {
        
    }

    public virtual void AddEffect(Piece piece)
    {

    }

    public virtual void RemoveEffect(Piece piece)
    {
        CellManager.Instance.CellAttachmentList.Remove(this);
    }

    public void RemoveImmediate()
    {
        RemoveEffect(cell.currentPiece);
        //cell.topCellAttachment = null;
        PoolManager.Instance.PushObj(attachmentName, gameObject);
    }

    public virtual void CancelBlock()
    {
        RemoveEffect(cell.currentPiece);
        StartCoroutine(DoCancel());
    }

    IEnumerator DoCancel()
    {
        sr.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.55f);
        PoolManager.Instance.PushObj(attachmentName, gameObject);
    }

    public virtual string RealAttachmentInfo()
    {
        if(!isEnetrnity)
        {
            return attachmenInfo + "\n" + time + "单位行动后自行消失";
        }
        return attachmenInfo;
    }

   #if UNITY_EDITOR
    private void OnDestroy()
    {
        if (cell == null)
            return;
        cell.topCellAttachment = null;
        cell = null;
    }
    #endif
}
