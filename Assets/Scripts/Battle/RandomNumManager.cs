using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNumManager :Singleton<RandomNumManager>
{
    private Queue<int> BattleRandomNumList=new Queue<int>();
    private Queue<List<int>> LevelUpRandomNumList = new Queue<List<int>>();

    public void CreateNewRandomList()
    {
        BattleRandomNumList.Clear();
        LevelUpRandomNumList.Clear();

        for (int i = 0; i < 999; i++)
        {
            int temp = Random.Range(0, 101);
            BattleRandomNumList.Enqueue(temp);
        }

        for (int i = 0; i < 99; i++)
        {
            List<int> temp = new List<int>();
            for (int j = 0; j < 6; j++)
            {
                int temp2 = Random.Range(0, 101);
                temp.Add(temp2);
            }
        }
    }

    public int GetBattleRandomNum()
    {
        return BattleRandomNumList.Dequeue();
    }

    public List<int> GetLevelUpRandomNum()
    {
        return LevelUpRandomNumList.Dequeue();
    }
}
