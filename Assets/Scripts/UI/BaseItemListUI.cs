using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseItemListUI : MonoBehaviour
{
    protected ItemSlot[] itemSlots;
    protected int limitIndex;
    protected int slotIndex;
    protected int bagIndex;
    protected GameObject arrow;
    protected ELementDetailShowUI eLementDetailShow;
    protected List<BagType> bagTypes;

    protected BagType allItemBag;
    protected BagType allUseItemBag;
    protected BagType allEquipmentBag;

    protected List<Item> allItemList;
    protected List<UseItem> allUseItemList;
    protected List<Equipment> allEquipmentList;

    protected BasePanel basePanel;

    public int SlotIndex
    {
        get => slotIndex;
        set
        {
            ChangeSelectSlot(value);
        }
    }

    public int BagIndex
    {
        get => bagIndex;
        set
        {
            bagTypes[bagIndex].ResetThisBagIcon();
            bagIndex = (value + bagTypes.Count) % bagTypes.Count;
            bagTypes[bagIndex].SetItemList();
        }
    }

    public void Init()
    {
        basePanel = GetComponentInParent<BasePanel>();
        itemSlots = GetComponentsInChildren<ItemSlot>();
        foreach (var slot in itemSlots)
        {
            slot.Init();
        }

        eLementDetailShow = GetComponentInChildren<ELementDetailShowUI>();
        eLementDetailShow.Init();

        arrow = TransformHelper.GetChildTransform(transform,"arrow").gameObject;

        SpecialInit();

        allItemBag = TransformHelper.GetChildTransform(transform, "AllItemBagIcon").GetComponent<BagType>();
        allItemBag.Init(ChangeToAllItemBag);

        allUseItemBag = TransformHelper.GetChildTransform(transform, "AllUseItemBagIcon").GetComponent<BagType>();
        allUseItemBag.Init(ChangeToUseItemBag);

        allEquipmentBag = TransformHelper.GetChildTransform(transform, "AllEquipmentBagIcon").GetComponent<BagType>();
        allEquipmentBag.Init(ChangeToEquipmentBag);

        bagTypes = new List<BagType> { allItemBag, allUseItemBag, allEquipmentBag };
    }

    private void Update()
    {
        if(UIManager.Instance.GetTopPanel()==basePanel)
        {
            if (Input.GetKeyDown(KeyCode.W))
                --SlotIndex;

            if (Input.GetKeyDown(KeyCode.S))
                ++SlotIndex;

            if (Input.GetKeyDown(KeyCode.A))
                --BagIndex;

            if (Input.GetKeyDown(KeyCode.D))
                ++BagIndex;

            if (Input.GetKeyDown(KeyCode.J))
            {
                itemSlots[slotIndex].slotAction();
            }
        }
    }

    protected virtual void SpecialInit()
    {

    }

    public virtual void OpenItemList()
    {
        BagIndex = 0;
        SlotIndex = 0;
    }

    public virtual void ReturnItemList()
    {
        SpecialInit();
        BagIndex = bagIndex;
        SlotIndex = slotIndex;
    }

    protected void ChangeToAllItemBag()
    {
        SetItemList(allItemList);
        SlotIndex = 0;
    }

    protected void ChangeToUseItemBag()
    {
        SetItemList(allUseItemList);
        SlotIndex = 0;
        
    }

    protected void ChangeToEquipmentBag()
    {
        SetItemList(allEquipmentList);
        SlotIndex = 0;
    }

    protected void SetItemList<T>(List<T> list) where T : Item
    {
        int i = 0;
        for (i = 0; i < itemSlots.Length; i++)
        {
            if (list.Count < i + 1)
                break;
            itemSlots[i].SetItemSlot(list[i]);
        }
        limitIndex = i;
        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].ClearItemSlot();
        }

        if (limitIndex < 1)
            limitIndex = 1;
    }

    /*protected void SetItemList(List<Item> list)
    {
        int i = 0;
        for (i = 0; i < itemSlots.Length; i++)
        {
            if (list.Count < i + 1)
                break;
            itemSlots[i].SetItemSlot(list[i]);
        }

        limitIndex = i;
        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].ClearItemSlot();
        }

        if (limitIndex < 1)
            limitIndex = 1;
    }

    protected void SetUseItemList(List<UseItem> list)
    {
        int i = 0;
        for (i = 0; i < itemSlots.Length; i++)
        {
            if (list.Count < i + 1)
                break;
            itemSlots[i].SetItemSlot(list[i]);
        }
        limitIndex = i;
        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].ClearItemSlot();
        }

        if (limitIndex < 1)
            limitIndex = 1;
    }

    protected void SetEquipmentList(List<Equipment> list)
    {
        int i = 0;
        for (i = 0; i < itemSlots.Length; i++)
        {
            if (list.Count < i + 1)
                break;
            itemSlots[i].SetItemSlot(list[i]);
        }
        limitIndex = i;
        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].ClearItemSlot();
        }

        if (limitIndex < 1)
            limitIndex = 1;
    }*/

    protected void ChangeSelectSlot(int num)
    {
        itemSlots[slotIndex].ResetSlot();
        slotIndex = (num + limitIndex) % limitIndex;
        eLementDetailShow.ChangeItemInfoShow(itemSlots[slotIndex].item);
        itemSlots[slotIndex].SelectSlot();
        arrow.transform.position = new Vector2(arrow.transform.position.x, itemSlots[slotIndex].transform.position.y);
    }

}
