using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefIntelligenceManager : Singleton<BriefIntelligenceManager>
{
    private Dictionary<int, BriefIntelligence> briefIntelligenceDic;

    private BriefIntelligenceUI briefIntelligenceUI;

    public BriefIntelligenceManager()
    {
        briefIntelligenceDic = new Dictionary<int, BriefIntelligence>();
        ParseBriefIntelligenceJSON();
    }

    private void ParseBriefIntelligenceJSON()
    {
        TextAsset textBriefIntelligence = Resources.Load<TextAsset>("JSON/BriefIntelligenceJSON");
        JSONObject jSONObject = new JSONObject(textBriefIntelligence.text);

        foreach(var obj in jSONObject.list)
        {
            int id = (int)obj["ID"].n;
            string _name = obj["Name"].str;

            BriefIntelligence briefIntelligence = new BriefIntelligence(_name);

            briefIntelligenceDic.Add(id, briefIntelligence);
        }
    }

    public BriefIntelligence GetBriefIntelligence(int id)
    {
        if (briefIntelligenceDic.ContainsKey(id))
            return briefIntelligenceDic[id];

        return null;
    }

    public void ShowBriefIntelligence(int id)
    {
        if (!briefIntelligenceDic.ContainsKey(id))
            return;

        if (briefIntelligenceUI == null)
            briefIntelligenceUI = UIManager.Instance.GetPanel(UIPanelType.BriefIntelligenceUI) as BriefIntelligenceUI;

        Debug.Log("OK2");
        briefIntelligenceUI.ShowBriefIntelligence(briefIntelligenceDic[id]);
    }
}

public class BriefIntelligence
{
    public bool HasGet;
    public string Name;

    public BriefIntelligence(string name)
    {
        HasGet = false;
        Name = name;
    }
}
