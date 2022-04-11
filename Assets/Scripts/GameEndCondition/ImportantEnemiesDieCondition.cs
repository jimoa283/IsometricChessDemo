using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantEnemiesDieCondition : VictoryConditon
{
    public string[] importantEnemyNames;

    public override void EndAction()
    {
        BattleManager.Instance.AllDyingPiecesDie();
        StartCoroutine(DoEndAction());
    }

    public override bool EndConditon()
    {
        foreach(var enemyName in importantEnemyNames)
        {
            bool hasFind = false;
            foreach(var enemy in BattleManager.Instance.dyingPieceList)
            {
                if (enemy.pieceStatus.pieceName == enemyName)
                {
                    hasFind = true;
                    break;
                }
            }

            if (!hasFind)
            {
                return false;
            }
                
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
