using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatantNumShow : MonoBehaviour
{
    private Text combatantNum;

    public void Init()
    {
        combatantNum = TransformHelper.GetChildTransform(transform, "CombatantNum").GetComponent<Text>();
        EventCenter.Instance.AddEventListener("ShowCombatantNum", ShowCombatantNum);
        EventCenter.Instance.AddEventListener("HideCombatantNum", HideCombatantNum);
        gameObject.SetActive(false);
    }

    public void ShowCombatantNum()
    {
        gameObject.SetActive(true);
        combatantNum.text = PieceQueueManager.Instance.PlayerList.Count + "/" + BattleGameManager.Instance.maxPlayerNum;
    }

    public void HideCombatantNum()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEvent("ShowCombatantNum", ShowCombatantNum);
        EventCenter.Instance.RemoveEvent("HideCombatantNum", HideCombatantNum);
    }
}
