using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatantNumUI : BasePanel
{
    private CombatantNumShow combatantNumShow;

    public override void Init()
    {
        base.Init();
        combatantNumShow = TransformHelper.GetChildTransform(transform,"CombatantNumShow").GetComponent<CombatantNumShow>();
        combatantNumShow.Init();
    }

   
}
