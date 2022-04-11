using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSingleton<T> : MonoBehaviour where T : MSingleton<T>
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    new GameObject("Singleton of" + typeof(T).Name).AddComponent<T>();
                }
                else
                {
                    instance.Init();
                }
            }
            return instance;
        }
    }

    protected virtual void Init()
    {

    }

    protected void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            Init();
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}