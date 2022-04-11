using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class MoveTarget : IImpactEffect
{
    public void DoEffect(BattleObj battleObj)
    {
        List<Piece> others = new List<Piece>();
        int maxNum = 0;
        foreach (var piece in battleObj.targetPieces)
        {
            Cell target;
            int tempNum = 0;
            Piece hit = CellManager.Instance.GetMoveCell(piece.currentCell, piece, battleObj.baseActionData.MaxMoveNum, 
                battleObj.owner.floorIndex - 1, out target,out tempNum,battleObj.owner.pieceStatus.lookDirection);

            if (tempNum > maxNum)
                maxNum = tempNum;

            if (hit != null && CampCheckFunc.CampAgainstCheck(battleObj.owner, hit))
            {
                //CameraController.Instance.AddBattlePiece(hit);
                others.Add(piece);
                others.Add(hit);
            }

            Vector3 pos = target.transform.position + Vector3.up * (piece.pieceStatus.isFly + 1) * 0.6f;

            piece.transform.DOMove(pos, 2f);
            piece.currentCell.currentPiece = null;
            piece.SetPos(target);
            /*piece.currentCell = target;
            piece.currentCell.currentPiece = piece;*/

        }

        if(others.Count>0)
        {
            battleObj.targetPieces = others;
            battleObj.baseActionData.Effects.Add(ImpactEffectManager.GetEffect(EffectType.Damage));
        }
        
        //float time = 0.5f * maxNum;

        battleObj.owner.StartCoroutine(DoChangeEffect(battleObj));
        //battleObj.baseActionData.SecondWaitTime = 0.5f * maxNum+1;
    }

    IEnumerator DoChangeEffect(BattleObj battleObj)
    {
        yield return new WaitForSeconds(2);
        battleObj.CheckEffect();
    }
}
