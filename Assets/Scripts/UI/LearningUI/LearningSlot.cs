using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LearningSlot : MonoBehaviour
{
    private Text learningNamtText;
    private Image instance;
    private Image bg;

    public void Init()
    {
        learningNamtText = TransformHelper.GetChildTransform(transform, "LearningName").GetComponent<Text>();
        instance = TransformHelper.GetChildTransform(transform, "Instance").GetComponent<Image>();
        bg = GetComponent<Image>();
    }

    public void SetSlot(string _name)
    {
        learningNamtText.gameObject.SetActive(true);
        learningNamtText.text = _name;
    }

    public void ClearSlot()
    {
        learningNamtText.gameObject.SetActive(false);
    }

    public void SelectSlot()
    {
        transform.DOLocalMoveX(-7,0.05f);
        instance.color = Color.black;
        learningNamtText.color = Color.black;
        bg.color = new Color(1, 1, 1, 0.3f);
    }

    public void CancelSelect()
    {
        transform.DOLocalMoveX(0, 0.05f);
        instance.color = Color.white;
        learningNamtText.color = Color.white;
        bg.color = new Color(0, 0, 0, 0.3f);
    }
}
