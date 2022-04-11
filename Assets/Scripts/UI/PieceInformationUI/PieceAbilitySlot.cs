using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceAbilitySlot : MonoBehaviour
{
    private GameObject upArrow;
    private GameObject downArrow;

    private Text value;
    public Color upColor;
    public Color downColor;

    public void Init()
    {
        upArrow = TransformHelper.GetChildTransform(transform, "UpArrow").gameObject;
        downArrow = TransformHelper.GetChildTransform(transform, "DownArrow").gameObject;
        value = TransformHelper.GetChildTransform(transform, "Value").GetComponent<Text>();
    }

    public void SetValueUp(int num)
    {
        value.color = upColor;
        upArrow.SetActive(true);
        downArrow.SetActive(false);
        value.text = num.ToString();
    }

    public void SetSimpleValue(int num)
    {
        value.color = Color.white;
        upArrow.SetActive(false);
        downArrow.SetActive(false);
        value.text = num.ToString();
    }

    public void SetDownValue(int num)
    {
        value.color = downColor;
        upArrow.SetActive(false);
        downArrow.SetActive(true);
        value.text = num.ToString();
    }

    public void SetValue(int baseValue,int extraValue)
    {
        if (extraValue == 0)
            SetSimpleValue(baseValue + extraValue);
        else if (extraValue > 0)
            SetValueUp(baseValue + extraValue);
        else
            SetDownValue(baseValue + extraValue);
    }
}
