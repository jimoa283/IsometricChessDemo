using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveObject : MonoBehaviour
{
    protected List<TipUI> tipUIList;
    protected TipRange tipRange;

    private void Start()
    {
        Init();
    }

    public  void Init()
    {
        tipUIList = new List<TipUI>();
        Transform TipUIListT = TransformHelper.GetChildTransform(transform, "TipUIList");
        int tempCount = TipUIListT.childCount;
        for (int i = 0; i < tempCount; i++)
        {
            TipUI tipUI = TipUIListT.GetChild(i).GetComponent<TipUI>();
            tipUI.Init(this);
            tipUIList.Add(tipUI);
        }

        tipRange = GetComponentInChildren<TipRange>();

        tipRange.Init(tipRangeEnterEvent, tipRangeExitEvent);
    }


    public void BaseActionOnClick()
    {
        MainPlayerController.Instance.SetIdle();
        MainPlayerController.Instance.canControll = false;
        HideTipUIInstant();
    }

    protected  void tipRangeEnterEvent()
    {
        MainPlayerController.Instance.interactiveObject = this;       
        ShowTipUI();
    }

    protected virtual void FinishAction()
    {
        MainPlayerController.Instance.WakeInteractiveObject();
    }

    protected  void tipRangeExitEvent()
    {
        MainPlayerController.Instance.interactiveObject = null;
        if(gameObject.activeInHierarchy)
        HideTipUIAnim();
    }

    public void ShowTipUI()
    {
        StartCoroutine(DoShowTipUI());
    }

    IEnumerator DoShowTipUI()
    {
        foreach (var tip in tipUIList)
        {
            tip.Show();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void HideTipUIAnim()
    {
        StartCoroutine("DoHideTipUIAnim");
    }

    IEnumerator DoHideTipUIAnim()
    {
        foreach (var tip in tipUIList)
        {
            tip.HideAnim();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void HideTipUIInstant()
    {
        foreach (var tip in tipUIList)
        {
            tip.InstantHide();
        }
    }
}
