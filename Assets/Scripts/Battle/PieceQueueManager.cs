using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceQueueManager:Singleton<PieceQueueManager>
{
    /// <summary>
    /// 全部棋子的list
    /// </summary>
    private List<Piece> pieceList;

    private List<Player> playerList;
    private List<Enemy> enemyList;

    /// <summary>
    /// 行动顺序的棋子list
    /// </summary>
    private List<Piece> pieceActionList;

    public List<Player> PlayerList { get => playerList;  }
    public List<Piece> PieceList { get => pieceList; }
    public List<Enemy> EnemyList { get => enemyList;  }
    public List<Piece> PieceActionList { get => pieceActionList;  }

    /// <summary>
    /// 每个棋子行动前应该等待的时间
    /// </summary>
    //private int waitTime = 100;

    public PieceQueueManager()
    {
        pieceList = new List<Piece>();
        pieceActionList = new List<Piece>();
        playerList = new List<Player>();
        enemyList = new List<Enemy>();
    }

    /*public void AddPlayer(Player player)
    {
        playerList.Add(player);
        pieceList.Add(player);
    }

    public void AddEnemy(Enemy enemy)
    {
        enemyList.Add(enemy);
        pieceList.Add(enemy);
    }*/

    

    /// <summary>
    /// 把棋子登录到list中
    /// </summary>
    /// <param name="piece">要登录的棋子</param>
    public void AddPiece(Piece piece)
    {
        if (!pieceList.Contains(piece))
        {
            pieceList.Add(piece);
            if (piece.CompareTag("Player"))
                playerList.Add(piece as  Player);
            else
                enemyList.Add(piece as Enemy);
        }
        else
        {
            Debug.LogError("重复的人物添加");
        }
    }

    /*public List<Piece> GetSimplePieceList()
    {
        return pieceList;
    }

    public List<Piece> GetPlayerList()
    {
        return playerList;
    }

    public List<Piece> GetEnemyList()
    {
        return enemyList;
    }*/

    public void RemoveDiePiece(Piece piece)
    {
        pieceList.Remove(piece);
        if (piece.CompareTag("Player"))
            playerList.Remove(piece as Player);
        else
            enemyList.Remove(piece as Enemy);
        SetPieceActionQueueByDie();
        EventCenter.Instance.EventTrigger<List<Piece>>("SetPiecesUI", pieceActionList);
    }

    public void RemoveSettingPiece(Piece piece)
    {
        BattleGameManager.Instance.battlePlayerList.Remove(piece as Player);
        piece.gameObject.SetActive(false);
        piece.currentCell.currentPiece = null;
        pieceList.Remove(piece);
        if (piece.CompareTag("Player"))
            playerList.Remove(piece as Player);
        else
            enemyList.Remove(piece as Enemy);
        SetPieceActionQueueBySetting();
        EventCenter.Instance.EventTrigger<List<Piece>>("SetPiecesUI", pieceActionList);
        EventCenter.Instance.EventTrigger("ShowCombatantNum");
    }

    public void AddSettingPiece(Piece piece)
    {
        /*pieceList.Add(piece);
        if (piece.CompareTag("Player"))
            playerList.Add(piece);
        else
            enemyList.Add(piece);*/
        BattleGameManager.Instance.battlePlayerList.Add(piece as Player)
;        foreach(var cell in BattleGameManager.Instance.playerStartCellList)
        {
           if(cell.currentPiece==null)
            {
                piece.SetBattlePos(cell);
                break;
            }
        }
        SetPieceActionQueueBySetting();
        EventCenter.Instance.EventTrigger<List<Piece>>("SetPiecesUI", pieceActionList);
        EventCenter.Instance.EventTrigger("ShowCombatantNum");
        //LevelManager.Instance.CurrentPiece = TheFirstPiece();
    }

    public void AddBattlingPiece(Piece piece)
    {
        
    }

    private void SetPieceActionQueueBySetting()
    {
        ResetRealPieceTime();
        ResetSupposePieceTime();
        SetPieceActionQueue();
    }


    /// <summary>
    /// 获得顺序的棋子list头，即当前行动的棋子
    /// </summary>
    /// <returns></returns>
    public Piece TheFirstPiece()
    {
        if (pieceList.Count <= 0)
            return null;
        return pieceActionList[0];
    }

    /// <summary>
    /// 重置每个棋子的等待时间计时器，应该用于游戏开始时的
    /// </summary>
    public  void ResetRealPieceTime()
    {
        if (pieceList.Count <= 0)
            return;
        foreach (var piece in pieceList)
        {
            piece.pieceStatus.realWaitTimer = 0;
        }
    }

    private void ResetSupposePieceTime()
    {
        if (pieceList.Count <= 0)
            return;
        foreach (var piece in pieceList)
        {
            piece.pieceStatus.supposeWaitTimer=piece.pieceStatus.realWaitTimer;
        }
    }

    /// <summary>
    /// 设置顺序队列中的棋子
    /// </summary>
    public void SetPieceActionQueue()
    {      
        pieceActionList.Clear();
        List<Piece> temp = new List<Piece>();
        while (pieceActionList.Count < 28)
        {
            if(pieceActionList.Count>0)
            {
                foreach (var piece in pieceList)
                {
                    piece.pieceStatus.supposeWaitTimer += piece.pieceStatus.Speed;
                    if (piece.pieceStatus.supposeWaitTimer >= piece.pieceStatus.waitTime)
                    {
                        temp.Add(piece);
                        if (pieceActionList.Count + temp.Count >= 28)
                            break;
                    }
                    
                }
                if(temp.Count>0)
                {
                    temp.Sort((p1, p2) =>
                    {
                        if (p1.pieceStatus.supposeWaitTimer >= p2.pieceStatus.supposeWaitTimer)
                            return -1;
                        else
                            return 1;
                    });
                    foreach(var piece in temp)
                    {
                        if (pieceActionList.Count < 28)
                            pieceActionList.Add(piece);
                        if (piece.pieceStatus.supposeWaitTimer >= 100)
                            piece.pieceStatus.supposeWaitTimer -= 100;
                        else
                            piece.pieceStatus.supposeWaitTimer = 0;
                    }
                    temp.Clear();
                }
            }
            else
            {
                foreach (var piece in pieceList)
                {
                    piece.pieceStatus.realWaitTimer += piece.pieceStatus.Speed;
                    if (piece.pieceStatus.realWaitTimer >= piece.pieceStatus.waitTime)
                    {
                        temp.Add(piece);
                    }                   
                }
                if (temp.Count > 0)
                {
                    temp.Sort((p1, p2) =>
                    {
                        if (p1.pieceStatus.realWaitTimer >= p2.pieceStatus.realWaitTimer)
                            return -1;
                        else
                            return 1;
                    });
                    pieceActionList.Add(temp[0]);
                    temp[0].pieceStatus.realWaitTimer -= 100;
                    temp.Clear();
                    ResetSupposePieceTime();
                }             
            }
        }
    }

    public void SetPieceActionQueueByDie()
    {
        Piece firstPiece;
        if (pieceActionList[0] != null)
            firstPiece = pieceActionList[0];
        else
            firstPiece = pieceActionList[1];
        pieceActionList.Clear();
        List<Piece> temp = new List<Piece>();
        ResetSupposePieceTime();
        pieceActionList.Add(firstPiece);
        while (pieceActionList.Count < 28)
        {
            foreach (var piece in pieceList)
            {
                piece.pieceStatus.supposeWaitTimer += piece.pieceStatus.Speed;
                if (piece.pieceStatus.supposeWaitTimer >= piece.pieceStatus.waitTime)
                {
                    temp.Add(piece);
                    if (pieceActionList.Count + temp.Count >= 28)
                        break;
                }

            }
            if (temp.Count > 0)
            {
                temp.Sort((p1, p2) =>
                {
                    if (p1.pieceStatus.supposeWaitTimer >= p2.pieceStatus.supposeWaitTimer)
                        return -1;
                    else
                        return 1;
                });
                foreach (var piece in temp)
                {
                    if (pieceActionList.Count < 28)
                    {
                        pieceActionList.Add(piece);
                    }
                    if (piece.pieceStatus.supposeWaitTimer >= 100)
                        piece.pieceStatus.supposeWaitTimer -= 100;
                    else
                        piece.pieceStatus.supposeWaitTimer = 0;
                }
                temp.Clear();
            }         
        }
    }

   /* public void SortPieceActionQueueOrder(List<Piece> temp)
    {
        temp.Sort((p1,p2)=> 
        {
            if (p1.realWaitTimer >= p2.realWaitTimer)
                return -1;
            else
                return 1;
         });

    }*/


    public int GetPieceIndex(Piece piece)
    {
        for (int i = 0; i < pieceList.Count; i++)
        {
            if (piece == pieceList[i])
                return i;
        }
        return -1;
    }

    /// <summary>
    /// 获得9的倍数范围内的顺序队列的棋子，用于UI显示
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public List<Piece> GetPieceQueue()
    {
        return pieceActionList ;
    }

    public int GetPieceActionIndex(Piece _piece)
    {
        if (_piece == null)
            return -1;
        for (int i = 0; i < 28; i++)
        {
            if (_piece == pieceActionList[i])
            {
                return i;
            }              
        }
        return -1;
    }


    public void CheckPieceListQueue()
    {
        if (pieceActionList.Count <= 0)
            return;
        foreach(var piece in pieceActionList)
        {
            Debug.Log(piece.name);
        }
    }

    public Piece GetPieceByName(string pieceName)
    {
        foreach(var piece in pieceList)
        {
            if (piece.pieceStatus.pieceName == pieceName)
                return piece;
        }

        return null;
    }

    public T GetTheNearestPiece<T>(List<T> list,Piece piece) where T:Piece
    {
        float distance = 999;
        T theNearsetPiece=null;
        foreach(var target in list)
        {
            float tempDis = GetDistance(piece.currentCell,target.currentCell,piece.floorIndex, target.floorIndex);
            if(tempDis<distance)
            {
                theNearsetPiece = target;
                distance = tempDis;
            }
        }

        return theNearsetPiece;
    }



    public float GetDistance(Cell cell1,Cell cell2,int floorIndex1,int floorIndex2)
    {
        int rowDistance = cell1.rowIndex - cell2.rowIndex;
        int lineDistance = cell1.lineIndex - cell2.lineIndex;
        int floorDistance = floorIndex1 - floorIndex2;

        return Mathf.Pow(rowDistance, 2) + Mathf.Pow(lineDistance, 2) + Mathf.Pow(floorDistance, 2);
    }

    public void ClearPieceQueue()
    {
        pieceList.Clear();
        pieceActionList.Clear();
        playerList.Clear();
        enemyList.Clear();
    }
}
