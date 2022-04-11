using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EffectRangeManager :Singleton<EffectRangeManager>
{
    private List<Cell> effectRanges;
    private List<Piece> targetPieces;
    private BaseActionData baseActionData;

    private UnityAction<Cell,int> createAction;

    public BaseActionData BaseActionData { get => baseActionData; }

    public EffectRangeManager()
    {
        effectRanges = new List<Cell>();
        targetPieces = new List<Piece>();
    }

    public void ClearEffectRange()
    {
        ClearEffectRangeDisplay();
        effectRanges.Clear();
        targetPieces.Clear();
    }

    public void ClearEffectRangeDisplay()
    {
        foreach (var cell in effectRanges)
        {
            cell.effectRange.Hide();
            cell.effectRange = null;            
        }
    }

    public void ResigterAction()
    {
        BattleManager.Instance.AddBattleObj(LevelManager.Instance.CurrentPiece, effectRanges, targetPieces, BaseActionData,
            LevelManager.Instance.CurrentPiece.pieceStatus.lookDirection,false);
    }

    public List<Cell> GetEffectRange()
    {
        return effectRanges;
    }

    public List<Piece> GetEffectTargets()
    {
        return targetPieces;
    }

   public void ResigterBaseActionDate(BaseActionData baseActionData)
    {
        this.baseActionData = baseActionData;
        LevelManager.Instance.CurrentPiece.PieceTimeProcessor.SelectActionTimeProcessorCheck();
    }

    public bool CheckRightPos()
    {
        if(BaseActionData.CheckTargetFunc!=CheckTargetFunc.SetBlockTarget)
        {
            if (targetPieces.Count > 0)
                return true;
            else
                return false;
        }
       else
        {
            if (effectRanges.Count > 0)
                return true;
            else
                return false;
        }
    }

    public void CreateEffectRange(Cell center,Piece piece)
    {
        BattleManager.Instance.CancelExtraAttackPiece();
        ClearEffectRange();
        if (!center.canAttack)
            return;
        List<Vector2> temp;

        if (piece.pieceStatus.lookDirection == LookDirection.Up)
            temp = BaseActionData.RangePosUp;
        else if (piece.pieceStatus.lookDirection == LookDirection.Down)
            temp = BaseActionData.RangePosDown;
        else if (piece.pieceStatus.lookDirection == LookDirection.Left)
            temp = BaseActionData.RangePosLeft;
        else
            temp = BaseActionData.RangePosRight;

       foreach(var pos in temp)
        {
            Cell cell = CellManager.Instance.GetEffectCell((int)(center.rowIndex + pos.x), (int)(center.lineIndex + pos.y), BaseActionData.VMinRange, BaseActionData.VMaxRange,piece);
            if(cell!=null&&BaseActionData.CheckTargetFunc(cell,piece))
            {
                cell.effectRange = PoolManager.Instance.GetObj("effectRange").GetComponent<ShowRange>();

                cell.effectRange.SimpleShow(cell);
                effectRanges.Add(cell);

                if(BaseActionData.CheckTargetFunc!=CheckTargetFunc.SetBlockTarget)
                {
                    if (cell.currentPiece != null)
                        targetPieces.Add(cell.currentPiece);
                }
            }
        }

        BaseActionData.SetBattlePieceFunc?.Invoke(piece,targetPieces,BaseActionData);
    }

}
