using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceActionUI : BasePanel
{
    private ActionUI actionUI;

    public override void Init()
    {
        base.Init();
        actionUI = GetComponentInChildren<ActionUI>();
        actionUI.Init();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)&&!LevelManager.Instance.CurrentPiece.hasMove)
        {
            UIManager.Instance.PopPanel(UIPanelType.PieceActionUI);
            Select.Instance.ReturnObj();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            UIManager.Instance.PopPanel(UIPanelType.PieceActionUI);
            Select.Instance.ExitActionSelect();
            //Select.Instance.ReturnObj();
            LevelManager.Instance.CurrentPiece.pieceActionQueue.DoPieceAction();
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        gameObject.SetActive(true);
        //actionUI.SetSkillAction();
        actionUI.SetAction();
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
