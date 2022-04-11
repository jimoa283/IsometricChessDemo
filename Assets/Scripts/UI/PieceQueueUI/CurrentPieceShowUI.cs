using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentPieceShowUI : MonoBehaviour
{
    private Image pieceBG;
    private Image pieceImage;
    private Text pieceName;
    private Text pieceProfecess;
    private Text pieceLevel;
    private Text pieceExp;
    private Text pieceHpValue;
    private Image pieceHealthBar;
    private Image expBar;
    private BasePanel panel;

    private PieceBuffShowSlot[] pieceBuffShowSlots;

    public  void Init(BasePanel panel)
    {
        pieceBG = TransformHelper.GetChildTransform(transform,"CurrentPieceCampBG").GetComponent<Image>();
        pieceImage = TransformHelper.GetChildTransform( transform,"CurrentPieceImage").GetComponent<Image>();
        pieceName = TransformHelper.GetChildTransform(transform,"PieceName").GetComponent<Text>();
        pieceProfecess = TransformHelper.GetChildTransform( transform,"PieceName").GetComponent<Text>();
        pieceHealthBar = TransformHelper.GetChildTransform(transform, "RealHealthBar").GetComponent<Image>();
        pieceHpValue = TransformHelper.GetChildTransform(transform, "HPValue").GetComponent<Text>();
        pieceLevel = TransformHelper.GetChildTransform(transform, "CurrentPieceLV").GetComponent<Text>();
        pieceExp = TransformHelper.GetChildTransform(transform, "CurrentPieceExp").GetComponent<Text>();
        expBar = TransformHelper.GetChildTransform(transform, "ExpBar").GetComponent<Image>();
        pieceBuffShowSlots = GetComponentsInChildren<PieceBuffShowSlot>();
        foreach(var slot in pieceBuffShowSlots)
        {
            slot.Init();
        }
        this.panel = panel;
        EventCenter.Instance.AddEventListener<Piece>("CurrentPieceUIChange", CurrentPieceUIChange);
    }

    public void CurrentPieceUIChange(Piece piece)
    { 
        if (piece.CompareTag("Player"))
            pieceBG.color = Color.blue;
        else
            pieceBG.color = Color.red;
        pieceLevel.text = "等级" + piece.pieceStatus.Level;
        pieceExp.text = "经验值" + piece.pieceStatus.CurrentExp;
        pieceImage.sprite = piece.pieceStatus.pieceSprite;
        pieceName.text = piece.pieceStatus.pieceName;
        pieceHpValue.text = "Hp <size=20>"+piece.pieceStatus.CurrentHealth+"</size>/"+piece.pieceStatus.maxHealth;
        pieceHealthBar.fillAmount = (float)piece.pieceStatus.CurrentHealth / piece.pieceStatus.maxHealth;
        expBar.fillAmount = (float)piece.pieceStatus.CurrentExp / piece.pieceStatus.levelUpExpList[piece.pieceStatus.Level];
        ShowPieceBUFF(piece.PieceBUFFList);
    }

    public void ShowPieceBUFF(PieceBUFFList pieceBUFFList)
    {
        int i = 0;
        for (i = 0; i < pieceBuffShowSlots.Length; i++)
        {
            if (i == pieceBUFFList.BuffList.Count)
                break;
            pieceBuffShowSlots[i].ShowPieceBuff(pieceBUFFList.BuffList[i]);
        }
        for (; i < pieceBuffShowSlots.Length; i++)
        {
            pieceBuffShowSlots[i].gameObject.SetActive(false);
        }
    }

    void OnDestroy()
    {
        EventCenter.Instance.RemoveEvent<Piece>("CurrentPieceUIChange",CurrentPieceUIChange);
    }
}
