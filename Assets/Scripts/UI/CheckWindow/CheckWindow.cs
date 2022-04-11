using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class CheckWindow : MonoBehaviour
{
    protected List<WindowButton> windowButtons;

    protected CanvasGroup canvasGroup;

    protected int buttonIndex;

    public int ButtonIndex
    {
        get => buttonIndex;
        set
        {
            windowButtons[buttonIndex].HideButton();
            buttonIndex = (value + windowButtons.Count) % windowButtons.Count;
            windowButtons[buttonIndex].ShowButton();
        }
    }

    public void Init(UnityAction[] actions,List<string> buttonNameList) 
    {
        canvasGroup = GetComponent<CanvasGroup>();
        windowButtons = new List<WindowButton>(GetComponentsInChildren<WindowButton>());

        for (int i = 0; i < actions.Length; i++)
        {
            windowButtons[i].Init(actions[i]);
            windowButtons[i].SetButtonName(buttonNameList[i]);
        }
    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            --ButtonIndex;

        if (Input.GetKeyDown(KeyCode.S))
            ++ButtonIndex;

        if (Input.GetKeyDown(KeyCode.J))
            DoAction();

        if (Input.GetKeyDown(KeyCode.I))
            WindowHide();
    }

    public void OpenWindow()
    {
        foreach(var button in windowButtons)
        {
            button.HideButton();
        }
        gameObject.SetActive(true);
        ButtonIndex = 0;
        transform.localScale = new Vector3(0.5f, 0.5f);
        canvasGroup.alpha = 0;
        transform.DOScale(Vector3.one, 0.2f);        
        canvasGroup.DOFade(1, 0.2f);
    }

    protected virtual void DoAction()
    {
        StartCoroutine(DoHideWindow(windowButtons[buttonIndex].DoAction));
    }

    protected virtual void WindowHide()
    {
        StartCoroutine(DoHideWindow(null));
    }

    protected virtual void DoWindowHide()
    {

    }

    protected IEnumerator DoHideWindow(UnityAction action)
    {
        canvasGroup.DOFade(0, 0.2f);
        yield return new WaitForSeconds(0.2f);
        DoWindowHide();
        action?.Invoke();
    }
}
