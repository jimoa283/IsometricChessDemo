using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class EventIcon : MonoBehaviour
{
    private Animator anim;
    private RectTransform placeNameShowRT;
    private Text placeNameText;
    //private UnityAction action;
    private CanvasGroup canvasGroup;
    private LevelEventInfo levelEventInfo;
    private GameObject selectShowObj;
    

    public void Init(LevelEventInfo levelEventInfo)
    {
        //this.action = action;
        this.levelEventInfo = levelEventInfo;
        selectShowObj = TransformHelper.GetChildTransform(transform, "SelectShow").gameObject;
        placeNameText = TransformHelper.GetChildTransform(transform, "PlaceName").GetComponent<Text>();
        placeNameShowRT = TransformHelper.GetChildTransform(transform, "PlaceNameShow").GetComponent<RectTransform>();
        canvasGroup = placeNameShowRT.GetComponent<CanvasGroup>();
        anim = GetComponent<Animator>();
        placeNameText.text = levelEventInfo.PlaceName;
        transform.position = levelEventInfo.Pos;
        EventActionTipShow(levelEventInfo.LevelEventActionType);
        anim.Play("Idle");
        placeNameShowRT.gameObject.SetActive(false);
    }

    private void EventActionTipShow(LevelEventActionType levelEventActionType)
    {
        switch (levelEventActionType)
        {
            case LevelEventActionType.Simple:
                break;
            case LevelEventActionType.Battle:
                TransformHelper.GetChildTransform(transform, "BattleTip").gameObject.SetActive(true);
                break;
            case LevelEventActionType.PlayerGet:
                TransformHelper.GetChildTransform(transform, "PlayerGetTip").gameObject.SetActive(true);
                break;
            case LevelEventActionType.Special:
                TransformHelper.GetChildTransform(transform, "SpecialEventTip").gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void SelectEventIcon()
    {
        StopCoroutine("DoCancelSelect");
        selectShowObj.SetActive(true);
        anim.Play("EventIcon");
        placeNameShowRT.gameObject.SetActive(true);
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.3f);
        placeNameShowRT.DOLocalMoveY(225, 0.3f);
    }

    public void CancelSelect()
    {
        selectShowObj.SetActive(false);
        anim.Play("Idle");
        canvasGroup.DOFade(0, 0.3f);
        placeNameShowRT.DOLocalMoveY(160, 0.3f);
        StartCoroutine("DoCancelSelect");
    }

    IEnumerator DoCancelSelect()
    {
        yield return new WaitForSeconds(0.35f);
        placeNameShowRT.gameObject.SetActive(false);
    }

    public void DoEvent()
    {
        LevelEventManager.Instance.hasShowLevelEventList.Add(levelEventInfo.ID);
        if (levelEventInfo.LevelEventActionType == LevelEventActionType.Battle)
            BattleGameManager.Instance.isInBattleScene = true;
        else
            BattleGameManager.Instance.isInBattleScene = false;
        CameraController.Instance.MapEventSceneChange(transform.position, levelEventInfo.SceneName);
    }
}
