using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PieceIntroduceManager : Singleton<PieceIntroduceManager>
{
    private Dictionary<string, PieceIntroduceData> pieceIntroduceDic;
    private PieceIntroduceUI pieceIntroduceUI;

    public PieceIntroduceManager()
    {
        pieceIntroduceDic = new Dictionary<string, PieceIntroduceData>();
        ParsePieceIntroduceJSON();
    }

    private void ParsePieceIntroduceJSON()
    {
        TextAsset textPieceIntroduce = Resources.Load<TextAsset>("JSON/PieceIntroduceJSON");
        JSONObject jSONObject = new JSONObject(textPieceIntroduce.text);

        foreach(var obj in jSONObject.list)
        {
            string pieceName = obj["PieceName"].str;
            Sprite pieceSprite = Resources.Load<Sprite>("Sprite/" + obj["PieceSpritePath"].str);
            Sprite pieceCampFlag = Resources.Load<GameObject>("Sprite/" + obj["PieceCampFlagPath"].str).GetComponent<SpriteRenderer>().sprite;
            string pieceInfo = (obj["PieceInfo"].str).Replace('#', '\n');

            PieceIntroduceData pieceIntroduceData = new PieceIntroduceData(pieceSprite, pieceCampFlag, pieceInfo);

            pieceIntroduceDic.Add(pieceName, pieceIntroduceData);
        }
    }

    public void SetPieceIntroduce(string pieceName,UnityAction action)
    {
        if (!pieceIntroduceDic.ContainsKey(pieceName))
            return;
        if(pieceIntroduceUI==null)
        {
            pieceIntroduceUI = GameObject.Instantiate((Resources.Load<GameObject>("UI/PieceIntroduceUI"))).GetComponent<PieceIntroduceUI>();
            pieceIntroduceUI.transform.SetParent(GameObject.Find("Canvas").transform, false);
            pieceIntroduceUI.Init();
        }
        pieceIntroduceUI.SetPieceIntroduce(pieceIntroduceDic[pieceName],action);
    }
}

public class PieceIntroduceData
{
    public Sprite PieceSprite;
    public Sprite PieceCampFlag;
    public string PieceInfo;

    public PieceIntroduceData(Sprite pieceSprite, Sprite pieceCampFlag, string pieceInfo)
    {
        PieceSprite = pieceSprite;
        PieceCampFlag = pieceCampFlag;
        PieceInfo = pieceInfo;
    }
}
