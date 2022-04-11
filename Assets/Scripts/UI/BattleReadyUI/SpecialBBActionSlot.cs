using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBBActionSlot : BBActionSlot
{
    //private Image bG;
    //private Text actionName;
    //private UnityAction slotAction;
    //private UnityAction selectAction;
    //private UnityAction resetAction;

   /* public void Init(UnityAction action)
    {
        bG = GetComponentInChildren<Image>();
        actionName = GetComponentInChildren<Text>();
        slotAction = action;
    }*/


    public override void BeSelected()
    {
        bG.color = new Color(1, 1, 1, 0.5f);
        actionName.color = Color.black;
    }

    public override void ResetSlot()
    {
        bG.color = new Color(0, 0, 0, 0.5f);
        actionName.color = Color.white;
        
    }
}
