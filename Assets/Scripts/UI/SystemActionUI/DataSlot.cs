using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DataSlot : MonoBehaviour
{
    private Save save;
    private GameObject nullData;
    private GameObject bg;
    private GameObject saveInfo;
    private GameObject newTip;
    private Text levelAndNameText;
    private Text levelNameText;
    private Image saveImage;
    private Text realTime;

    public void Init()
    {
        nullData = TransformHelper.GetChildTransform(transform, "NullData").gameObject;
        bg = TransformHelper.GetChildTransform(transform, "BG").gameObject;
        saveInfo = TransformHelper.GetChildTransform(transform, "SaveInfo").gameObject;
        newTip = TransformHelper.GetChildTransform(transform, "NewTip").gameObject;
        levelAndNameText = TransformHelper.GetChildTransform(transform, "LevelAndTime").GetComponent<Text>();
        levelNameText = TransformHelper.GetChildTransform(transform, "LevelNameText").GetComponent<Text>();
        saveImage = TransformHelper.GetChildTransform(transform, "SaveImage").GetComponent<Image>();
        realTime = TransformHelper.GetChildTransform(transform, "RealTime").GetComponent<Text>();
    }

    public void SetDataSlot(Save save)
    {
        this.save = save;
        nullData.SetActive(false);
        saveInfo.SetActive(true);
        if (save.isTheNewest)
            newTip.SetActive(true);
        else
            newTip.SetActive(false);
        int tempTime = (int)save.GameTime;
        int hour = tempTime / 3600;
        int minute = (tempTime - hour * 3600) / 60;
        levelAndNameText.text = "<size=13>等级" + save.PlayerLevelList[0] + "</size>  " + hour+":"+minute;
        realTime.text = save.realTime;
    }

    public void CancelNew()
    {
        newTip.SetActive(false);
    }

    public void ClearSlot()
    {
        nullData.SetActive(true);
        saveInfo.gameObject.SetActive(false);
        save = null;
    }

    public void ShowDataSlot()
    {
        transform.DOLocalMoveX(-8,0.1f);
    }

    public void HideDataSlot()
    {
        transform.DOLocalMoveX(0, 0.1f);
    }

    public virtual void SlotAction()
    {

    }
}
