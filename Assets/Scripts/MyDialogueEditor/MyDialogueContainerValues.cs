using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class MyDialogueContainerValues
{
    public MyDialogueContainerValues()
    {

    }
}

[System.Serializable]
public class Container_String
{
    public string Value;
}

[System.Serializable]
public class Container_Int
{
    public int Value;
}

[System.Serializable]
public class Container_Float
{
    public float Value;
}

[System.Serializable]
public class Container_Sprite
{
    public Sprite Value;
}

[System.Serializable]
public class Container_ConditionAddType
{
#if UNITY_EDITOR
    public EnumField EnumField { get; set; }
#endif

    public ConditionAddType Value = ConditionAddType.And;
}

[System.Serializable]
public class Container_ConditionCheck
{
#if UNITY_EDITOR
    public ObjectField ObjectField { get; set; }
#endif
    public Container_ConditionCheckSO Container_ConditionCheckSO=new Container_ConditionCheckSO();
    public Container_ConditionAddType Container_ConditionAddType=new Container_ConditionAddType();
}

[System.Serializable]
public class Container_Event
{
    public EventSO Value;
}
[System.Serializable]
public class Container_ConditionCheckSO
{
    public ConditionCheckSO Value;
}
