using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningShowUI : MonoBehaviour
{
    private GameObject arrow;
    private LearningSlot[] learningSlots;
    private List<LearningWindow> learningWindowList=new List<LearningWindow>();
    private Transform learningWindowPos;

    private int slotIndex;
    private int limitIndex;

    private bool isTop;

    public int SlotIndex
    {
        get => slotIndex;
        set
        {
            if (limitIndex == 0)
                return;
            learningSlots[slotIndex].CancelSelect();
            learningWindowList[slotIndex].gameObject.SetActive(false);
            slotIndex = (value + limitIndex) % limitIndex;
            learningSlots[slotIndex].SelectSlot();
            arrow.transform.position = new Vector2(arrow.transform.position.x, learningSlots[slotIndex].transform.position.y);
            learningWindowList[slotIndex].gameObject.SetActive(true);
            learningWindowList[slotIndex].PageIndex = 0;
        }
    }

    public void Init()
    {
        arrow = TransformHelper.GetChildTransform(transform, "Arrow").gameObject;
        learningSlots = GetComponentsInChildren<LearningSlot>();
        foreach(var slot in learningSlots)
        {
            slot.Init();
        }
        learningWindowPos = TransformHelper.GetChildTransform(transform, "LearningDetailWindow");
        slotIndex = 0;
    }

    private void Update()
    {
        if(isTop)
        {
            if (Input.GetKeyDown(KeyCode.W))
                --SlotIndex;

            if (Input.GetKeyDown(KeyCode.S))
                ++SlotIndex;

            if(Input.GetKeyDown(KeyCode.J))
            {
                learningWindowList[slotIndex].SetControll(true);
                isTop = false;
            }

            if(Input.GetKeyDown(KeyCode.I))
            {
                UIManager.Instance.PopPanel(UIPanelType.LearningUI);
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                learningWindowList[slotIndex].SetControll(false);
                isTop = true;
            }
        }
    }

    public void OpenLearningShowUI()
    {
        int i = 0;

        for (; i < LearningManager.Instance.LearingWindowOpenList.Count; i++)
        {
            if(LearningManager.Instance.LearingWindowOpenList[i])
            {
                learningSlots[i].SetSlot(LearningManager.Instance.LearningChineseNameList[i]);
                if(learningWindowList.Count==i)
                {
                    LearningWindow learningWindow = Instantiate(LearningManager.Instance.GetLearningWindow(i)).GetComponent<LearningWindow>();
                    learningWindow.Init();
                    learningWindow.OpenLearingWindow(learningWindowPos, Vector3.one, null);
                    learningWindowList.Add(learningWindow);
                }                
                learningWindowList[i].SetControll(false);
                learningWindowList[i].gameObject.SetActive(false);
            }
            else
            {
                break;
            }
        }

        limitIndex = i;
        isTop = true;

        for (; i < learningSlots.Length; i++)
        {
            learningSlots[i].ClearSlot();
        }

        SlotIndex = 0;
    }


}
