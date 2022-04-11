using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BagType : MonoBehaviour
{
    private Image BG;
    //private List<Item> itemList;
    private UnityAction action;
    private GameObject bagNameText;

    public void Init(UnityAction action)
    {
        BG = TransformHelper.GetChildTransform(transform, "BG").GetComponent<Image>();
        bagNameText = TransformHelper.GetChildTransform(transform, "BagNameText").gameObject;
        this.action = action;
        ResetThisBagIcon();
    }

    public void ShowThisBagIcon()
    {
        bagNameText.SetActive(true);
        BG.color = new Color(0,0,0,0.5f);
    }

    public void ResetThisBagIcon()
    {
        bagNameText.SetActive(false);
        BG.color = new Color(0, 0, 0, 1);
    }

    public void SetItemList()
    {
        ShowThisBagIcon();
        action?.Invoke();
    }
}
