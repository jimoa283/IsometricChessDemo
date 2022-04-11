using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseValueUpBuff : AMPassiveEffect
{
    public override void AddEffect(Piece piece)
    {
        ValueChangeBuffTip valueChangeBuffTip = PoolManager.Instance.GetObj("ValueChangeTip").GetComponent<ValueChangeBuffTip>();
        valueChangeBuffTip.ShowValueUp("防御",piece.transform.position);
    }
}
