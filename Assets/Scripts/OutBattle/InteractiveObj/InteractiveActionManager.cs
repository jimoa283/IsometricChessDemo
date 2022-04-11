using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class InteractiveActionManager:Singleton<InteractiveActionManager>
{
    private Dictionary<string, bool> interactiveActionActiveDic;

    public Dictionary<string, bool> InteractiveActionActiveDic  => interactiveActionActiveDic;

    public InteractiveActionManager()
    {
        interactiveActionActiveDic = new Dictionary<string, bool>();
    }

    public void LoadInteractiveDic(List<string> keys,List<bool> values)
    {
        int tempCount = keys.Count;
        interactiveActionActiveDic.Clear();
        for (int i = 0; i < tempCount; i++)
        {
            interactiveActionActiveDic.Add(keys[i], values[i]);
        }
    }

    public void SetInteractiveActive(string id,bool isActive)
    {
        if (!interactiveActionActiveDic.ContainsKey(id))
            interactiveActionActiveDic.Add(id, isActive);
        else
            interactiveActionActiveDic[id] = isActive;
    }

    public bool GetInteractiveActive(string id)
    {
        if (!interactiveActionActiveDic.ContainsKey(id))
        {
            interactiveActionActiveDic.Add(id, false);
            return false;
        }            
        else
        {
            return interactiveActionActiveDic[id];
        }
    }
}
