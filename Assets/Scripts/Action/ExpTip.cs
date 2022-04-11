using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ExpTip : MonoBehaviour
{
    private Text expNum;
    
    public void Init(int num,Vector3 pos)
    {
        expNum = TransformHelper.GetChildTransform(transform, "ExpNum").GetComponent<Text>();
        expNum.text = "经验值 " + num;
        transform.position = pos-Vector3.up*0.15f;
        StartCoroutine(DoFade());
    }

    IEnumerator DoFade()
    {
        transform.DOMoveY(transform.position.y + 0.15f, 0.7f);
        yield return new WaitForSeconds(0.75f);
        PoolManager.Instance.PushObj("ExpTip", gameObject);
    }
}
