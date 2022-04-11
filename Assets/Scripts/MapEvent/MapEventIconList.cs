using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapEventIconList : MonoBehaviour
{
   private List<EventIcon> eventIcons=new List<EventIcon>();
   private List<LevelEventInfo> levelEventInfoList;
    private MapLevelEventInfoUI mapLevelEventInfo;
   private int gameLevel;

   private int iconIndex;

    [SerializeField]private bool canControll=true;

    public int IconIndex { get => iconIndex;
        set
        {
            eventIcons[iconIndex].CancelSelect();
            iconIndex = (value + eventIcons.Count) % eventIcons.Count;
            eventIcons[iconIndex].SelectEventIcon();
            mapLevelEventInfo.ShowInfo(levelEventInfoList[iconIndex]);
        }
    }

    private void Start()
    {
        EventCenter.Instance.EventTrigger<UnityAction>("SceneStartFadeIn", Init);
    }

    private void Init()
    {
        canControll = true;
        mapLevelEventInfo = GameObject.Find("Canvas").transform.GetChildTransform("Panel").GetComponent<MapLevelEventInfoUI>();
        mapLevelEventInfo.Init();

        if (gameLevel != GameManager.Instance.gameLevel)
        {
            gameLevel = GameManager.Instance.gameLevel;
            levelEventInfoList = new List<LevelEventInfo>(LevelEventManager.Instance.GetLevelEventInfoList(gameLevel));

            while (eventIcons.Count > 0)
            {
                Destroy(eventIcons[0]);
            }

            eventIcons.Clear();

            for (int i = 0; i < levelEventInfoList.Count; i++)
            {
                EventIcon eventIcon;
                if (levelEventInfoList[i].LevelEventType == LevelEventType.Main)
                {
                    eventIcon = PoolManager.Instance.GetObj("MainEventIcon").GetComponent<EventIcon>();
                }
                else
                {
                    eventIcon = PoolManager.Instance.GetObj("SideEventIcon").GetComponent<EventIcon>();
                }

                eventIcon.Init(levelEventInfoList[i]);
                eventIcons.Add(eventIcon);
            }
        }

        for (int i = 0; i < eventIcons.Count;)
        {
            if (LevelEventManager.Instance.hasShowLevelEventList.Contains(levelEventInfoList[i].ID))
            {
                levelEventInfoList.RemoveAt(i);
                Destroy(eventIcons[i].gameObject);
                eventIcons.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }

        for (int i = 0; i < eventIcons.Count; i++)
        {
            if (levelEventInfoList[i].LevelEventType == LevelEventType.Main)
            {
                IconIndex = i;
            }
        }
    }

    private void Update()
    {
        if(UIManager.Instance.PanelStack.Count==0&&canControll)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.BattleReadyUI));
            }

            if(eventIcons.Count>1)
            {
                if (Input.GetKeyDown(KeyCode.A))
                    --IconIndex;

                if (Input.GetKeyDown(KeyCode.D))
                    ++IconIndex;
            }
         
            if(Input.GetKeyDown(KeyCode.J))
            {
                canControll = false;
                MainPlayerController.Instance.passWord = 0;
                eventIcons[iconIndex].DoEvent();
            }
        }
    }

}
