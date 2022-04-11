using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SimpleActionCheckWindow : CheckWindow
{
   public void SimpleWindowInit(UnityAction[] actions,SimpleCheckWindowData simpleCheckWindowData)
    {
        Init(actions,simpleCheckWindowData.ButtonTextList);

        Text checkWindowInfoText = TransformHelper.GetChildTransform(transform, "CheckWindowInfo").GetComponent<Text>();
        checkWindowInfoText.text = simpleCheckWindowData.CheckWindowInfo;
    }

    protected override void DoWindowHide()
    {
        UIManager.Instance.PopPanel(UIPanelType.SimpleCheckWindowUI);
    }

    protected override void WindowHide()
    {
        
    }
}
