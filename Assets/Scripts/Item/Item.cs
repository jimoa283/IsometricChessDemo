using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment,
    UseItem
}

public class Item : Element
{
    public ItemType ItemType;
    public int BuyPrice;
    public int SellPrice;
    public int Number;
    public Item(int iD, string name, ElementType type, string info, Sprite sprite,ItemType itemType,int buyPrice,int sellPrice) : base(iD, name, type, info,sprite)
    {
        ItemType = itemType;
        BuyPrice = buyPrice;
        SellPrice = sellPrice;
        Number = 0;
    }
}
