using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ELementDetailShowUI : MonoBehaviour
{
    private Text powerValue;
    private Text rangeValue;
    private Text firstInfo;
    private Text secondInfo;
    private GameObject powerObj;
    private GameObject rangeObj;
    private GameObject firstInfoObj;
    private GameObject secondInfoObj;

   public void Init()
    {
        powerObj = transform.Find("Power").gameObject;
        rangeObj = transform.Find("Range").gameObject;
        firstInfoObj = transform.Find("FirstInfo").gameObject;
        secondInfoObj = transform.Find("SecondInfo").gameObject;
        powerValue = powerObj.transform.Find("PowerValue").GetComponent<Text>();
        rangeValue = rangeObj.transform.Find("RangeValue").GetComponent<Text>();
        rangeValue = rangeObj.transform.Find("RangeValue").GetComponent<Text>();
        firstInfo = firstInfoObj.transform.Find("FirstInfoText").GetComponent<Text>();
        secondInfo = secondInfoObj.transform.Find("SecondInfoText").GetComponent<Text>();
    }

    public void ChangeSkillInfoShow(Skill skill)
    {
        if(skill.SkillType==SkillType.Active)
        {
            ActiveSkill activeSkill = skill as ActiveSkill;
            FirstInfoShow(activeSkill.BaseActionData.RealPower(activeSkill.BaseActionData,activeSkill.BaseActionData.owner),activeSkill.BaseActionData.HMinRange, activeSkill.BaseActionData.HMaxRange, activeSkill.BaseActionData.VMinRange, activeSkill.BaseActionData.VMaxRange, skill.Info);
        }
         else
        {
            FirstInfoShow(-1, -1, -1, -1, -1, skill.Info);
        }
    }

    public void ChangeUseItemInfoShow(UseItem item)
    {
        FirstInfoShow(item.BaseActionData.Power, item.BaseActionData.HMinRange, item.BaseActionData.HMaxRange, item.BaseActionData.VMinRange,item.BaseActionData.VMaxRange, item.Info);
    }

    public void ChangeEquipmentInfoShow(Equipment equipment)
    {
        SecondInfoShowForEquipment(equipment.Info,equipment);
    }

    public void ChangeToFirstInfoShow()
    {
        powerObj.SetActive(true);
        rangeObj.SetActive(true);
        firstInfoObj.SetActive(true);
        secondInfoObj.SetActive(false);
    }

   public void ChangeToSecondInfoShow()
    {
        powerObj.SetActive(false);
        rangeObj.SetActive(false);
        firstInfoObj.SetActive(false);
        secondInfoObj.SetActive(true);
    }

    public void ChangeItemInfoShow(Item item)
    {
        if(item==null)
        {
            NullInfoShow();
        }
        else if(item.ItemType==ItemType.UseItem)
        {
            ChangeToFirstInfoShow();
            UseItem useItem = item as UseItem;
            FirstInfoShow(useItem.BaseActionData.RealPower(useItem.BaseActionData,null), useItem.BaseActionData.HMinRange, useItem.BaseActionData.HMaxRange, useItem.BaseActionData.VMinRange,useItem.BaseActionData.VMaxRange, useItem.Info);
        }
        else 
        {
            ChangeToSecondInfoShow();
            Equipment equipment = item as Equipment;
            SecondInfoShowForEquipment(equipment.Info,equipment);
        }
    }

    private void FirstInfoShow(int power,int hMinRange,int hMaxRange,int vMinRange,int vMaxRange,string info)
    {
        powerObj.SetActive(true);
        rangeObj.SetActive(true);
        firstInfoObj.SetActive(true);
        secondInfoObj.SetActive(false);

        if(power<0)
        {
            powerValue.text = "-";
            
        }
        else
        {
            powerValue.text = power.ToString();         
        }
        if(hMaxRange<0)
            rangeValue.text = "-";
        else
            rangeValue.text = hMinRange + "~" + hMaxRange + "   (高度  " + vMinRange + "~+" + vMaxRange + ")";
        firstInfo.text = info;
    }

   /* private void ForPassiveSkillShow(string info)
    {
        powerObj.SetActive(true);
        rangeObj.SetActive(true);
        firstInfoObj.SetActive(true);
        secondInfoObj.SetActive(false);

        powerValue.text = "-";
        rangeValue.text = "-";
        firstInfo.text = info;
    }*/

    private void SecondInfoShow(string info)
    {
        powerObj.SetActive(false);
        rangeObj.SetActive(false);
        firstInfoObj.SetActive(false);
        secondInfoObj.SetActive(true);

        secondInfo.text = info;
    }

    private void SecondInfoShowForEquipment(string info,Equipment equipment)
    {
        SecondInfoShow(info);
        if(equipment.Owner!=null)
        {
            secondInfo.text += "\n  " + equipment.Owner.pieceStatus.pieceName + "已装备";
        }
    }

    private void NullInfoShow()
    {
        powerObj.SetActive(false);
        rangeObj.SetActive(false);
        firstInfoObj.SetActive(false);
        secondInfoObj.SetActive(true);

        secondInfo.text = "";
    }
}
