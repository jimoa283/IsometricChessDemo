using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PieceActionQueue 
{
    private Queue<UnityAction> pieceActionQueue=new Queue<UnityAction>();

    public PieceActionQueue()
    {
       // pieceActionQueue = new Queue<UnityAction>();
    }
    
    public void AddPieceAction(UnityAction action)
    {
        pieceActionQueue.Enqueue(action);
    }

    public void RemovePieceAction()
    {
        if(pieceActionQueue.Count>0)
           pieceActionQueue.Dequeue();
    }

    public void ClearPieceAction()
    {
        pieceActionQueue.Clear();
    }

    public void InsertPieceAction(UnityAction action)
    {
        Queue<UnityAction> temp = new Queue<UnityAction>();
        temp.Enqueue(action);
        while(pieceActionQueue.Count>0)
        {
            temp.Enqueue(pieceActionQueue.Dequeue());
        }

        pieceActionQueue = temp;
    }

    public void DoPieceAction()
    {
        if(pieceActionQueue.Count>0)
        {
            pieceActionQueue.Dequeue()?.Invoke();
        }       
        else
            LevelManager.Instance.ChangeLevelState(LevelStateID.SetPieceDirection);
    }
}
