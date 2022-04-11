using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceBuffShowSlot : MonoBehaviour
{
    private Image buffImage;
    private Text buffCountDownNum;

    public void Init()
    {
        buffImage = TransformHelper.GetChildTransform(transform, "BuffImage").GetComponent<Image>();
        buffCountDownNum = TransformHelper.GetChildTransform(transform, "BuffCountDownNum").GetComponent<Text>();
        gameObject.SetActive(false);
    }

    public void ShowPieceBuff(BUFF buff)
    {
        gameObject.SetActive(true);
        buffImage.sprite = buff.BUFFSprite;
        buffCountDownNum.text = buff.AliveTime.ToString();
    }

}
