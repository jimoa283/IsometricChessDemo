using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MSingleton<GameManager>
{
    public GameConf gameConf;

    public int money;

    public int gameLevel;

    

    private void Start()
    {
        gameLevel = 0;
    }

    private void Update()
    {

    }

    public void TestSet()
    {
        ItemManager.Instance.ChangeItemNum(UseItemManager.Instance.GetUseItem(1001), 2);
        ItemManager.Instance.ChangeItemNum(UseItemManager.Instance.GetUseItem(1002), 3);
        ItemManager.Instance.ChangeItemNum(EquipmentManager.Instance.GetEquipment(2002), 3);
        ItemManager.Instance.ChangeItemNum(EquipmentManager.Instance.GetEquipment(2001), 3);
        ItemManager.Instance.ChangeItemNum(UseItemManager.Instance.GetUseItem(1001), 4);
    }
}
