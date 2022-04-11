using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoriesShowWindow : MonoBehaviour
{
    private WarTrophySlot[] warTrophySlots;
    private Text pointGetText;
    private Text moneyGetText;

    public void Init()
    {
        warTrophySlots = GetComponentsInChildren<WarTrophySlot>();
        pointGetText = TransformHelper.GetChildTransform(transform, "PointGetText").GetComponent<Text>();
        moneyGetText = TransformHelper.GetChildTransform(transform, "MoneyGetText").GetComponent<Text>();

        foreach(var slot in warTrophySlots)
        {
            slot.Init();
        }
    }

    public void SetVictoriesWindow()
    {
        pointGetText.text = BattleGameManager.Instance.pointGetNum.ToString();
        int money=0;
        for (int i = 0; i < BattleGameManager.Instance.pocketDatas.Count; i++)
        {
            money += BattleGameManager.Instance.pocketDatas[i].money;
            warTrophySlots[i].SetWarTrophy(BattleGameManager.Instance.pocketDatas[i]);
        }
        moneyGetText.text = money.ToString();
    }


}
