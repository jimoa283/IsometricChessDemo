using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class BattleManager : Singleton<BattleManager>
{
    


    private List<BattleObj> battleObjQueue;
    private BattleObj currentBattleObj;
    private bool isBattling;
    
    public event UnityAction expUpEvents;
    //public event UnityAction pieceDieEvents;
    public int extraAttackCount = 1;
    public int countAttackCount = 1;

    public Piece extraAttackPiece;
    public Piece beExtraAttackPiece;
    public Cell beExtraAttackPieceCell;
    public List<Piece> extraBattlePieceList;
    public List<Piece> dyingPieceList;
    public List<Piece> levelUpPieceList;
    public Queue<Piece> getSkillPieceList;

    private List<Func<UnityAction,bool>> beforeBattleSpEvent;
    private List<Func<UnityAction,bool>> afterBattleSpEvent;

    public BattleObj CurrentBattleObj { get => currentBattleObj; set => currentBattleObj = value; }
    public bool IsBattling { get => isBattling; }
    public List<Func<UnityAction,bool>> BeforeBattleSpEvent { get => beforeBattleSpEvent;}
    public List<Func<UnityAction,bool>> AfterBattleSpEvent { get => afterBattleSpEvent;}

    public Func<bool> battleGameVictoryCondition;
    public Func<bool> battleGameLoseCondition;

    public BattleManager()
    {
        battleObjQueue = new List<BattleObj>();
        extraBattlePieceList = new List<Piece>();
        dyingPieceList = new List<Piece>();
        levelUpPieceList = new List<Piece>();
        getSkillPieceList = new Queue<Piece>();
        beforeBattleSpEvent = new List<Func<UnityAction,bool>>();
        afterBattleSpEvent = new List<Func<UnityAction,bool>>();
        ResetBattleManager();
    }

    public void ResetBattleManager()
    {
        expUpEvents = null;
        dyingPieceList.Clear();
        getSkillPieceList.Clear();
        levelUpPieceList.Clear();
        
        extraAttackCount = 1;
        countAttackCount = 1;
    }

    public void BattleClearOnSceneChange()
    {
        beforeBattleSpEvent.Clear();
        afterBattleSpEvent.Clear();
        battleGameVictoryCondition = null;
        battleGameLoseCondition = null;
    }

    public int CheckTargetInRange(BattleObj battleObj)
    {
        List<Piece> temp;
        temp = battleObj.baseActionData.GetTargetPiecesFunc(battleObj.baseActionData, battleObj.owner, battleObj.owner.currentCell, battleObj.baseActionData.CheckTargetFunc);                                                

        for (int i = 0; i < battleObj.targetPieces.Count;)
        {
            if(!temp.Contains(battleObj.targetPieces[i]))
            {
                battleObj.targetPieces.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
        return battleObj.targetPieces.Count;
    }

    public void AddBattleObj(Piece starter, List<Cell> targetCells, List<Piece> targetPieces, BaseActionData baseActionData,LookDirection lookDirection,bool isPassiveSkill)
    {
        BattleObj battleObj = new BattleObj(starter, targetCells, targetPieces, baseActionData,lookDirection,isPassiveSkill);
        battleObjQueue.Add(battleObj);
    }

    public void ShowExtraAttackPiece(Piece piece,Piece target,Cell cell)
    {
        extraAttackPiece = piece;
        beExtraAttackPiece = target;
        beExtraAttackPieceCell = cell;
        AddExtraBattlePiece(piece);
        piece.ShowExtraAttackTip();
    }

    public void AddExtraBattlePiece(Piece piece)
    {
        if(!extraBattlePieceList.Contains(piece))
            extraBattlePieceList.Add(piece);
    }

    public void AddDiePiece(Piece piece)
    {
        if (!dyingPieceList.Contains(piece))
            dyingPieceList.Add(piece);
    }

    public void CancelExtraAttackPiece()
    {
        extraAttackPiece?.CloseExtraAttackTip();
        extraBattlePieceList.Clear();
        
        extraAttackPiece = null;
        beExtraAttackPiece = null;
    }

    public void CancelExtraAttackPieceShow()
    {
        extraAttackPiece?.CloseExtraAttackTip();
    }

    public void SetExtraAttack()
    {     
        if(extraAttackPiece!=null&&beExtraAttackPiece!=null)
        {
            LookDirection lookDirection = SetBattlPieceFunc.AdjustPieceDirection(extraAttackPiece.currentCell, beExtraAttackPieceCell);

            List<Cell> tempCell = new List<Cell>();
            List<Piece> tempPiece = new List<Piece>();
            tempCell.Add(beExtraAttackPieceCell);
            tempPiece.Add(beExtraAttackPiece);

            //CameraController.Instance.AddBattlePiece(extraAttackPiece);

            AddBattleObj(extraAttackPiece, tempCell, tempPiece, extraAttackPiece.pieceStatus.Weapon.ActiveSkill.BaseActionData,lookDirection,false);

            extraAttackCount--;

            CancelExtraAttackPiece();
        }          
    }

    public void InsertBattleObj(Piece starter, List<Cell> targetCells, List<Piece> targetPieces, BaseActionData baseActionData,bool isPassiveSkill)
    {
        BattleObj battleObj = new BattleObj(starter, targetCells, targetPieces, baseActionData,starter.pieceStatus.lookDirection,isPassiveSkill);
        battleObj.exitAction = battleObj.ExpUpAction;
        
        battleObjQueue.Insert(0, battleObj);
    }

    private void CheckSpEvent(List<Func<UnityAction,bool>> list,UnityAction action)
    {
        foreach(var spEvent in list)
        {
            if(spEvent(action))
            {
                list.Remove(spEvent);
                return;
            }
        }
        action?.Invoke();
    }

    public void StartBattle()
    {
        if(battleObjQueue.Count>0)
        {
            CheckSpEvent(beforeBattleSpEvent, () =>
             {
                 if (!isBattling)
                 {
                     LevelManager.Instance.CurrentPiece.PieceActionCountDown();
                     CameraController.Instance.AddRangeBattlePieces(extraBattlePieceList);
                     CameraController.Instance.AddRangeBattlePieces(battleObjQueue[0].targetPieces);
                     CameraController.Instance.AddBattlePiece(battleObjQueue[0].owner);
                    
                     isBattling = true;
                 }
                 CameraController.Instance.SpecialBattleShot(()=>
                 {
                     UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.BattleUI));
                     CheckBattleChange();
                 });
             });
        }        
    }


    public void CheckBattleChange()
    {
        if(battleObjQueue.Count>0)
        {
            currentBattleObj = battleObjQueue[0];
            battleObjQueue.RemoveAt(0);
            currentBattleObj.DoAction();
        }
        else
        {
            //isBattling = false;          
            UIManager.Instance.PopPanel(UIPanelType.BattleUI);
            LevelUpCheck();
        }
    }

    public void LevelUpCheck()
    {
        expUpEvents?.Invoke();
        expUpEvents = null;

        if(levelUpPieceList.Count>0)
        {
            BattleGameManager.Instance.StartCoroutine(DoPieceLevelUp());
        }
        else
        {
            CameraController.Instance.ExitSpecialBattleShot(CheckAfterBattleSpEvent);
        }
    }

    public void GetSkillCheck()
    {
        if(getSkillPieceList.Count>0)
        {
            Piece temp = getSkillPieceList.Dequeue();
            string pieceName = temp.pieceStatus.pieceName;
            string skillName = temp.pieceStatus.skillList[temp.pieceStatus.skillList.Count - 1].Name;
            EventCenter.Instance.EventTrigger("ShowPieceGetSkill", pieceName, skillName);
        }
        else
        {
            CameraController.Instance.ExitSpecialBattleShot(CheckAfterBattleSpEvent);
        }
    }

    private void CheckAfterBattleSpEvent()
    {
        CheckSpEvent(afterBattleSpEvent, CheckBattleGameEnd);
    }

    private void CheckBattleGameEnd()
    {
        if(battleGameVictoryCondition!=null&&battleGameVictoryCondition.Invoke())
        {
            BattleGameManager.Instance.BeforeBattleVictoryAction?.Invoke();
            return;
        }

        if(battleGameLoseCondition!=null&&battleGameLoseCondition.Invoke())
        {
            BattleGameManager.Instance.BeforeBattleLoseAction?.Invoke();
            return;
        }
        PieceActionFinishCheck();
    }

    public void AllDyingPiecesDie()
    {
        foreach (var piece in dyingPieceList)
        {
            piece.PieceDie();
        }
    }

    private void PieceActionFinishCheck()
    {
        AllDyingPiecesDie();
        foreach(var enemy in PieceQueueManager.Instance.EnemyList)
        {
            enemy.GetRiskCellList();
        }
        UnityAction exitAction;
        if (LevelManager.Instance.CurrentPiece.CompareTag("Player"))
            exitAction = ExitBattleForPlayer;
        else
            exitAction = ExitBattleForEnemy;
        if (LevelManager.Instance.CurrentPiece.pieceStatus.CurrentHealth>0&&LevelManager.Instance.CurrentPiece!=null)
            LevelManager.Instance.CurrentPiece.PieceTimeProcessor.AfterActionTimePricessorCheck(exitAction);
        else
            LevelManager.Instance.ChangeLevelState(LevelStateID.TurnEnd);
    }




    public void ExitBattleForPlayer()
    {
        Piece piece = LevelManager.Instance.CurrentPiece;
        isBattling = false;
        if (piece.hasMove)
        {
            if (piece.actionCount == 0)
            {
                LevelManager.Instance.ChangeLevelState(LevelStateID.SetPieceDirection);
            }
            else
            {
                if (!piece.hasMove)
                {
                    Select.Instance.ChangeSelectState(SelectStateID.ChooseActionState);
                }
                else
                {
                    Select.Instance.ChangeSelectState(SelectStateID.ChooseActionState);
                    Select.Instance.currentSelectState.ActionOnJ();
                }
            }
        }
        else
            Select.Instance.ChangeSelectState(SelectStateID.ChooseActionState);
    }
    
    public void ExitBattleForEnemy()
    {
        Enemy enemy = LevelManager.Instance.CurrentPiece as Enemy;
        isBattling = false;
        //if(enemy.hasMove)
        //{
            if(enemy.actionCount==0)
            {
                enemy.EnemyFSM.PerformTransition(EnemyStateID.Idle);
                LevelManager.Instance.ChangeLevelState(LevelStateID.TurnEnd);
            }
        //}
    }

    IEnumerator DoPieceLevelUp()
    {
        yield return new WaitForSeconds(0.85f);
        foreach(var piece in levelUpPieceList)
        {
            LevelUpTip temp = PoolManager.Instance.GetObj("LevelUpTip").GetComponent<LevelUpTip>();
            if (getSkillPieceList.Contains(piece))
                temp.ShowLevelUpTip(piece.transform.position, true);
            else
                temp.ShowLevelUpTip(piece.transform.position, false);
        }

        yield return new WaitForSeconds(0.7f);
        GetSkillCheck();
    }
}
