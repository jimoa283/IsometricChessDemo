using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EquipmentSlot : MonoBehaviour
{
    private Image bg;
    private Image equipmentImage;
    private Text equipmentName;
    public Equipment equipment;

    public void Init()
    {
        bg = GetComponent<Image>();
        equipmentImage = transform.Find("Image").GetComponent<Image>();
        equipmentName = transform.Find("Name").GetComponent<Text>();
    }

    public void SetEquipment(Equipment equipment)
    {
        this.equipment = equipment;
        equipmentImage.gameObject.SetActive(true);
        equipmentName.gameObject.SetActive(true);
        equipmentImage.sprite = equipment.Sprite;
        equipmentName.text = equipment.Name;
    }

    public void ClearSlot()
    {
        equipment = null;
        equipmentImage.gameObject.SetActive(false);
        equipmentName.gameObject.SetActive(false);
    }

    public void SelectSlot()
    {
        bg.color = new Color(1, 1, 1, 0.6f);
        equipmentName.color = Color.black;
        transform.DOLocalMoveX(-3, 0.1f);
    }

    public void CancelSelect()
    {
        bg.color = new Color(0, 0, 0, 0.6f);
        equipmentName.color = Color.white;
        transform.DOLocalMoveX(0, 0.1f);
    }
}
