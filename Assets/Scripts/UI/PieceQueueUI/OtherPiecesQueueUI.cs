using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherPiecesQueueUI : MonoBehaviour
{
    private PieceSlot[] pieceQueue;
    private ScrollRect rect;
    private BasePanel panel;
    private PieceSlot currentSlot;
    [SerializeField]private int queueIndex;

    public int QueueIndex { get => queueIndex;
        set
        {
            if (value < 0 || value >= pieceQueue.Length)
                return;
            queueIndex = value;
            float pos = ((float)queueIndex / (pieceQueue.Length - 9));
            if (pos > 1)
                pos = 1;
            rect.horizontalNormalizedPosition = pos;
        }
    }

    public void Init(BasePanel panel)
    {
        pieceQueue = transform.GetComponentsInChildren<PieceSlot>();
        rect = GetComponentInChildren<ScrollRect>();
        this.panel = panel;

        foreach(var slot in pieceQueue)
        {
            slot.Init();
        }

        EventCenter.Instance.AddEventListener<List<Piece>>("SetPiecesUI", SetPiecesUI);
        EventCenter.Instance.AddEventListener<int>("ShowSelectPieceUIPlace", ShowSelectPieceUIPlace);
        //EventCenter.Instance.AddEventListener("ReturnToFirstPiece", ReturnToFirstPiece);
    }

    private void Update()
    {
        if(UIManager.Instance.GetTopPanel()==panel)
        {
            if(Input.GetKeyDown(KeyCode.N))
            {
                --QueueIndex;
            }

            if(Input.GetKeyDown(KeyCode.M))
            {
                ++QueueIndex;
            }
        }
    }

    public void SetPiecesUI(List<Piece> queue)
    {
        for (int i = 1; i < queue.Count; i++)
        {
            pieceQueue[i-1].SetPiece(queue[i]);         
        }
        //rect.horizontalNormalizedPosition = 0;
        QueueIndex = 0;
        EventCenter.Instance.EventTrigger<Piece>("CurrentPieceUIChange", PieceQueueManager.Instance.TheFirstPiece());
    }

    /*public void ReturnToFirstPiece()
    {
        HideBeforeSlot();
        QueueIndex = 0;
    }*/

    public void ShowSelectPieceUIPlace(int num)
    {
        HideBeforeSlot();           
        if (num <=0)
        {
            return;
        }
        QueueIndex = num / 9;
        pieceQueue[num-1].ShowPiece(num-1);
        currentSlot = pieceQueue[num - 1];
    }

    private void HideBeforeSlot()
    {
        if (currentSlot != null)
        {
            currentSlot.ResetSlot();
            QueueIndex = 0;
            currentSlot = null;
        }
    }

    void OnDestroy()
    {
        EventCenter.Instance.RemoveEvent<List<Piece>>("SetPiecesUI", SetPiecesUI);
        EventCenter.Instance.RemoveEvent<int>("ShowSelectPiecePlace", ShowSelectPieceUIPlace);
        //EventCenter.Instance.RemoveEvent("ReturnToFirstPiece", ReturnToFirstPiece);
    }
}
