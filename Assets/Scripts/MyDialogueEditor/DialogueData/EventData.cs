using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[System.Serializable]
public class EventData : BaseData
{
#if UNITY_EDITOR
    public ObjectField EventField;
#endif
    public Container_Event Container_Event=new Container_Event();
}
