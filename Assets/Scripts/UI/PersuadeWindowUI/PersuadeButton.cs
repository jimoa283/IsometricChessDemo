using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PersuadeButton : WindowButton
{
    private GameObject highLightMask;
    private Text persuadeLevelText;

    public override void DoAction()
    {
        base.DoAction();
    }

    public override void HideButton()
    {
        highLightMask.SetActive(false);
        buttonArrow.SetActive(false);
        buttonText.color = Color.white;
    }

    public override void Init(UnityAction action)
    {
        base.Init(action);
        persuadeLevelText = TransformHelper.GetChildTransform(transform, "PersuadeLevelText").GetComponent<Text>();
        highLightMask = TransformHelper.GetChildTransform(transform, "HighLightMask").gameObject;
    }

    public void SetPersuadeLevel(int level)
    {
        switch (level)
        {
            case 1:
                persuadeLevelText.text = "（如果加把劲似乎能说服）";
                break;
            case 2:
                persuadeLevelText.text = "(似乎不至于不愿意听)";
                break;
            case 3:
                persuadeLevelText.text = "(似乎无法达成共识）";
                break;
            default:
                break;
        }
    }

    public override void ShowButton()
    {
        highLightMask.SetActive(true);
        buttonArrow.SetActive(true);
        buttonText.color = Color.black;
    }
}
