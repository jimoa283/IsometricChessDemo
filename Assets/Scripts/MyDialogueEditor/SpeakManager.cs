using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeakManager : Singleton<SpeakManager>
{
    private Dictionary<string, SpeakData> speakDic;

    private UnityAction afterSpeakAction;

    public MyDialogueContainerSO currentDialogueContainer;

    public SpeakManager()
    {
        speakDic = new Dictionary<string,SpeakData>();
        ParseSpeakJson();
    }

    private void ParseSpeakJson()
    {
        TextAsset textSpeak = Resources.Load<TextAsset>("JSON/SpeakContentJSON");
        JSONObject jSONObject = new JSONObject(textSpeak.text);
        foreach (var obj in jSONObject.list)
        {
            string id = obj["ID"].str;
            string speakerName = obj["SpeakerName"].str;
            string speakContent = obj["SpeakContent"].str;
            string nextSpeakIndex =obj["NextSpeakIndex"].str;

            SpeakDataType speakDataType = (SpeakDataType)System.Enum.Parse(typeof(SpeakDataType), obj["SpeakDataType"].str);

            switch (speakDataType)
            {
                case SpeakDataType.SimpleSpeak:

                    SpeakData speakData = new SpeakData(speakerName, speakContent, nextSpeakIndex, speakDataType);
                    speakDic.Add(id, speakData);
                    break;

                case SpeakDataType.ShowIntroduceSpeak:

                    string pieceIntroduceName = obj["PieceIntroduceName"].str;

                    ShowIntroduceSpeak showIntroduceSpeak = new ShowIntroduceSpeak(speakerName, speakContent, nextSpeakIndex, speakDataType, pieceIntroduceName);
                    speakDic.Add(id, showIntroduceSpeak);
                    break;

                case SpeakDataType.ItemGetSpeak:

                    break;

                case SpeakDataType.SelectAnswerSpeak:

                    List<SingleSelectAnswer> selectAnswerList = new List<SingleSelectAnswer>();
                    foreach (var _obj in obj["SelectAnswerList"].list)
                    {
                        string answer = _obj["Answer"].str;
                        string _nextSpeakIndex = _obj["NextSpeakIndex"].str;
                        bool isLock = _obj["IsLock"].b;
                        LockType lockType = (LockType)System.Enum.Parse(typeof(LockType), _obj["LockType"].str);

                        switch (lockType)
                        {
                            case LockType.Intelligence:
                                int informationID = (int)_obj["InformationID"].n;
                                IntelligenceLockSelectAnswer informationLockSelectAnswer = new IntelligenceLockSelectAnswer(answer, _nextSpeakIndex, isLock, lockType, informationID);
                                selectAnswerList.Add(informationLockSelectAnswer);
                                break;
                            case LockType.Value:
                                int value = (int)_obj["Value"].n;
                                ValueLockSelectAnswer valueLockSelectAnswer = new ValueLockSelectAnswer(answer, _nextSpeakIndex, isLock, lockType, value);
                                selectAnswerList.Add(valueLockSelectAnswer);
                                break;
                            case LockType.Null:
                                SingleSelectAnswer singleSelectAnswer = new SingleSelectAnswer(answer, _nextSpeakIndex, isLock, lockType);
                                selectAnswerList.Add(singleSelectAnswer);
                                break;
                            default:
                                break;
                        }

                    }

                    SelectAnswerSpeak selectAnswerSpeak = new SelectAnswerSpeak(speakerName, speakContent, nextSpeakIndex, speakDataType, selectAnswerList);
                    speakDic.Add(id, selectAnswerSpeak);
                    break;

                case SpeakDataType.IntelligenceGetSpeak:

                    int intelligenceID = (int)obj["IntelligenceID"].n;
                    IntelligenceGetSpeak intelligenceGetSpeak = new IntelligenceGetSpeak(speakerName, speakContent, nextSpeakIndex, speakDataType,intelligenceID);
                    speakDic.Add(id, intelligenceGetSpeak);
                    break;

                case SpeakDataType.PlayerSelfSpeak:

                    break;

                default:
                    break;
            }

        }
    }

    public void StartSpeakById(string id,Vector3 pos,UnityAction action)
    {
        afterSpeakAction = action;
        SetSpeakById(id, pos);
    }

    public void StartSpeakBySO(MyDialogueContainerSO dialogueContainerSO,Vector3 pos,UnityAction action)
    {
        afterSpeakAction = action;
        SetSpeakBySO(dialogueContainerSO, pos);
    }

    public void SetSpeakById(string id, Vector3 pos)
    {
        if (id == null)
        {           
            afterSpeakAction?.Invoke();            
        }            
        else if(speakDic.ContainsKey(id))
        {
            DynamicSpeakUI speakUI = PoolManager.Instance.GetObj("SpeakUI").GetComponent<DynamicSpeakUI>();
            if(speakUI!=null)
              speakUI.StartSpeakByData(speakDic[id], pos);
        }
    }

    public void SetSpeakBySO(MyDialogueContainerSO dialogueContainerSO,Vector3 pos)
    {
        if(dialogueContainerSO==null)
        {
            afterSpeakAction?.Invoke();
        }
        else
        {
            SOSpeakUI speakUI = PoolManager.Instance.GetObj("SOSpeakUI").GetComponent<SOSpeakUI>();
            speakUI.StartSpeakBySO(dialogueContainerSO,pos,afterSpeakAction);
        }
    }

}

public enum SpeakDataType
{
    SimpleSpeak,
    ShowIntroduceSpeak,
    ItemGetSpeak,
    SelectAnswerSpeak,
    IntelligenceGetSpeak,
    PlayerSelfSpeak
}

[System.Serializable]
public class SpeakData
{
    public string SpeakerName;
    [TextArea]
    public string SpeakContent;
    public string NextSpeakIndex;
    public SpeakDataType SpeakDataType;

    public SpeakData(string speakerName, string speakContent, string nextSpeakIndex, SpeakDataType speakDataType)
    {
        SpeakerName = speakerName;
        SpeakContent = speakContent;
        NextSpeakIndex = nextSpeakIndex;
        SpeakDataType = speakDataType;
    }
}

public class ShowIntroduceSpeak : SpeakData
{
    public string PieceIntroduceName;

    public ShowIntroduceSpeak(string speakerName, string speakContent, string nextSpeakIndex, SpeakDataType speakDataType,string pieceIntroduceName):base(speakerName,speakContent,nextSpeakIndex,speakDataType)
    {
        PieceIntroduceName = pieceIntroduceName;
    }
}

public class SelectAnswerSpeak : SpeakData
{
    public List<SingleSelectAnswer> SelectAnswerList;

    public SelectAnswerSpeak(string speakerName, string speakContent, string nextSpeakIndex, SpeakDataType speakDataType,List<SingleSelectAnswer> selectAnswerList) : base(speakerName, speakContent, nextSpeakIndex, speakDataType)
    {
        SelectAnswerList = selectAnswerList;
    }
}

public enum LockType
{
    Intelligence,
    Value,
    Null
}

public class SingleSelectAnswer
{
    public string Answer;
    public string NextSpeakIndex;
    public bool IsLock;
    public LockType LockType;

    public SingleSelectAnswer(string answer, string nextSpeakIndex, bool isLock, LockType lockType)
    {
        Answer = answer;
        NextSpeakIndex = nextSpeakIndex;
        IsLock = isLock;
        LockType = lockType;
    }

    public virtual bool UnLockCheck()
    {
        return true;
    }
}

public class IntelligenceLockSelectAnswer : SingleSelectAnswer
{
    public int IntelligenceID;

    public IntelligenceLockSelectAnswer(string answer, string nextSpeakIndex, bool isLock, LockType lockType,int intelligenceID) :base(answer,nextSpeakIndex,isLock,lockType)
    {
        IntelligenceID = intelligenceID;
    }

    public override bool UnLockCheck()
    {
        if (!BriefIntelligenceManager.Instance.GetBriefIntelligence(IntelligenceID).HasGet)
            return false;
        else
        {
            BriefIntelligenceManager.Instance.ShowBriefIntelligence(IntelligenceID);
            return true;
        }           
    }
}

public class ValueLockSelectAnswer:SingleSelectAnswer
{
    public int Value;

    public ValueLockSelectAnswer(string answer, string nextSpeakIndex, bool isLock, LockType lockType,int value) : base(answer, nextSpeakIndex, isLock, lockType)
    {
        Value = value;
    }
}

public class IntelligenceGetSpeak:SpeakData
{
    public int IntelligenceID;

    public IntelligenceGetSpeak(string speakerName, string speakContent, string nextSpeakIndex, SpeakDataType speakDataType,int intelligenceID) : base(speakerName, speakContent, nextSpeakIndex, speakDataType)
    {
        IntelligenceID = intelligenceID;
    }
}

