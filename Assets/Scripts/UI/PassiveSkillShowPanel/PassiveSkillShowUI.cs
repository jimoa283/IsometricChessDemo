using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkillShowUI : BasePanel
{
    private PassiveSkillShowPanel passiveSkillShowPanel;

    public override void Init()
    {
        base.Init();
        passiveSkillShowPanel = GetComponent<PassiveSkillShowPanel>();
        passiveSkillShowPanel.Init();
    }
}
