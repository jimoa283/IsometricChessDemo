using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[System.Serializable]
public class ChoiceData : BaseData
{
#if UNITY_EDITOR
    public TextField TextField { get; set; }
    public ObjectField AudioClipField { get; set; }
#endif
    public Container_String Text=new Container_String();
    public AudioClip AudioClip;
    public List<Container_ConditionCheck> ConditionChecks = new List<Container_ConditionCheck>();
}

