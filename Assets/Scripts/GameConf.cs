using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameConf")]
public class GameConf :ScriptableObject
{

    [Header("SortingLayer名字")]
    public string[] sortingLayerName;

    public int num;
    public UIPanelType panelType;
    public AttackType attackType;

    
}
