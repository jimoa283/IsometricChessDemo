using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PieceSelectSlot : MonoBehaviour
{
    private Image pieceImage;
    private Image battleIcon;
    private GameObject highLightMask;
    private Piece piece;

    public void Init()
    {
        pieceImage = transform.Find("pieceImage").GetComponent<Image>();
        battleIcon = transform.Find("BattleIcon").GetComponent<Image>();
        highLightMask = transform.Find("HighLightMask").gameObject;
    }

    public void SetPiece(Piece piece)
    {
        this.piece = piece;
        pieceImage.sprite = piece.pieceStatus.pieceSprite;
        if (PieceQueueManager.Instance.PieceList.Contains(piece))
        {
            battleIcon.gameObject.SetActive(true);
            if(BattleGameManager.Instance.importantPlayerList.Contains(piece as Player))
            {
                battleIcon.color = Color.green;
            }
            else
            {
                battleIcon.color = Color.white;
            }
        }           
        else
            battleIcon.gameObject.SetActive(false);
    }

    public void SetPieceBattle()
    {
        if (PieceQueueManager.Instance.PlayerList.Count == BattleGameManager.Instance.maxPlayerNum||PieceQueueManager.Instance.PieceList.Contains(piece))
            return;

        battleIcon.gameObject.SetActive(true);
        PieceQueueManager.Instance.AddSettingPiece(piece);
    }

    public void SelectPiece(bool isMove)
    {
        if (isMove)
            transform.DOLocalMoveY(-8, 0.1f, true);
        else
            transform.DOLocalMoveY(-8,0.1f,true);
            //transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - 8);
        highLightMask.SetActive(true);
    }

    public void CancelSelect(bool isMove)
    {
        if (isMove)
            transform.DOLocalMoveY(0, 0.1f,true);
        else
            transform.DOLocalMoveY(0,0.05f,true);
        highLightMask.SetActive(false);
    }

    public void CancelBattle()
    {
        battleIcon.gameObject.SetActive(false);
    }
}
