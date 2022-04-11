using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportanePlayersDieCondition : LoseCondition
{
    public string[] importantPlayerNames;

    public override void EndAction()
    {
        BattleManager.Instance.AllDyingPiecesDie();
        StartCoroutine(DoEndAction());
    }

    public override bool EndConditon()
    {
        foreach (var playerName in importantPlayerNames)
        {
            bool hasFind = false;
            foreach (var player in BattleManager.Instance.dyingPieceList)
            {
                if (player.pieceStatus.pieceName == playerName)
                {
                    hasFind = true;
                    break;
                }
            }

            if (!hasFind)
                return false;
        }

        return true;
    }

    public override void Init()
    {
        BattleManager.Instance.battleGameLoseCondition = EndConditon;
        BattleGameManager.Instance.BeforeBattleLoseAction = EndAction;
    }

    IEnumerator DoEndAction()
    {
        yield return new WaitForSeconds(0.5f);
        BattleGameManager.Instance.BattleLose();
    }
}
