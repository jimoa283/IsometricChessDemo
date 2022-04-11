using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLevelEventInfoUI : MonoBehaviour
{
    private Text eventTypeText;
    private Text eventInfoText;
    private Image placeImage;
    private Text placeInfoText;
    public Color mainColor;
    public Color sideColor;

    public void Init()
    {
        eventInfoText = TransformHelper.GetChildTransform(transform, "EventInfo").GetComponent<Text>();
        eventTypeText = TransformHelper.GetChildTransform(transform, "EventType").GetComponent<Text>();
        placeImage = TransformHelper.GetChildTransform(transform, "PlaceImage").GetComponent<Image>();
        placeInfoText = TransformHelper.GetChildTransform(transform, "PlaceInfo").GetComponent<Text>();
    }

    public void ShowInfo(LevelEventInfo levelEventInfo)
    {
        if (levelEventInfo.LevelEventType == LevelEventType.Main)
        {
            eventTypeText.text = "主线故事";
            eventTypeText.color = mainColor;
        }            
        else
        {
            eventTypeText.text = "支线故事";
            eventTypeText.color = sideColor;
        }
            
        eventInfoText.text = levelEventInfo.EventInfo;
        placeImage.sprite = levelEventInfo.PlaceSprite;
        placeInfoText.text = levelEventInfo.PlaceInfo;
    }
}
