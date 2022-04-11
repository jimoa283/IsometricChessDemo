using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpecialBattleTip : MonoBehaviour
{
    private GameObject missTip;
    private GameObject noDamageTip;

    public void Init()
    {
        if(missTip==null)
        missTip = TransformHelper.GetChildTransform(transform, "MissTip").gameObject;

        if(noDamageTip==null)
        noDamageTip = TransformHelper.GetChildTransform(transform, "NoDamage").gameObject;
    }

    public void ShowMissTip(Vector3 pos)
    {
        Init();
        transform.position = pos;
        //gameObject.SetActive(true);
        missTip.SetActive(true);
        noDamageTip.SetActive(false);
        StartCoroutine(DoFade());
    }

    public void ShowNoDamageTip(Vector3 pos)
    {
        Init();
        transform.position = pos;
        //gameObject.SetActive(true);
        missTip.SetActive(true);
        noDamageTip.SetActive(false);
        StartCoroutine(DoFade());
    }

    IEnumerator DoFade()
    {
        transform.DOMoveY(transform.position.y + 0.3f, 1f);
        yield return new WaitForSeconds(1.05f);
        PoolManager.Instance.PushObj("SpecialBattleTip", gameObject);
    }
}
