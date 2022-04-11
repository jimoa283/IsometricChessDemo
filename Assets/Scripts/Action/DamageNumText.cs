using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamageNumText : MonoBehaviour
{
    private Text value;
    public Color red;
    public Color green;

    public void Init(bool isRed,Vector3 pos)
    {
        value = TransformHelper.GetChildTransform(transform, "Value").GetComponent<Text>();
        value.color = isRed ? red : green;
        transform.position = pos;
    }

    IEnumerator DoFade()
    {
        yield return new WaitForSeconds(0.3f);
        value.DOFade(0, 0.4f);
        yield return new WaitForSeconds(0.45f);
        //PoolManager.Instance.PushObj("")
    }
}
