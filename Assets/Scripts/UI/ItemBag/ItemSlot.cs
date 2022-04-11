using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemSlot : MonoBehaviour
{
    protected Image bG;
    protected Image itemImage;
    protected Text itemName;
    protected Text itemNum;
    public Item item;

    public virtual void Init()
    {
        bG = GetComponent<Image>();
        itemImage = transform.Find("ItemImage").GetComponent<Image>();
        itemName = transform.Find("ItemName").GetComponent<Text>();
        itemNum = transform.Find("ItemNum").GetComponent<Text>();
    }

    protected  void BaseSetItemSlot(Item item)
    {
        this.item = item;
        itemImage.gameObject.SetActive(true);
        itemName.gameObject.SetActive(true);
        itemNum.gameObject.SetActive(true);
        itemImage.sprite = item.Sprite;
        itemName.text = item.Name;
    }

    public virtual void SetItemSlot(Item item)
    {
        /*this.item = item;
        itemImage.gameObject.SetActive(true);
        itemName.gameObject.SetActive(true);
        itemNum.gameObject.SetActive(true);
        itemImage.sprite = item.Sprite;
        itemName.text = item.Name;*/
        BaseSetItemSlot(item);
        itemNum.text = item.Number.ToString();
    }

    public virtual void ClearItemSlot()
    {
        item = null;
        itemImage.gameObject.SetActive(false);
        itemName.gameObject.SetActive(false);
        itemNum.gameObject.SetActive(false);
    }

    public virtual void SelectSlot()
    {
        bG.color = new Color(1, 1, 1, 0.4f);
        itemName.color = Color.black;
        itemNum.color = Color.black;
        transform.DOLocalMoveX( - 3f, 0.1f);
    }

    public virtual void ResetSlot()
    {
        bG.color = new Color(0, 0, 0, 0.4f);
        itemName.color = Color.white;
        itemNum.color = Color.white;
        transform.DOLocalMoveX( 0, 0.1f);
    }

    public virtual void slotAction()
    {

    }
}
