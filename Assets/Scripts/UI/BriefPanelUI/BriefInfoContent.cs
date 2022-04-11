using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class BriefInfoContent : MonoBehaviour
{
    private Text infoText;
    private UnityAction action;
    private CanvasGroup canvasGroup;

    public void Init()
    {
        infoText = TransformHelper.GetChildTransform(transform, "Info").GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
        EventCenter.Instance.AddEventListener<string, UnityAction>("SetBriefInfo", SetBriefInfo);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(DoAction());
        }
    }

    public void SetBriefInfo(string info,UnityAction action)
    {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.3f);

        transform.localScale = Vector3.one * 0.5f;
        transform.DOScale(1, 0.3f);

        this.action = action;
        infoText.text = info;
    }

    IEnumerator DoAction()
    {
        canvasGroup.DOFade(0, 0.3f);
        transform.DOScale(0.5f, 0.3f);

        yield return new WaitForSeconds(0.35f);
        action?.Invoke();
        UIManager.Instance.PopPanel(UIPanelType.BriefInfoUI);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEvent<string, UnityAction>("SetBriefInfo", SetBriefInfo);
    }
}
