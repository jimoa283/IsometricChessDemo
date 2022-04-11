using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameUISlot : BBActionSlot
{
    public override void BeSelected()
    {
        bG.color = new Color(bG.color.r,bG.color.g,bG.color.b,1);
        actionName.color = Color.black;
    }

    public override void ResetSlot()
    {
        bG.color = new Color(bG.color.r, bG.color.g, bG.color.b, 0);
        actionName.color = Color.white;

    }
}
