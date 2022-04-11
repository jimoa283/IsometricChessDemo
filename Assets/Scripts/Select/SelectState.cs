using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SelectState 
{
    [SerializeField]
    private SelectStateID selectStateID;

    public SelectStateID SelectStateID { get => selectStateID; set => selectStateID = value; }

    protected PieceActionSelectUI pieceActionSelectUI;

    public SelectState(SelectStateID selectStateID,PieceActionSelectUI pieceActionSelectUI)
    {
        SelectStateID = selectStateID;
        this.pieceActionSelectUI = pieceActionSelectUI;
    }


    public virtual void OnEnter()
    {

    }

    public virtual void ActionOnJ()
    {
        
    }

    public virtual void ActionOnK()
    {
        
    }

    public virtual void ActionOnL()
    {
        
    }

    public virtual void ActionOnI()
    {

    }

    public virtual void SetPos(Cell cell)
    {
        pieceActionSelectUI.SetActiveSelectAction?.Invoke(cell);
    }


}
