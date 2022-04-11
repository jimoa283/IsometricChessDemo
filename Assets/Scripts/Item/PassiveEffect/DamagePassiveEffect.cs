using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePassiveEffect :IPassiveEffect
{
    public virtual int DoDamageEffect(int damage, Piece piece, Piece target)
    {
        return damage;
    }
}
