using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShowRange : MonoBehaviour
{
    public string rangeName;
    public int order;
    private SpriteRenderer sr;
    public Vector3 startScale;

    public void SimpleShow(Cell cell)
    {
        transform.localScale = startScale;
        transform.SetParent(cell.transform);
        transform.localPosition = Vector3.zero;
        SortingLayerSlover.CloneCellSortLayer(cell,gameObject, order);
    }

    public void ShowByAnim(Cell cell)
    {
        SimpleShow(cell);
        transform.DOScale(0.9f, 0.1f);
    }

    public void Hide()
    {        
        PoolManager.Instance.PushObj(rangeName, gameObject);
    }
}
