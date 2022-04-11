using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefInfoUI : BasePanel
{
    private BriefInfoContent briefInfoContent;

    public BriefInfoContent BriefInfoContent { get => briefInfoContent; set => briefInfoContent = value; }

    public override void Init()
    {
        base.Init();
        briefInfoContent = TransformHelper.GetChildTransform(transform, "Content").GetComponent<BriefInfoContent>();
        briefInfoContent.Init();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        gameObject.SetActive(true);
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
