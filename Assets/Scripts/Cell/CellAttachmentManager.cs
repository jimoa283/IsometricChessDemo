using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttachmentPropertyType
{
    Grass,
    Ice,
    Fire,
    Stone,
    Null
}

public class CellAttachmentManager : Singleton<CellAttachmentManager>
{
    public List<string> fireAttachmentList=new List<string>() { "Fire" };
    public List<string> iceAttachmentList=new List<string>() {"IceBlock"};
    public List<string> grassAttachmentList=new List<string>() { };
    public List<string> stoneAttachmentList=new List<string>() {"Embers" };
    public List<string> nullAttachmentList=new List<string>() { };

    public void AddCellAttachment(Cell cell,string attachmentName)
    {
        if(fireAttachmentList.Contains(attachmentName))
        {
            AddFireCellAttachment(cell, attachmentName);
        }
        else if(iceAttachmentList.Contains(attachmentName))
        {
            AddIceCellAttachment(cell, attachmentName);
        }
    }

    public void AddFireCellAttachment(Cell cell,string attachmentName)
    {
        if(cell.topCellAttachment!=null)
        {
            if (!cell.topCellAttachment.canRemove)
                return;

            switch (cell.topCellAttachment.propertyType)
            {
                case AttachmentPropertyType.Grass:
                    ChangeTopToAddAttachment(cell, attachmentName);
                    break;
                case AttachmentPropertyType.Ice:
                    OnlyRemoveTop(cell);
                    break;
                case AttachmentPropertyType.Fire:
                    break;
                case AttachmentPropertyType.Stone:
                    break;
                case AttachmentPropertyType.Null:
                    break;
                default:
                    break;
            }
        }
        else
        {
            if(cell.propertyType==AttachmentPropertyType.Grass)
            {
                OnlyAddAttachment(cell, attachmentName);
            }
        }
    }

    public void AddIceCellAttachment(Cell cell,string attachmentName)
    {
        if(cell.topCellAttachment!=null)
        {
            if (!cell.topCellAttachment.canRemove)
                return;
            switch (cell.topCellAttachment.propertyType)
            {
                case AttachmentPropertyType.Grass:
                    ChangeTopToAddAttachment(cell,attachmentName);
                    break;
                case AttachmentPropertyType.Ice:
                    break;
                case AttachmentPropertyType.Fire:
                    OnlyRemoveTop(cell);
                    break;
                case AttachmentPropertyType.Stone:
                    ChangeTopToAddAttachment(cell, attachmentName);
                    break;
                case AttachmentPropertyType.Null:
                    break;
                default:
                    break;
            }
        }
        else
        {
            if(cell.propertyType==AttachmentPropertyType.Grass||cell.propertyType==AttachmentPropertyType.Stone)
            {
                OnlyAddAttachment(cell, attachmentName);
            }
        }
    }

    public void AddStoneCellAttachment(Cell cell,string attachmentName)
    {
        if (cell.topCellAttachment != null)
        {
            if (!cell.topCellAttachment.canRemove)
                return;
            switch (cell.topCellAttachment.propertyType)
            {
                case AttachmentPropertyType.Grass:
                    break;
                case AttachmentPropertyType.Ice:
                    break;
                case AttachmentPropertyType.Fire:
                    break;
                case AttachmentPropertyType.Stone:
                    break;
                case AttachmentPropertyType.Null:
                    break;
                default:
                    break;
            }
        }
        else
        {
            //if (cell.propertyType == AttachmentPropertyType.Grass || cell.propertyType == AttachmentPropertyType.Stone)
            //{
                OnlyAddAttachment(cell, attachmentName);
            //}
        }
    }

    public void OnlyAddAttachment(Cell cell,string attachmentName)
    {
        CellAttachment cellAttachment = PoolManager.Instance.GetObj(attachmentName).GetComponent<CellAttachment>();
        cellAttachment.FirstSetCell(cell);
    }

    public void ChangeTopToAddAttachment(Cell cell,string attachmenName)
    {
        cell.topCellAttachment.ChangeToLow();
        OnlyAddAttachment(cell, attachmenName);
    }

    public void RemoveTopToAddAttachment(Cell cell,string attachmentName)
    {
        cell.topCellAttachment.RemoveImmediate();
        OnlyAddAttachment(cell, attachmentName);
    }

    public void RemoveAllAttachment(Cell cell)
    {
        cell.lowCellAttachment.RemoveImmediate();
        cell.topCellAttachment.RemoveImmediate();
    }

    /*public void RemoveAllToAddEmbers(Cell cell)
    {
        RemoveAllAttachment(cell);
        OnlyAddAttachment(cell,"Embers");
    }*/

    public void OnlyRemoveTop(Cell cell)
    {
        cell.topCellAttachment.RemoveImmediate();
        if (cell.lowCellAttachment != null)
            cell.lowCellAttachment.ChangeToTop();
    }
}
