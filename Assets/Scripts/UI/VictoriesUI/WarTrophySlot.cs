using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarTrophySlot : MonoBehaviour
{
    private Image pocketLevelImage;
    private Image itemImage;
    private GameObject itemImageBG;
    private Text itemNameText;
    private Text itemNumText;

    public void Init()
    {
        pocketLevelImage = TransformHelper.GetChildTransform(transform, "PocketLevelImage").GetComponent<Image>();
        itemImageBG = TransformHelper.GetChildTransform(transform, "ItemImageBG").gameObject;
        itemImage = TransformHelper.GetChildTransform(transform, "ItemImage").GetComponent<Image>();
        itemNameText = TransformHelper.GetChildTransform(transform, "ItemNameText").GetComponent<Text>();
        itemNumText = TransformHelper.GetChildTransform(transform, "ItemNumText").GetComponent<Text>();
    }

    public void SetWarTrophy(PocketData pocketData)
    {
        pocketLevelImage.gameObject.SetActive(true);
        itemImageBG.gameObject.SetActive(true);
        itemNameText.gameObject.SetActive(true);
        itemNumText.gameObject.SetActive(true);
        pocketLevelImage.sprite= pocketData.pocketSprite;
        Item item = ItemManager.Instance.GetNumItem(pocketData.itemID, 0);
        itemImage.sprite = item.Sprite;
        itemNameText.text = item.Name;
        itemNumText.text = pocketData.itemNum.ToString();
        ItemManager.Instance.ChangeItemNum(item, pocketData.itemNum);
    }


}
