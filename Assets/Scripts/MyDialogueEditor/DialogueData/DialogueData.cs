using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[System.Serializable]
public class DialogueData : BaseData
{
    public DialogueData_Name DialogueData_Name = new DialogueData_Name();
    public DialogueData_Text DialogueData_Text = new DialogueData_Text();
    public DialogueData_Audio DialogueData_Audio = new DialogueData_Audio();
    public List<DialogueData_Port> DialogueData_Ports = new List<DialogueData_Port>();
}

[System.Serializable]
public class DialogueData_Name
{
#if UNITY_EDITOR
    public TextField textField { get; set; }
#endif
    public Container_String Name=new Container_String();
}

[System.Serializable]
public class DialogueData_Text
{
#if UNITY_EDITOR
    public TextField textField { get; set; }
#endif
    public Container_String Text=new Container_String();
}

[System.Serializable]
public class DialogueData_Audio
{
#if UNITY_EDITOR
    public ObjectField objectField { get; set; }
#endif
    public AudioClip AudioClip;
}

[System.Serializable]
public class DialogueData_Port
{
    public string PortGuid;
    public string inputGuid;
    public string outputGuid;
}
