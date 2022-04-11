using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ConditionCheck",menuName ="MyDialogue/ConditionCheck")]
[System.Serializable]
public class ConditionCheckSO : ScriptableObject
{
    public virtual bool ChoiceCheck()
    {
        return false;
    }
}
