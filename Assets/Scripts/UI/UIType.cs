using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIType
{ 
    public string UIName { get; private set; }
    public string UIPath { get; private set; }

    public UIType(string path)
    {
        UIPath = path;
        UIName = path.Substring(path.LastIndexOf('/') + 1);
    }
}
