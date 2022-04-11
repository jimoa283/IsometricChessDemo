using UnityEngine;

public static class TransformHelper 
{
   public static Transform GetChildTransform(this Transform transform,string childName)
   {
        Transform childTF = transform.Find(childName);
        if (childTF != null) return childTF;

        for(int i=0;i<transform.childCount;i++)
        {
            childTF = GetChildTransform(transform.GetChild(i), childName);
            if (childTF != null) return childTF;
        }
        return null;
   }
}
