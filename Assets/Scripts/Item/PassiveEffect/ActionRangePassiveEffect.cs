using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRangePassiveEffect : IPassiveEffect
{
    public virtual int ActionRangeChangeEffect(BaseActionData baseActionData, int range)
    {
        return range;
    }
}
