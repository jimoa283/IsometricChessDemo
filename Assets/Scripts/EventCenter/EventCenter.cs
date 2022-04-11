using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter : Singleton<EventCenter>
{

    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

    //private readonly string szErrorMessage = "DispatchEvent Error, Event:{0}, Error:{1}, {2}";

    public void AddEventListener<T>(string eventName, UnityAction<T> action)
    {
        if (action == null)
        {
            Debug.LogError("the Event Null");
            return;
        }

        if (!eventDic.ContainsKey(eventName))
        {
            eventDic.Add(eventName, new EventInfo<T>(action));
        }
       else
        {
            (eventDic[eventName] as EventInfo<T>).actions += action;
        }
    }

    public void AddEventListener(string eventName, UnityAction action)
    {
        if (action == null)
        {
            Debug.LogError("the Event Null");
            return;
        }

        if (!eventDic.ContainsKey(eventName))
        {
            eventDic.Add(eventName, new EventInfo(action));
        }
        else
        {
            (eventDic[eventName] as EventInfo).actions += action;
        }
    }

    public void AddEventListener<T,K>(string eventName, UnityAction<T,K> action)
    {
        if (action == null)
        {
            Debug.LogError("the Event Null");
            return;
        }

        if (!eventDic.ContainsKey(eventName))
        {
            eventDic.Add(eventName, new EventInfo<T,K>(action));
        }
        else
        {
            (eventDic[eventName] as EventInfo<T,K>).actions += action;
        }
    }

    public void RemoveEvent(string eventName, UnityAction action)
    {
        if (action == null)
            return;
        if(eventDic.ContainsKey(eventName))
        (eventDic[eventName] as EventInfo).actions -= action;
    }

    public void RemoveEvent<T>(string eventName, UnityAction<T> action)
    {
        if (action == null)
            return;
        if (eventDic.ContainsKey(eventName))
            (eventDic[eventName] as EventInfo<T>).actions -= action;
    }

    public void RemoveEvent<T,K>(string eventName, UnityAction<T,K> action)
    {
        if (action == null)
            return;
        if (eventDic.ContainsKey(eventName))
            (eventDic[eventName] as EventInfo<T,K>).actions -= action;
    }

    public void EventTrigger(string eventName)
    {
        if (eventDic.ContainsKey(eventName))
            (eventDic[eventName] as EventInfo).actions.Invoke();
        /*else
            Debug.Log("No Event" + eventName);*/
    }

    public void EventTrigger<T>(string eventName,T ele1)
    {
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo<T>).actions.Invoke(ele1);
        }
       /* else
            Debug.Log("No Event"+eventName);*/
    }

    public void EventTrigger<T,K>(string eventName,T ele1,K ele2)
    {
        if (eventDic.ContainsKey(eventName))
            (eventDic[eventName] as EventInfo<T,K>).actions.Invoke(ele1,ele2);
    /*    else
            Debug.Log("No Event" + eventName);*/
    }

    public void ClearEvent()
    {
        eventDic.Clear();
    }

}

