using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBattleUI : BasePanel
{

    private CurrentPieceShowUI currentPieceShowUI;
    private OtherPiecesQueueUI otherPiecesQueueUI;
    private CellInfoUI cellInfoUI;

    public override void OnEnter()
    {
        UIManager.Instance.PauseBeforePanel();
        otherPiecesQueueUI.SetPiecesUI(PieceQueueManager.Instance.PieceActionList);
        Select.Instance.canMove = true;
        gameObject.SetActive(true);
        if(!BattleGameManager.Instance.isBattling)
            EventCenter.Instance.EventTrigger("ShowCombatantNum");
        EventCenter.Instance.EventTrigger<Cell>("ShowCellInfo", Select.Instance.currentCell);
    }

    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
    }

    public override void OnPause()
    {
        base.OnPause();
        gameObject.SetActive(false);
    }

    public override void OnResume()
    {
        base.OnResume();
        gameObject.SetActive(true);
        EventCenter.Instance.EventTrigger<Piece>("CurrentPieceUIChange", PieceQueueManager.Instance.TheFirstPiece());
    }

    public override void Init()
    {
        base.Init();
        currentPieceShowUI = transform.GetComponentInChildren<CurrentPieceShowUI>();
        otherPiecesQueueUI = transform.GetComponentInChildren<OtherPiecesQueueUI>();
        cellInfoUI = TransformHelper.GetChildTransform(transform, "CellInfo").GetComponent<CellInfoUI>();
        currentPieceShowUI.Init(this);
        otherPiecesQueueUI.Init(this);
        cellInfoUI.Init();
    }
}
