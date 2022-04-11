using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckLoadWindow : CheckWindow
{
    private WindowButton load;
    private WindowButton cancel;

   /* protected override void SpecialInit(UnityAction[] actions)
    {
        load = TransformHelper.GetChildTransform(transform, "LoadButton").GetComponent<WindowButton>();
        load.Init(actions[0]);

        cancel = TransformHelper.GetChildTransform(transform, "CancelButton").GetComponent<WindowButton>();
        cancel.Init(actions[1]);

        windowButtons = new WindowButton[] { load, cancel };
    }*/
}
