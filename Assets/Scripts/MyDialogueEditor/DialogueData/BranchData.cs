using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[System.Serializable]
public class BranchData :BaseData
{
#if UNITY_EDITOR
    public ObjectField CheckField;
#endif
    public string trueGuidNode;
    public string falseGuidNode;
    public List<Container_ConditionCheck> conditionCheckSOs = new List<Container_ConditionCheck>();
}

public enum ConditionAddType
{
    And,
    Or
}
