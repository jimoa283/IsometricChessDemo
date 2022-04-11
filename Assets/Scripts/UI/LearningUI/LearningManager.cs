using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class LearningManager : Singleton<LearningManager>
{
    private List<string> learningWindowPathList = new List<string>() {"BaseBattleLearning" };
    private List<string> learningChineseNameList = new List<string>() { "战斗的基础" };
    private List<bool> learingWindowOpenList = new List<bool>();

    public List<bool> LearingWindowOpenList { get => learingWindowOpenList; }
    public List<string> LearningChineseNameList { get => learningChineseNameList;}

    public LearningManager()
    {
        Init();
    }


    private void Init()
    {
        for (int i = 0; i < learningWindowPathList.Count; i++)
        {
            learingWindowOpenList.Add(false);
        }
    }

    public void LoadLearningWindowOpenList(List<bool> list)
    {
        learingWindowOpenList = new List<bool>(list);
    }

    public GameObject GetLearningWindow(int id)
    {
        return Resources.Load<GameObject>("LearningWindow/"+learningWindowPathList[id]);
    }

    public void SetLearingWindowOpen(int id,bool isOpen)
    {
        learingWindowOpenList[id] = isOpen;
    }

    public void SetLearingWindowUI(int id)
    {
        if (learingWindowOpenList[id])
            return;
        SetLearingWindowOpen(id, true);
        BasePanel panel= UIManager.Instance.GetPanel(UIPanelType.LearningWindowUI);
        GameObject learingWindow = GameObject.Instantiate(GetLearningWindow(id));
        learingWindow.transform.SetParent(panel.transform);
        UIManager.Instance.PushPanel(panel);
    }
}
