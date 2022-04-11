using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MissionInfo : MonoBehaviour
{
    private Text missionName;
    private Text victroyCondition;
    private Text loseCondition;
    private CanvasGroup canvasGroup;
    private RectTransform rt;

   public void Init()
    {
        rt = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        missionName = TransformHelper.GetChildTransform(transform, "MissionNameText").GetComponent<Text>();
        victroyCondition = TransformHelper.GetChildTransform(transform, "VictroyConditionInfoText").GetComponent<Text>();
        loseCondition = TransformHelper.GetChildTransform(transform, "LoseConditionInfoText").GetComponent<Text>();
        EventCenter.Instance.AddEventListener("HideMissionInfo", HideMissionInfo);
        EventCenter.Instance.AddEventListener("ShowMissionInfo", ShowMissionInfo);

        missionName.text = BattleGameManager.Instance.levelInfo.levelName;
        victroyCondition.text = BattleGameManager.Instance.levelInfo.victoryConditionInfo;
        loseCondition.text = BattleGameManager.Instance.levelInfo.loseConditionInfo;
    }

    public void HideMissionInfo()
    {
        gameObject.SetActive(false);
    }

    public void ShowMissionInfo()
    {
        if (gameObject.activeInHierarchy)
            return;
        gameObject.SetActive(true);
        canvasGroup.alpha = 0;
        rt.localPosition = new Vector3(rt.localPosition.x, rt.localPosition.y-100);
        canvasGroup.DOFade(1, 0.5f);
        rt.DOLocalMoveY(rt.localPosition.y+ 100, 0.5f);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEvent("HideMissionInfo", HideMissionInfo);
        EventCenter.Instance.RemoveEvent("ShowMissionInfo", ShowMissionInfo);
    }
}
