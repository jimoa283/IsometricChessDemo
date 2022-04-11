using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ֪ʶ��
/// 1.AB����ص�API
/// 2.����ģʽ
/// 3.ί��-->Lambda���ʽ
/// 4.Э��
/// 5.�ֵ�
/// </summary>
public class ABMgr : MSingleton<ABMgr>
{
    // AB�������� Ŀ����
    // ���ⲿ������Ľ�����Դ����

    // ����
    private AssetBundle mainAB = null;
    // ��������ȡ�õ������ļ�
    private AssetBundleManifest mainfest = null;

    // AB�������ظ����� �ظ����ػᱨ��
    // �ֵ� ���ֵ����洢 ���ع���AB��
    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// ���AB�����·�� �����޸�
    /// </summary>
    private string PathUrl
    {
       get
        {
            return Application.streamingAssetsPath + "/";
        }
    }

    /// <summary>
    /// ������ �����޸�
    /// </summary>
    private string MainABName
    { 
        get
        {
#if UNITY_IOS
       return "IOS";
#elif UNITY_ANDROID
       return "Android"
#else
       return "PC";
#endif
        }
    }

    /// <summary>
    /// ����AB��
    /// </summary>
    /// <param name="abName"></param>
    public void LoadAB(string abName)
    {
        // ����AB��
        if (mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
            mainfest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        // ����Ҫ��ȡ�����������Ϣ
        AssetBundle ab;
        string[] strs = mainfest.GetAllDependencies(abName);
        for (int i = 0; i < strs.Length; ++i)
        {
            // �жϰ��Ƿ���ع�
            if (!abDic.ContainsKey(strs[i]))
            {
                ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
                abDic.Add(strs[i], ab);
            }
        }
        // ������Դ��Դ��
        // ���û�м��ع� �ټ���
        if (!abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(PathUrl + abName);
            abDic.Add(abName, ab);
        }
    }

    // ͬ������
    public object LoadRes(string abName,string resName)
   {
        LoadAB(abName);

        // ������Դ
        Object obj=abDic[abName].LoadAsset(resName);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
    }


    /// <summary>
    /// ͬ������ ����type
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public object LoadRes(string abName,string resName,System.Type type)
    {

        LoadAB(abName);

        // ������Դ
        Object obj = abDic[abName].LoadAsset(resName,type);
        if (obj is GameObject)
            return GameObject.Instantiate(obj);
        else
            return obj;
    }

    /// <summary>
    /// ͬ������ ���ݷ���
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    public T LoadRes<T>(string abName,string resName) where T:Object
    {
        LoadAB(abName);

        // ������Դ
        T obj = abDic[abName].LoadAsset<T>(resName);
        if (obj is GameObject)
            return GameObject.Instantiate(obj);
        else
            return obj;
    }

   // �첽����
   // ������첽���� AB����û��ʹ���첽����
   // ֻ�Ǵ�AB���� ������Դʱ ʹ���첽
   // ���������첽����
   public void LoadResAsync(string abName,string resName,UnityAction<Object> callBack)
   {
        StartCoroutine(ReallyLoadResAsync(abName, resName, callBack));
   }

    private IEnumerator ReallyLoadResAsync(string abName, string resName, UnityAction<Object> callBack)
    {
        LoadAB(abName);

        // ������Դ
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName);
        yield return abr;
        // �첽���ؽ����� ͨ��ί�� ���ݸ��ⲿ �ⲿ��ʹ��
        if (abr.asset is GameObject)
            callBack?.Invoke(Instantiate(abr.asset));
        else
            callBack?.Invoke(abr.asset);

    }

    // ����Type�첽������Դ
    public void LoadResAsync(string abName, string resName,System.Type type, UnityAction<Object> callBack)
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName,type,callBack));
    }

    private IEnumerator ReallyLoadResAsync(string abName, string resName,System.Type type,UnityAction<Object> callBack)
    {
        LoadAB(abName);

        // ������Դ
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName,type);
        yield return abr;
        // �첽���ؽ����� ͨ��ί�� ���ݸ��ⲿ �ⲿ��ʹ��
        if (abr.asset is GameObject)
            callBack?.Invoke(Instantiate(abr.asset));
        else
            callBack?.Invoke(abr.asset);

    }

    // ���ݷ����첽������Դ
    public void LoadResAsync<T>(string abName, string resName, UnityAction<T> callBack)where T:Object
    {
        StartCoroutine(ReallyLoadResAsync<T>(abName, resName, callBack));
    }

    private IEnumerator ReallyLoadResAsync<T>(string abName, string resName, UnityAction<T> callBack) where T : Object
    {
        LoadAB(abName);

        // ������Դ
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(resName);
        yield return abr;
        // �첽���ؽ����� ͨ��ί�� ���ݸ��ⲿ �ⲿ��ʹ��
        if (abr.asset is GameObject)
            callBack?.Invoke(Instantiate(abr.asset) as T);
        else
            callBack?.Invoke(abr.asset as T);

    }

    // ������ж��
    public void Unload(string abName)
   {
        if(abDic.ContainsKey(abName))
        {
            abDic[abName].Unload(false);
            abDic.Remove(abName);
        }    
   }

   // ���а���ж��
   public void ClearAB()
   {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        mainAB = null;
        mainfest = null;
   }
}
