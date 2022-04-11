using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellInfoUI : MonoBehaviour
{
    private Text cellNameText;
    private Text cellHeightNumText;
    private Text cellInfoText;
    
    public void Init()
    {
        cellNameText = TransformHelper.GetChildTransform(transform, "CellNameText").GetComponent<Text>();
        cellHeightNumText = TransformHelper.GetChildTransform(transform, "CellHeightNumText").GetComponent<Text>();
        cellInfoText = TransformHelper.GetChildTransform(transform, "CellInfoText").GetComponent<Text>();
        EventCenter.Instance.AddEventListener<Cell>("ShowCellInfo", ShowCellInfo);
    }

    public void ShowCellInfo(Cell cell)
    {        
        cellHeightNumText.text = cell.floorIndex.ToString();
        if (cell.topCellAttachment != null)
        {
            cellInfoText.text = cell.topCellAttachment.RealAttachmentInfo();
            cellNameText.text = cell.topCellAttachment.attachmentName;
        }       
        else
        {
            cellInfoText.text = cell.cellInfo;
            cellNameText.text = cell.cellName;
        }
            
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEvent<Cell>("ShowCellInfo",ShowCellInfo);
    }
}
