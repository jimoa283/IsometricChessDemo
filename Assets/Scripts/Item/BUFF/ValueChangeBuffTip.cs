using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ValueChangeBuffTip : MonoBehaviour
{
    private Text valueNameText;
    private GameObject upArrow;
    private GameObject downArrow;

   public void Init()
    {
        valueNameText = TransformHelper.GetChildTransform(transform, "ValueNameText").GetComponent<Text>();
        upArrow = TransformHelper.GetChildTransform(transform, "UpArrow").gameObject;
        downArrow = TransformHelper.GetChildTransform(transform, "DownArrow").gameObject;
        
    }

    public void ShowValueUp(string valueName,Vector3 pos)
    {
        Init();
        gameObject.SetActive(true);
        valueNameText.text = valueName;
        upArrow.SetActive(true);
        downArrow.SetActive(false);
        transform.position = pos - Vector3.up * 0.15f;
        StartCoroutine(DoFade());
    }

    public void ShowValueDown(string valueName,Vector3 pos)
    {
        Init();
        gameObject.SetActive(true);
        valueNameText.text = valueName;
        upArrow.SetActive(false);
        downArrow.SetActive(true);
        transform.position = pos - Vector3.up * 0.15f;
        StartCoroutine(DoFade());
    }

    IEnumerator DoFade()
    {
        transform.DOMoveY(transform.position.y + 0.15f, 0.7f).SetEase(Ease.OutCubic);
        yield return new WaitForSeconds(0.75f);
        PoolManager.Instance.PushObj("ValueChangeTip", gameObject);
    }
}
