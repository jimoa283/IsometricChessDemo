using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShopManager : Singleton<ItemShopManager>
{
    private ItemShopListModel currentShopItemList;
    private Dictionary<int,ItemShopListModel> shopItemListDic;

    public ItemShopListModel CurrentShopItemList { get => currentShopItemList;set { currentShopItemList = value; } }

    public ItemShopManager()
    {
        shopItemListDic = new Dictionary<int, ItemShopListModel>();
        ParseShopUseItemList();
        RegisterNewItemList(1);
    }

    private void ParseShopUseItemList()
    {
        TextAsset textShopUseItemList = Resources.Load<TextAsset>("JSON/ShopListJSON");
        JSONObject jSONObject = new JSONObject(textShopUseItemList.text);

        int level = 1;
        foreach(var obj in jSONObject.list)
        {
            //List<Item> shopItemList = new List<Item>();
            ItemShopListModel temp = new ItemShopListModel();
            for (int i = 0; i < obj["ShopItemIDList"].Count; i++)
            {
                int id = (int)obj["ShopItemIDList"][i].n;
                int num = (int)obj["ShopItemNumList"][i].n;
                Item item = UseItemManager.Instance.GetNumUseItem(id, num);
                //shopItemList.Add(useItem);
                temp.AddItem(item);
            }

            shopItemListDic.Add(level, temp);
            level++;
        }
    }

    public void RegisterNewItemList(int level)
    {
        if(shopItemListDic.ContainsKey(level))
        {
            currentShopItemList = new ItemShopListModel(shopItemListDic[level]);
        }      
    }
}

public class ItemShopListModel
{
    private List<Item> allItemList = new List<Item>();
    private List<UseItem> allUseItemList = new List<UseItem>();
    private List<Equipment> allEquipmentList = new List<Equipment>();

    public List<Item> AllItemList { get 
        {
            allItemList.Clear();
            allItemList.AddRange(allUseItemList);
            allItemList.AddRange(allEquipmentList);
            return allItemList;
        }  
    }

    public List<UseItem> AllUseItemList { get => allUseItemList; }
    public List<Equipment> AllEquipmentList { get => allEquipmentList; }

    public ItemShopListModel()
    {
       /* allItemList = new List<Item>();
        allUseItemList = new List<UseItem>();
        allEquipmentList = new List<Equipment>();*/
    }

    public ItemShopListModel(ItemShopListModel ori)
    {
         //allEquipmentList = new List<Equipment>(ori.allEquipmentList);
         allUseItemList = new List<UseItem>(ori.allUseItemList);
         allItemList = new List<Item>(ori.allItemList);
       /* allItemList = new List<Item>();
        allUseItemList = new List<UseItem>();
        allEquipmentList = new List<Equipment>();
        foreach (var item in ori.allItemList)
        {
            AddItem(UseItemManager.Instance.GetNumUseItem(item.ID, item.Number));
        }*/
    }

    public ItemShopListModel(List<int> itemIDList, List<int> itemNumList)
    {
        for (int i = 0; i < itemIDList.Count; i++)
        {
            AddItem(ItemManager.Instance.GetNumItem(itemIDList[i], itemNumList[i]));
        }
    }


    public void AddItem(Item item)
    {
        if(item.ItemType==ItemType.UseItem)
        {
            allUseItemList.Add(item as UseItem);
        }
        else if(item.ItemType==ItemType.Equipment)
        {
            allEquipmentList.Add(item as Equipment);
        }
    }
}
