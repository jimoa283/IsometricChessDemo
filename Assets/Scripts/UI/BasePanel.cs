using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel :MonoBehaviour
{
    /// <summary>
    /// 设置当前panel的操控状态
    /// </summary>
   [SerializeField]protected bool canControll;


   public virtual void Init()
    {       
        
    }


    /// <summary>
    /// 该panel新建
    /// </summary>
    public virtual void OnEnter()
    {
        canControll = true;
    }

    /// <summary>
    /// 该panel暂停，即不能操控
    /// </summary>
    public virtual void OnPause()
    {
        canControll = false;
    }

    /// <summary>
    /// 该panel继续，即回复操控
    /// </summary>
    public virtual void OnResume()
    {
        canControll = true;
    }

    /// <summary>
    /// 该panel退出，即关闭
    /// </summary>
    public virtual void OnExit()
    {
        canControll = false; 
    }
        
}
