using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    private GameObject poolObj;

    private Dictionary<string, Queue<GameObject>> prefabDic;

    private Dictionary<string, string> prefabPathDic;

    public PoolManager()
    {   
        prefabDic = new Dictionary<string, Queue<GameObject>>();
        prefabPathDic = new Dictionary<string, string>();
        
        ParsePoolObjJson();
    }

    private void ParsePoolObjJson()
    {
        TextAsset textPoolObj = Resources.Load<TextAsset>("JSON/PoolObjJson");
        JSONObject jSONObject = new JSONObject(textPoolObj.text);

        foreach(var obj in jSONObject.list)
        {
            string poolObjName = obj["PoolObjName"].str;
            string poolObjPath = obj["PoolObjPath"].str;
            prefabPathDic.Add(poolObjName, poolObjPath);
        }
    }

    public GameObject GetObj(string prefabName)
    {
        GameObject temp = null;

        //若字典中有登记这种预制件并且该种物品在对象池中的数量大于0
        if (prefabDic.ContainsKey(prefabName) && prefabDic[prefabName].Count > 0)
        {
            temp = prefabDic[prefabName].Dequeue();
        }
        //否则新生成一个物体传出去
        else
        {
            temp = GameObject.Instantiate(Resources.Load<GameObject>(prefabPathDic[prefabName]));
        }
        //在传出去前显示该物体
        temp.SetActive(true);
        temp.transform.SetParent(null);
        return temp;
    }

    public GameObject GetPoolObj(string objName)
    {
        return Resources.Load<GameObject>(prefabPathDic[objName]);
    }

    public void PushObj(string prefabName,GameObject obj)
    {
        if(poolObj==null)
            poolObj = new GameObject("PoolManager");

        if (prefabDic.ContainsKey(prefabName))
        {
            prefabDic[prefabName].Enqueue(obj);                    
        }
        //字典中没有
        else
        {
            //创建这个预制体的缓存池数据          
            prefabDic.Add(prefabName, new Queue<GameObject>());
            prefabDic[prefabName].Enqueue(obj);
            //如果根目录下没有这个预制体命名的子物体   
            if (!poolObj.transform.Find(prefabName))
            {
                new GameObject(prefabName).transform.SetParent(poolObj.transform);
            }
        }

        obj.SetActive(false);
        obj.transform.SetParent(poolObj.transform.Find(prefabName));
    }



    public void ClearDic()
    {
        GameObject.Destroy(poolObj);
        poolObj = null;
        prefabDic.Clear();
    }
}
