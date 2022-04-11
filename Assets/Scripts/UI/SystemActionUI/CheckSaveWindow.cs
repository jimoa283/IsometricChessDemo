using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckSaveWindow : CheckWindow
{
    private WindowButton save;
    private WindowButton cancel;
   /* protected override void SpecialInit(UnityAction[] actions)
    {
        save = TransformHelper.GetChildTransform(transform, "SaveButton").GetComponent<WindowButton>();
        save.Init(actions[0]);

        cancel = TransformHelper.GetChildTransform(transform, "CancelButton").GetComponent<WindowButton>();
        cancel.Init(actions[1]);

        windowButtons = new WindowButton[] { save, cancel };
    }*/
}
