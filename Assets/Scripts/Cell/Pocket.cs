using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket :MonoBehaviour
{  
    public Cell cell;
    public string pocketName;
    public PocketData pocketData;

    public void SetPocket(Cell cell,int id)
    {
        pocketData = PocketManager.Instance.GetPocketData(id);
        this.cell = cell;
        cell.pocket = this;
        transform.position = cell.transform.position + Vector3.up * 0.3f;
        SortingLayerSlover.GetSortingLayerName(GetComponent<SpriteRenderer>(), cell.floorIndex + 1, 0);
    }

    public void GetPocket()
    {
        cell.pocket = null;
        BattleGameManager.Instance.pocketDatas.Add(pocketData);
        PoolManager.Instance.PushObj(pocketName, gameObject);        
    }
}
