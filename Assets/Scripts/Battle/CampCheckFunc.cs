using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampCheckFunc 
{


   public static bool CampAgainstCheck(Piece owner,Piece target)
    {
        if(owner.CompareTag("Player"))
        {
            switch (target.tag)
            {
                case "Player":
                    return false;
                case "Enemy":
                    return true;
                default:
                    break;
            }
        }
        else if(owner.CompareTag("Enemy"))
        {
            switch (target.tag)
            {
                case "Player":
                    return true;
                case "Enemy":
                    return false;
                default:
                    break;
            }
        }

        return false;
    }

    public static bool CampFriendCheck(Piece owner, Piece target)
    {
        if (owner.CompareTag("Player"))
        {
            switch (target.tag)
            {
                case "Player":
                    return true;
                case "Enemy":
                    return false;
                default:
                    break;
            }
        }
        else if (owner.CompareTag("Enemy"))
        {
            switch (target.tag)
            {
                case "Player":
                    return false;
                case "Enemy":
                    return true;
                default:
                    break;
            }
        }

        return false;
    }
}
