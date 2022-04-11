using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WindowButton : MonoBehaviour
{
    protected Image buttonBG;
    protected Text buttonText;
    protected GameObject buttonArrow;
    protected UnityAction action;
    public  Color hideBGColor;
    public  Color showBGColor;


    public virtual void Init(UnityAction action)
    {
        this.action = action;
        if (buttonBG == null)
        {
            buttonBG = GetComponent<Image>();
            buttonText = TransformHelper.GetChildTransform(transform, "ButtonText").GetComponent<Text>();
            buttonArrow = TransformHelper.GetChildTransform(transform, "Arrow").gameObject;
        }
    }

    public void SetButtonName(string buttonName)
    {
        buttonText.text = buttonName;
    }

    public virtual void ShowButton()
    {
        buttonText.color = Color.black;
        buttonBG.color = showBGColor;
        buttonArrow.SetActive(true);
    }

    public virtual void HideButton()
    {
        buttonText.color = Color.white;
        buttonBG.color = hideBGColor;
        buttonArrow.SetActive(false);
    }

    public virtual void DoAction()
    {
        action?.Invoke();
    }
}
