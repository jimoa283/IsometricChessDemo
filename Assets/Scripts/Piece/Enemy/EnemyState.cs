using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState 
{
    protected EnemyStateID enemyStateID;

    public EnemyStateID EnemyStateID { get => enemyStateID;}

    protected EnemyFSM enemyFSM;

    protected EnemyState(EnemyStateID enemyStateID, EnemyFSM enemyFSM)
    {
        this.enemyStateID = enemyStateID;
        this.enemyFSM = enemyFSM;
    }

    public abstract void Enter();

    public abstract void Act();

    public abstract void Change();
}
