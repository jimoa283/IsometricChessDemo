using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceSlot : MonoBehaviour
{
    private Image pieceSprite;
    private Image pieceSlotBG;
    private Text pieceIndex;

    public void Init()
    {
        pieceSprite = transform.Find("PieceImage").GetComponent<Image>();
        pieceSlotBG = GetComponent<Image>();
        pieceIndex = transform.Find("PieceIndex").GetComponent<Text>();
    }

    public void SetPiece(Piece piece)
    {
        transform.localScale = Vector3.one;
        if(piece.CompareTag("Player"))
        {
            pieceSlotBG.color = Color.blue;
        }
        else if(piece.CompareTag("Enemy"))
        {
            pieceSlotBG.color = Color.red;
        }
        pieceSprite.sprite = piece.pieceStatus.pieceSprite;
        pieceIndex.text = "";
    }

    public void ShowPiece(int num)
    {
        pieceIndex.gameObject.SetActive(true);
        transform.localScale = new Vector3(1.2f, 1.2f, 0);
        pieceIndex.text = (num+2).ToString();
    }

    public void ResetSlot()
    {
        transform.localScale = Vector3.one;
        pieceIndex.gameObject.SetActive(false);
    }
}
