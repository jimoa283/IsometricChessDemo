using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SkillType
{
    Active,
    Passive
}

public enum LaunchType
{ 
  Shot,
  Instant
}


public enum RangeType
{
    Line,
    Circle
}

/*public enum TargetType
{
    BenefitFriend,
    BenefitSelf,
    AgainstEnemy,
    SetBlock
}*/



[System.Serializable]
public class Skill : Element
{
    public SkillType SkillType;

    public Piece Owner;
    public Skill(int iD, string name, ElementType type, string info, Sprite sprite, SkillType skillType ) : base(iD, name, type, info,sprite)
    {
        SkillType = skillType;
    }

    public virtual void SetPiece(Piece piece)
    {
        Owner = piece;
    }
}
