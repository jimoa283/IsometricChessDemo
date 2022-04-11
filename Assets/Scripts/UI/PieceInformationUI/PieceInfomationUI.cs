using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceInfomationUI : BasePanel
{
    private PieceDetailInfoUI pieceDetailInfoUI;
    private PieceSelectUI pieceSelectUI;
    private Piece currentPiece;

    public override void Init()
    {
        base.Init();
        pieceDetailInfoUI = GetComponentInChildren<PieceDetailInfoUI>();
        pieceSelectUI = GetComponentInChildren<PieceSelectUI>();
        pieceSelectUI.Init();
        pieceDetailInfoUI.Init();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (canControll)
            {
                UIManager.Instance.PopPanel(UIPanelType.PieceInformationUI);
                Select.Instance.WakeUp();
            }
            else
                canControll = true;
        }    

        if(Input.GetKeyDown(KeyCode.J))
        {
            if (canControll)
                canControll = false;
        }

        if(!BattleGameManager.Instance.isBattling&&canControll)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                --pieceSelectUI.SlotIndex;
                currentPiece = BattleGameManager.Instance.playerList[pieceSelectUI.SlotIndex];
                pieceDetailInfoUI.ChangePieceInfo(currentPiece);
            }                
            if (Input.GetKeyDown(KeyCode.D))
            {
                ++pieceSelectUI.SlotIndex;
                currentPiece = BattleGameManager.Instance.playerList[pieceSelectUI.SlotIndex];
                pieceDetailInfoUI.ChangePieceInfo(currentPiece);
            }
            if(BattleGameManager.Instance.isInBattleScene)
            {
                if (Input.GetKeyDown(KeyCode.L))
                {
                    pieceSelectUI.SetPieceBattle();
                }
                if (Input.GetKeyDown(KeyCode.K))
                {

                }
            }            
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        UIManager.Instance.PauseBeforePanel();
        if (BattleGameManager.Instance.isInBattleScene)
            currentPiece = Select.Instance.currentCell.currentPiece;
        else
            currentPiece = BattleGameManager.Instance.playerList[0];
        pieceSelectUI.OpenPieceSelectUI(currentPiece);
        pieceDetailInfoUI.OpenDetailInfoUI(currentPiece);
        gameObject.SetActive(true);
    }

    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
    }

    public override void OnPause()
    {
        base.OnPause();
    }

    public override void OnResume()
    {
        base.OnResume();
    }

   
}
