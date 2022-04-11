using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class BBActionSlot : MonoBehaviour
{
    protected Image bG;
    protected Text actionName;
    protected UnityAction slotAction;
    protected RectTransform rt;
    protected float oriX;
    //private UnityAction selectAction;
    //private UnityAction resetAction;

    public void Init(UnityAction action)
    {
        bG = GetComponentInChildren<Image>();
        actionName = GetComponentInChildren<Text>();
        rt = GetComponent<RectTransform>();
        //ResetSlot();
        slotAction = action;
        oriX = rt.localPosition.x;
        ResetSlot();
    }

   
    public void DoAction()
    {
        slotAction?.Invoke();
    }

    public virtual void BeSelected()
    {
        bG.gameObject.SetActive(true);
        actionName.color = Color.black;
        rt.DOLocalMoveX(-11, 0.1f);
    }

    public virtual void ResetSlot()
    {
        bG.gameObject.SetActive(false);
        actionName.color = Color.white;
        rt.DOLocalMoveX(-8, 0.1f);
    }

    /*public void BeSelectedSpecia()
    {
        bG.color = new Color(1, 1, 1, 0.5f);
        actionName.color = Color.black;
    }

    public void ResetSlotSpecia()
    {
        bG.color = new Color(0, 0, 0, 0.5f);
        actionName.color = Color.white;
    }*/
}
