using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowVFXObj : VFXObj
{
    protected Vector3 centerPos;
    protected float top;

    public override void CellInit(Vector3 birthPos, Vector3 targetPos, Cell cell)
    {
        top = birthPos.y >= targetPos.y ? birthPos.y + 2f : targetPos.y + 2f;
        centerPos = new Vector3((birthPos.x + targetPos.x) / 2, top);
        base.CellInit(birthPos, targetPos, cell);       
    }

    public override void PieceInit(Vector3 birthPos, Vector3 targetPos)
    {
        top = birthPos.y >= targetPos.y ? birthPos.y + 2f : targetPos.y + 2f;
        centerPos = new Vector3((birthPos.x + targetPos.x) / 2, top);
        base.PieceInit(birthPos, targetPos);
    }

    protected override void ObjMove(float percent)
    {
        float u = 1 - percent;
        float tt = percent * percent;
        float uu = u * u;

        transform.position = uu * birthPos;
        transform.position += 2 * u * percent * centerPos;
        transform.position += tt * targetPos;
    }
}
