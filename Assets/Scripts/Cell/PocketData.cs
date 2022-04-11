using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PocketData 
{
    public int money;
    public int itemID;
    public int itemNum;
    public Sprite pocketSprite;

    public PocketData(int money,int itemID,int itemNum, Sprite pocketSprite)
    {
        this.money = money;
        this.itemID = itemID;
        this.itemNum = itemNum;
        this.pocketSprite = pocketSprite;
    }
}
