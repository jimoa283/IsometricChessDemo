using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUI : BasePanel
{
    private LoadList loadList;

    public override void Init()
    {
        base.Init();
        loadList = TransformHelper.GetChildTransform(transform, "LoadList").GetComponent<LoadList>();
        loadList.Init();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        gameObject.SetActive(true);
        
        loadList.SetAllSaveSlot();
    }

    public override void OnExit()
    {
        base.OnExit();
        
        gameObject.SetActive(false);
    }

    public override void OnPause()
    {
        base.OnPause();
    }

    public override void OnResume()
    {
        base.OnResume();
    }
}
