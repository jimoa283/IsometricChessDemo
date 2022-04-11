using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VFXObj : MonoBehaviour
{
    protected Vector3 birthPos;
    protected Vector3 targetPos;
    public float waitTime;
    protected Cell cell;
    public float speed;


    public virtual void CellInit(Vector3 birthPos,Vector3 targetPos,Cell cell)
    {

        this.birthPos = birthPos+Vector3.up * 0.3f;
        this.targetPos = targetPos+Vector3.up*0.3f;
        transform.position = this.birthPos;
        this.cell = cell;
        StartCoroutine(DoVFXACtion());
    }

    public virtual void PieceInit(Vector3 birthPos, Vector3 targetPos)
    {
        transform.position = birthPos;
        this.birthPos = birthPos;
        this.targetPos = targetPos;
        StartCoroutine(DoVFXACtion());
    }


    protected virtual void ObjMove(float percent)
    {
        percent = 1;
    }

    IEnumerator DoVFXACtion()
    {
        float percent = 0;
        while (percent<1)
        {
            yield return null;
            percent += Time.deltaTime * (1/speed);
            ObjMove(percent);
        }
        VFXObjList.Instance.VFXAction();
        yield return new WaitForSeconds(waitTime);
        PoolManager.Instance.PushObj(name, gameObject);
    }
}
