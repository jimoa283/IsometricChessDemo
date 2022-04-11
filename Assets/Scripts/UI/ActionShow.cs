using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ActionShow : MonoBehaviour
{
    private Text actionName;

    public void Init()
    {
        actionName = TransformHelper.GetChildTransform(transform, "ActionName").GetComponent<Text>();
        EventCenter.Instance.AddEventListener<string>("SetActionName", SetActionName);
    }



    public void SetActionName(string name)
    {
        actionName.text = name;
        transform.localScale = new Vector3(0.1f, 1, 1);
        transform.DOScaleX(1, 0.3f).SetEase(Ease.InQuad);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEvent<string>("SetActionName", SetActionName);
    }
}
