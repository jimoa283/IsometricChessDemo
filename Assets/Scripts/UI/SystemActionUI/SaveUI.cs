using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveUI : BasePanel
{
    private SaveList saveList;

    public override void Init()
    {
        base.Init();
        saveList = TransformHelper.GetChildTransform(transform, "SaveList").GetComponent<SaveList>();
        saveList.Init();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        gameObject.SetActive(true);
        //EventCenter.Instance.EventTrigger("HideMissionInfo");
        saveList.SetAllSaveSlot();
    }

    public override void OnExit()
    {
        base.OnExit();
        //EventCenter.Instance.EventTrigger("ShowMissionInfo");
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
