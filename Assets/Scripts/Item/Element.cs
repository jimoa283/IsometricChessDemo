using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType
{
    Item,
    Skill
}

public class Element 
{
    public int ID;
    public string Name;
    public ElementType Type;
    public string Info;
    //public string SpritePath;
    public Sprite Sprite;
    public Piece Piece;

    public Element(int iD, string name, ElementType type, string info,Sprite sprite)
    {
        ID = iD;
        Name = name;
        Type = type;
        Info = info;
        Sprite = sprite;
    }
}
