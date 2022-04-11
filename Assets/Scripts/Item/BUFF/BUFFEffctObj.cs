using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class BUFFEffctObj : MonoBehaviour
{
    private Text buffObjName;

    private SpriteRenderer sr;

    private UnityAction action;

   public void Init(UnityAction action)
    {
        sr = GetComponent<SpriteRenderer>();
        this.action = action;
        buffObjName = TransformHelper.GetChildTransform(transform, "BuffObjName").GetComponent<Text>();
        FirstShow();
    }



    public void StartBUFFEffectObjShow()
    {
        gameObject.SetActive(true);
        StartCoroutine("DoBUFFEffectObjShow");
    }

    public void StopBUFFEffectObjShow()
    {
        StopCoroutine("DoBUFFEffectObjShow");
    }

    public void FirstShow()
    {
        StartCoroutine("DoFirstShow");
    }

    IEnumerator DoFirstShow()
    {
        buffObjName.gameObject.SetActive(true);
        sr.color = new Color(sr.color.r,sr.color.g,sr.color.b,1);
        yield return new WaitForSeconds(1.5f);
        buffObjName.DOFade(0, 2f);
        sr.DOFade(0, 2f);
        yield return new WaitForSeconds(2.05f);
        buffObjName.gameObject.SetActive(false);
        gameObject.SetActive(false);

        action?.Invoke();
    }

    IEnumerator DoBUFFEffectObjShow()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        sr.DOFade(1, 0.8f).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(0.9f);
        sr.DOFade(0, 0.8f).SetEase(Ease.InQuint);
        yield return new WaitForSeconds(0.9f);
        gameObject.SetActive(false);

        action?.Invoke();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
