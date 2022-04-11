using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefIntelligenceUI : BasePanel
{
    private BriegIntelligenceShowPanel briegIntelligenceShowPanel;

    public override void Init()
    {
        base.Init();
        briegIntelligenceShowPanel = TransformHelper.GetChildTransform(transform, "BriefIntelligenceShowPanel").GetComponent<BriegIntelligenceShowPanel>();
        briegIntelligenceShowPanel.Init();
    }

    public void ShowBriefIntelligence(BriefIntelligence briefIntelligence)
    {
        briegIntelligenceShowPanel.ShowBriefIntelligence(briefIntelligence);
    }
}
