using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PieceSelectUI : MonoBehaviour
{
    private Transform pieceSelectCotainer;
    private Vector3 originalPos;
    private RectTransform rt;
    private CanvasGroup canvasGroup;

    private List<PieceSelectSlot> pieceSelectSlots;
    private PieceSelectSlot[] first;

    private int slotIndex;

    public int SlotIndex { get => slotIndex;
        set
        {
            if (value >= 0 && value < pieceSelectSlots.Count)
            {
                pieceSelectSlots[slotIndex].CancelSelect(true);
                if (value > slotIndex && slotIndex >= 8)
                {
                    pieceSelectCotainer.DOLocalMoveX(pieceSelectCotainer.localPosition.x - 42f, 0.1f);
                }
                else if (value < slotIndex && slotIndex >= 9)
                {
                    pieceSelectCotainer.DOLocalMoveX(pieceSelectCotainer.localPosition.x + 42f, 0.1f);
                }
                slotIndex = (value + pieceSelectSlots.Count) % pieceSelectSlots.Count;
                pieceSelectSlots[slotIndex].SelectPiece(true);
            }
        }
    }

    public void Init()
    {
        rt = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        pieceSelectCotainer = transform.Find("pieceList/pieceListContent");
        originalPos = pieceSelectCotainer.localPosition;
        first = GetComponentsInChildren<PieceSelectSlot>();
        pieceSelectSlots = new List<PieceSelectSlot>(); 

        int i = 0;
        for (i = 0; i < BattleGameManager.Instance.playerList.Count; i++)
        {
            first[i].Init();
            first[i].CancelSelect(false);
            pieceSelectSlots.Add(first[i]);
            first[i].SetPiece(BattleGameManager.Instance.playerList[i]);
        }
        for(;i<first.Length;i++)
        {
            Destroy(first[i].gameObject);
        }
        slotIndex = 0;
    }

    void Update()
    {
      /*  if (Input.GetKeyDown(KeyCode.A))
            --SlotIndex;

        if (Input.GetKeyDown(KeyCode.D))
            ++SlotIndex;*/
       
    }

    public void OpenPieceSelectUI(Piece piece)
    {
        if (piece.CompareTag("Player"))
            gameObject.SetActive(true);
        else
        {
            gameObject.SetActive(false);
            return;
        }
        rt.localPosition = new Vector3(rt.localPosition.x, 220);
        canvasGroup.alpha = 0;
        rt.DOLocalMoveY(150, 0.5f);
        canvasGroup.DOFade(1, 0.5f);
        foreach (var slot in pieceSelectSlots)
        {
            slot.CancelSelect(false);
        }
        if (BattleGameManager.Instance.isInBattleScene)
            TransformToRequirePos(piece);
        else
            SlotIndex = 0;
    }

   public void TransformToRequirePos(Piece piece)
    {
        gameObject.SetActive(true);
        pieceSelectSlots[slotIndex].CancelSelect(false);
        int index = PieceQueueManager.Instance.GetPieceIndex(piece);
        if (0 <= index && index <= 8)
            pieceSelectCotainer.transform.localPosition = originalPos;
        else
        {
            pieceSelectCotainer.transform.localPosition += Vector3.right * (index - 8) * -42f;
        }
        slotIndex = index;
        pieceSelectSlots[slotIndex].SelectPiece(false);
    }

    public void SetPieceBattle()
    {
        pieceSelectSlots[slotIndex].SetPieceBattle();
    }
}
