using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPlayerDieCondition : LoseCondition
{
    public override void EndAction()
    {
        BattleManager.Instance.AllDyingPiecesDie();
        StartCoroutine(DoEndAction());
    }

    public override bool EndConditon()
    {
        foreach (var player in PieceQueueManager.Instance.PlayerList)
        {
            if (player.pieceStatus.CurrentHealth > 0)
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
