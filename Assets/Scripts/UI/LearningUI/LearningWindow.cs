using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class LearningWindow : MonoBehaviour
{
    private List<GameObject> learningContentList = new List<GameObject>();
    private Text pageNumText;
    private GameObject leftArrow;
    private GameObject rightArrow;
    private int pageIndex;
    private bool canControll;
    private UnityAction action;
    private UnityAction closeAction;
    private RectTransform rt;
    private CanvasGroup canvasGroup;

    public int PageIndex { get => pageIndex;
        set 
        {
            learningContentList[pageIndex].SetActive(false);
            pageIndex = (value + learningContentList.Count) % learningContentList.Count;
            learningContentList[pageIndex].SetActive(true);
            pageNumText.text = pageIndex + 1 + "/" + learningContentList.Count;
        }
    }

    public void Init()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rt = GetComponent<RectTransform>();
        pageNumText = TransformHelper.GetChildTransform(transform, "PageNumText").GetComponent<Text>();
        leftArrow = TransformHelper.GetChildTransform(transform, "LeftArrow").gameObject;
        rightArrow = TransformHelper.GetChildTransform(transform, "RightArrow").gameObject;
        FindAllLearingContent();
        
        pageIndex = 0;
    }

    private void Update()
    {
        if(canControll)
        {
            if (Input.GetKeyDown(KeyCode.A))
                --PageIndex;

            if (Input.GetKeyDown(KeyCode.D))
                ++PageIndex;

            if(Input.GetKeyDown(KeyCode.J))
            {
                closeAction?.Invoke();
            }
        }
    }

    private void FindAllLearingContent()
    {
        Transform learningContentListTransform = TransformHelper.GetChildTransform(transform, "LearnContentList").transform;
        for (int i = 0; i <learningContentListTransform.childCount; i++)
        {
            learningContentList.Add(learningContentListTransform.transform.GetChild(i).gameObject);
        }
        if(learningContentListTransform.childCount==1)
        {
            rightArrow.gameObject.SetActive(false);
            leftArrow.gameObject.SetActive(false);
        }
    }

    public void OpenLearingWindow(Transform tf,Vector3 scale,UnityAction action)
    {
        PageIndex = 0;
        gameObject.SetActive(true);
        transform.SetParent(tf);
        rt.localPosition = Vector3.zero;
        rt.localScale = scale;
        this.action = action;
        closeAction = CloseLearningWindow;
    }

    public void OpenLearingWindowByAnim(Transform tf,Vector3 scale,UnityAction action)
    {
        OpenLearingWindow(tf,scale,action);
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.3f);
        transform.localScale = new Vector3(0.8f, 1, 1);
        transform.DOScaleX(1, 0.3f);
        closeAction = CloseLearingWindowByAnim;
    }

    public void CloseLearingWindowByAnim()
    {
        StartCoroutine(DoCloseLearingWindowByAnim());
    }

    IEnumerator DoCloseLearingWindowByAnim()
    {
        canvasGroup.DOFade(0, 0.3f);
        yield return new WaitForSeconds(0.35f);
        action?.Invoke();
    }

    public void CloseLearningWindow()
    {
        action?.Invoke();
    }

    public void SetControll(bool canControll)
    {
        this.canControll = canControll;
    }

}
