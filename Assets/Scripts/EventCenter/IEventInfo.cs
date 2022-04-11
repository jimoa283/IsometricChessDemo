using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IEventInfo 
{
   
}

public class EventInfo<T> :IEventInfo
{
    public UnityAction<T> actions;
    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}

public class EventInfo:IEventInfo
{
    public UnityAction actions;
    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}

public class EventInfo<T,K>:IEventInfo
{
    public UnityAction<T, K> actions;
    public EventInfo(UnityAction<T,K> action)
    {
        actions += action;
    }
}
