using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkillShowPanel : MonoBehaviour
{
    private PassiveSkillShowSlot[] passiveSkillShowSlots;
    private int currentSlotIndex;

    public void Init()
    {
        passiveSkillShowSlots = GetComponentsInChildren<PassiveSkillShowSlot>();
        foreach(var slot in passiveSkillShowSlots)
        {
            slot.Init(this);
        }
        //ResetAllSlot();
        EventCenter.Instance.AddEventListener<Sprite, string>("ShowPassiveSkill", ShowPassiveSkill);
    }

    public void ResetAllSlot()
    {
        foreach(var slot in passiveSkillShowSlots)
        {
            slot.ResetSlot();
        }
        currentSlotIndex = 0;
    }

    public void ShowPassiveSkill(Sprite sprite,string passiveSkillName)
    {
        passiveSkillShowSlots[currentSlotIndex].ShowPassiveSkill(sprite, passiveSkillName);
        currentSlotIndex++;
    }

    public void CheckSlotActive()
    {
        foreach(var slot in passiveSkillShowSlots)
        {
            if (slot.isActive)
                return;
        }

        currentSlotIndex = 0;
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEvent<Sprite, string>("ShowPassiveSkill", ShowPassiveSkill);
    }
}
