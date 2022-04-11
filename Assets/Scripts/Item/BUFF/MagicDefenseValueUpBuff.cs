using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDefenseValueUpBuff : AMPassiveEffect
{
    public override void AddEffect(Piece piece)
    {
        ValueChangeBuffTip valueChangeBuffTip = PoolManager.Instance.GetObj("ValueChangeTip").GetComponent<ValueChangeBuffTip>();
        valueChangeBuffTip.ShowValueUp("魔防", piece.transform.position);
    }
}
