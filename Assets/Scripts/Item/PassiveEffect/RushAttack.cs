using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushAttack : IPassiveEffect
{
    public override void DoEffect(Piece piece)
    {
        if (piece.moveStepCount >= 9)
        {
            ActiveSkill temp3 = SkillManager.Instance.GetActiveSkill(3100);
            List<Piece> pieces = temp3.BaseActionData.GetTargetPiecesFunc(temp3.BaseActionData, piece, piece.currentCell, temp3.BaseActionData.CheckTargetFunc);

            if (pieces.Count > 0)
            {
                int rangeNum = Random.Range(0, pieces.Count);
                List<Piece> temp1 = new List<Piece>();
                List<Cell> temp2 = new List<Cell>();
                temp1.Add(pieces[rangeNum]);
                temp2.Add(pieces[rangeNum].currentCell);
                BattleManager.Instance.InsertBattleObj(piece, temp2, temp1, temp3.BaseActionData, true);
            }
        }
    }
}
