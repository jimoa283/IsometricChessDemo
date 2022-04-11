using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEnemyDieCondition : VictoryConditon
{
    public override void EndAction()
    {
        BattleManager.Instance.AllDyingPiecesDie();
        StartCoroutine(DoEndAction());
    }

    public override bool EndConditon()
    {
        foreach(var enemy in PieceQueueManager.Instance.EnemyList)
        {
            if (enemy.pieceStatus.CurrentHealth > 0)
                return false;
        }

        return true;
    }

    public override void Init()
    {
        BattleManager.Instance.battleGameVictoryCondition = EndConditon;
        BattleGameManager.Instance.BeforeBattleVictoryAction = EndAction;
    }

    IEnumerator DoEndAction()
    {
        yield return new WaitForSeconds(0.5f);
        BattleGameManager.Instance.BattleVictory();
    }
}
