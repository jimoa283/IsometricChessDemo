using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TipUI : MonoBehaviour
{
    private float animTime = 0.5f;
    private CanvasGroup canvasGroup;
    //private UnityAction action;
    public KeyCode keyCode;
    private GameObject disableMask;
    private InteractiveAction interactiveAction;

    public void Init(InteractiveObject interactiveObject)
    {
        //this.action = action;
        canvasGroup = GetComponent<CanvasGroup>();
        interactiveAction = GetComponent<InteractiveAction>();
        disableMask = TransformHelper.GetChildTransform(transform, "DisableMask").gameObject;
        InstantHide();
        interactiveAction.Init(interactiveObject,this);     
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
            DoAction();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        StopCoroutine("DoHide");
        transform.DOLocalMoveX(-10, animTime);
        canvasGroup.DOFade(1, animTime);
    }

    public void SetDisable(bool isActive)
    {
        disableMask.SetActive(isActive);
    }

    public void DoAction()
    {
        if(gameObject.activeInHierarchy&&!disableMask.activeInHierarchy)
            interactiveAction.DoAction();
    }

    public void HideAnim()
    {
        if(gameObject.activeInHierarchy)
        StartCoroutine("DoHide");
    }

    IEnumerator DoHide()
    {
        float tempX = transform.position.x;
        transform.DOLocalMoveX(0, animTime);
        canvasGroup.DOFade(0, animTime);
        yield return new WaitForSeconds(animTime + 0.05f);
        gameObject.SetActive(false);
    }

    public void InstantHide()
    {
        transform.localPosition = new Vector3(0, transform.localPosition.y);
        canvasGroup.alpha = 0;
        gameObject.SetActive(false);
    }
}
