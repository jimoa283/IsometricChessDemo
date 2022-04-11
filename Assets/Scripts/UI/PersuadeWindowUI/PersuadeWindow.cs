using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PersuadeWindow : CheckWindow
{
    private Text pieceNameText;
    private Text pieceSpeakInfoText;
    private Image pieceInfoShowBG;
    private Image pieceImage;
    public Color applaudColor;
    public Color againstColor;
    public Color neutralityColor;
    private GameObject interval;
    private PersuadeButton applaudButton;
    private PersuadeButton againstButton;

    public void PersuadeWindowInit(PersuadeWindowData persuadeWindowData,UnityAction[] actions)
    {
        if(pieceNameText==null)
        {
            pieceNameText = TransformHelper.GetChildTransform(transform, "PieceName").GetComponent<Text>();
            pieceSpeakInfoText = TransformHelper.GetChildTransform(transform, "PieceSpeakInfo").GetComponent<Text>();
            pieceInfoShowBG = TransformHelper.GetChildTransform(transform, "PieceInfoShow").GetComponent<Image>();
            pieceImage = TransformHelper.GetChildTransform(transform, "PieceImage").GetComponent<Image>();
            interval = TransformHelper.GetChildTransform(transform, "Interval").gameObject;
            applaudButton = TransformHelper.GetChildTransform(transform, "ApplaudButton").GetComponent<PersuadeButton>();
            againstButton = TransformHelper.GetChildTransform(transform,"AgainstButton").GetComponent<PersuadeButton>();
            canvasGroup = GetComponent<CanvasGroup>();
        }
        pieceNameText.text = persuadeWindowData.PieceName;
        pieceSpeakInfoText.text = persuadeWindowData.PieceSpeakInfoShow;
        pieceImage.sprite = persuadeWindowData.PieceImage;
        
        InitByAttitudeType(persuadeWindowData, actions);

        
    }

    private void InitByAttitudeType(PersuadeWindowData persuadeWindowData,UnityAction[] actions)
    {
        windowButtons = new List<WindowButton>();
        switch (persuadeWindowData.AttitudeType)
        {
            case AttitudeType.Applaud:
                pieceInfoShowBG.color = applaudColor;
                interval.SetActive(false);

                againstButton.gameObject.SetActive(false);
                applaudButton.gameObject.SetActive(true);

                OnePersuadeButtonInit(applaudButton, actions[0], persuadeWindowData.ButtoTextList[0], persuadeWindowData.PersuadeLevelList[0]);
                break;
            case AttitudeType.Against:
                pieceInfoShowBG.color = againstColor;
                interval.SetActive(false);

                againstButton.gameObject.SetActive(true);
                applaudButton.gameObject.SetActive(false);

                OnePersuadeButtonInit(againstButton, actions[0], persuadeWindowData.ButtoTextList[0], persuadeWindowData.PersuadeLevelList[0]);
                break;
            case AttitudeType.Neutrality:
                pieceInfoShowBG.color = neutralityColor;
                interval.SetActive(true);

                againstButton.gameObject.SetActive(true);
                applaudButton.gameObject.SetActive(true);

                OnePersuadeButtonInit(applaudButton, actions[0], persuadeWindowData.ButtoTextList[0], persuadeWindowData.PersuadeLevelList[0]);

                OnePersuadeButtonInit(againstButton, actions[1], persuadeWindowData.ButtoTextList[1], persuadeWindowData.PersuadeLevelList[1]);
                break;
            default:
                break;
        }
    }

    private void OnePersuadeButtonInit(PersuadeButton persuadeButton,UnityAction action,string buttonName,int level)
    {
        windowButtons.Add(persuadeButton);
        persuadeButton.Init(action);
        persuadeButton.SetButtonName(buttonName);
        persuadeButton.SetPersuadeLevel(level);
    }

    protected override void DoWindowHide()
    {
        UIManager.Instance.PopPanel(UIPanelType.PersuadeWindowUI);
    }

    protected override void WindowHide()
    {
        base.WindowHide();
        MainPlayerController.Instance.WakeInteractiveObject();
    }
}
