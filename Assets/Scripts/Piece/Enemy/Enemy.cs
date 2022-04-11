using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Piece
{
    private EnemyFSM enemyFSM;

    public string pocketName;
    public int pocketID;
    private HostilityLine hostilityLine;
    public List<Cell> riskCellList = new List<Cell>();
    public List<Piece> threatPieceList = new List<Piece>();

    public EnemyFSM EnemyFSM { get => enemyFSM; }

    public override void PieceMove(Cell cell)
    {
        StartCoroutine(DoPieceMove(cell, enemyFSM.CurrentState.Change));
    }

    public override void SetBattlePos(Cell cell)
    {
        base.SetBattlePos(cell);
        enemyFSM = GetComponent<EnemyFSM>();
        enemyFSM.Init(this);      
    }

    public override void PieceDie()
    {
        if(pocketName!="")
        {
            Pocket pocket = PoolManager.Instance.GetObj(pocketName).GetComponent<Pocket>();
            pocket.SetPocket(currentCell,pocketID);
        }
        foreach (var cell in riskCellList)
        {
            cell.RemoveRiskEnemy(this);
        }
        base.PieceDie();
    }

    public void GetRiskCellList()
    {
        foreach (var cell in riskCellList)
        {
            cell.RemoveRiskEnemy(this);
        }

        riskCellList.Clear();

        riskCellList = RangeManager.Instance.FindRiskCellForEnemy(this);
    }

    public void SetHostilityLine(Vector3 pos)
    {
        hostilityLine.SetLine(transform.position, pos);
    }

    public void ClearHostilityLine()
    {
        hostilityLine.ClearLine();
    }

    public override void Init()
    {
        base.Init();
        hostilityLine = GetComponentInChildren<HostilityLine>();
        hostilityLine.Init();
    }

    public override void SetPos(Cell cell)
    {
        base.SetPos(cell);
        GetRiskCellList();
    }
}
