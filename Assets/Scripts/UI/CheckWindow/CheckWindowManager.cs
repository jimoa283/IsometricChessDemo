using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckWindowManager : Singleton<CheckWindowManager>
{
    private Dictionary<string, SimpleCheckWindowData> simpleCheckWindowDic;
    private Dictionary<string, PersuadeWindowData> persuadeWindowDic; 

    public CheckWindowManager()
    {
        simpleCheckWindowDic = new Dictionary<string, SimpleCheckWindowData>();
        persuadeWindowDic = new Dictionary<string, PersuadeWindowData>();
        ParseSimpleCheckWindowDataJSON();
        ParsePersuadeWindowJSON();
    }

    private void ParseSimpleCheckWindowDataJSON()
    {
        TextAsset textSimpleCheckWindowData = Resources.Load<TextAsset>("JSON/SimpleCheckWindowDataJSON");
        JSONObject jSONObject = new JSONObject(textSimpleCheckWindowData.text);

        foreach(var obj in jSONObject.list)
        {
            string _name = obj["Name"].str;

            string checkWindowInfo = obj["CheckWindowInfo"].str;
            List<string> buttonTextList = new List<string>();
            foreach (var _obj in obj["ButtonTextList"].list)
            {
                string buttonText = _obj.str;
                buttonTextList.Add(buttonText);
            }

            SimpleCheckWindowData simpleCheckWindowData = new SimpleCheckWindowData(checkWindowInfo,buttonTextList);

            simpleCheckWindowDic.Add(_name, simpleCheckWindowData);

        }
    }

    private void ParsePersuadeWindowJSON()
    {
        TextAsset textPersuadeWindowData = Resources.Load<TextAsset>("JSON/PersuadeWindowDataJSON");
        JSONObject jSONObject = new JSONObject(textPersuadeWindowData.text);

        foreach(var obj in jSONObject.list)
        {
            string _name = obj["Name"].str;

            string pieceName = obj["PieceName"].str;
            string pieceSpeakInfoShow = obj["PieceSpeakInfoShow"].str.Replace('#','\n');
            Sprite pieceImage = Resources.Load<Sprite>("Sprite/"+obj["PieceImagePath"].str);
            AttitudeType attitudeType = (AttitudeType)System.Enum.Parse(typeof(AttitudeType), obj["AttitudeType"].str);

            List<string> buttonTextList = new List<string>();
            foreach(var _obj in obj["ButtonTextList"].list)
            {
                string buttonText = _obj.str;
                buttonTextList.Add(buttonText);
            }

            List<int> persuadeLevelList = new List<int>();
            foreach(var _obj in obj["PersuadeLevelList"].list)
            {
                int persuadeLevel = (int)_obj.n;
                persuadeLevelList.Add(persuadeLevel);
            }

            PersuadeWindowData persuadeWindowData = new PersuadeWindowData(pieceName, pieceSpeakInfoShow, pieceImage, attitudeType, buttonTextList, persuadeLevelList);

            persuadeWindowDic.Add(_name, persuadeWindowData);
        } 
    }

    public void SetSimpleCheckWindow(string checkWindowName,UnityAction[] actions)
    {
        if (!simpleCheckWindowDic.ContainsKey(checkWindowName))
            return;

        SimpleCheckWindowData simpleCheckWindowData = simpleCheckWindowDic[checkWindowName];
        SimpleCheckWindowUI simpleCheckWindowUI = UIManager.Instance.GetPanel(UIPanelType.SimpleCheckWindowUI) as SimpleCheckWindowUI;
        simpleCheckWindowUI.SetSimpleCheckWindow(actions, simpleCheckWindowData);
        UIManager.Instance.PushPanel(simpleCheckWindowUI);
    }

   public void SetPersuadeWindow(string windowName,UnityAction[] actions)
    {
        if (!persuadeWindowDic.ContainsKey(windowName))
            return;

        PersuadeWindowData persuadeWindowData = persuadeWindowDic[windowName];
        PersuadeWindowUI persuadeWindowUI = UIManager.Instance.GetPanel(UIPanelType.PersuadeWindowUI) as PersuadeWindowUI;
        persuadeWindowUI.SetPersuadeWindow(actions, persuadeWindowData);
        UIManager.Instance.PushPanel(persuadeWindowUI);
    }
}

public enum CheckWindowType
{
    Simple,
    Persuade
}

/*public class CheckWindowData
{
    public CheckWindowType CheckWindowType;

    public CheckWindowData(CheckWindowType checkWindowType)
    {
        CheckWindowType = checkWindowType;
    }
   
}*/

public class SimpleCheckWindowData
{
    public string CheckWindowInfo;
    public List<string> ButtonTextList;

    public SimpleCheckWindowData(string checkWindowInfo,List<string> buttonTextList)
    {
        CheckWindowInfo = checkWindowInfo;
        ButtonTextList = buttonTextList;
    }
}

public enum AttitudeType
{
    Applaud,
    Against,
    Neutrality
}

public class PersuadeWindowData
{
    public string PieceName;
    public string PieceSpeakInfoShow;
    public Sprite PieceImage;
    public AttitudeType AttitudeType;
    public List<string> ButtoTextList;
    public List<int> PersuadeLevelList;

    public PersuadeWindowData(string pieceName, string pieceSpeakInfoShow, Sprite pieceImage, AttitudeType attitudeType, List<string> buttoTextList, List<int> persuadeLevelList)
    {
        PieceName = pieceName;
        PieceSpeakInfoShow = pieceSpeakInfoShow;
        PieceImage = pieceImage;
        AttitudeType = attitudeType;
        ButtoTextList = buttoTextList;
        PersuadeLevelList = persuadeLevelList;
    }
}


