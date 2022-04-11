using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBUFFList : MonoBehaviour
{
    private Piece piece;

    private List<BUFF> buffList=new List<BUFF>();
    private List<BUFFEffctObj> buffObjList=new List<BUFFEffctObj>();
    private int buffObjIndex;

    public List<BUFF> BuffList { get => buffList;}

    public void Init(Piece piece)
    {
        this.piece = piece;
        //buffList = new List<BUFF>();
        //buffObjList = new List<BUFFEffctObj>();
    }

    private bool CheckBuff(BUFF _buff)
    {
        foreach(var buff in buffList)
        {
            if (buff.Name == _buff.Name)
                return false;
        }
        return true;
    }

    public void AddBUFFEffectObj(string objName)
    {
        BUFFEffctObj obj = PoolManager.Instance.GetObj(objName).GetComponent<BUFFEffctObj>();
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        StopBuffObjShow();
        buffObjList.Add(obj);
        buffObjIndex = buffObjList.Count-2;
        obj.Init(GoBuffObjShow);
    }

    public void StopBuffObjShow()
    {
        if(buffObjList.Count>0)
        {
            buffObjList[buffObjIndex].StopBUFFEffectObjShow();
        }       
    }

    public void GoBuffObjShow()
    {
        buffObjIndex = ++buffObjIndex % buffObjList.Count;
        buffObjList[buffObjIndex].StartBUFFEffectObjShow();
    }

    public void AddBUFF(BUFF buff)
    {
        if (!CheckBuff(buff))
            return;
        piece.pieceStatus.buffPower += buff.Power;
        piece.pieceStatus.buffHitRate += buff.HitRate;
        piece.pieceStatus.buffDefense += buff.Defense;
        piece.pieceStatus.buffSpeed += buff.Speed;
        piece.pieceStatus.buffMagic += buff.Magic;
        piece.pieceStatus.buffAvoid += buff.Avoid;
        piece.pieceStatus.buffMagicDefense += buff.MagicDefense;
        piece.pieceStatus.buffVMove += buff.VMove;
        piece.pieceStatus.buffLucky += buff.Lucky;
        piece.pieceStatus.buffHMove += buff.HMove;
        piece.pieceStatus.buffFireResistance += buff.FireResistance;
        piece.pieceStatus.buffIceResistance += buff.IceResistance;
        piece.pieceStatus.buffWindResistance += buff.WindResistance;
        piece.pieceStatus.buffThunderResistance += buff.ThunderResistance;
        piece.pieceStatus.buffLightResistance += buff.LightResistance;
        piece.pieceStatus.buffDarkResistance += buff.DarkResistance;
        if(buff.BUFFEffectObjName!="Null")
          AddBUFFEffectObj(buff.BUFFEffectObjName);
        piece.PieceTimeProcessor.AddTimeProcessorAction(buff.TriggerTimeType, buff.BUFFEffect);
        buffList.Add(buff);
        if (buff.HMove != 0 && piece.CompareTag("Enemy"))
        {
            (piece as Enemy).GetRiskCellList();
        }
    }

    public void RemoveBUFF(BUFF buff)
    {
        piece.pieceStatus.buffPower -= buff.Power;
        piece.pieceStatus.buffHitRate -= buff.HitRate;
        piece.pieceStatus.buffDefense -= buff.Defense;
        piece.pieceStatus.buffSpeed -= buff.Speed;
        piece.pieceStatus.buffMagic -= buff.Magic;
        piece.pieceStatus.buffAvoid -= buff.Avoid;
        piece.pieceStatus.buffMagicDefense -= buff.MagicDefense;
        piece.pieceStatus.buffVMove -= buff.VMove;
        piece.pieceStatus.buffLucky -= buff.Lucky;
        piece.pieceStatus.buffHMove -= buff.HMove;
        piece.pieceStatus.buffFireResistance -= buff.FireResistance;
        piece.pieceStatus.buffIceResistance -= buff.IceResistance;
        piece.pieceStatus.buffWindResistance -= buff.WindResistance;
        piece.pieceStatus.buffThunderResistance -= buff.ThunderResistance;
        piece.pieceStatus.buffLightResistance -= buff.LightResistance;
        piece.pieceStatus.buffDarkResistance -= buff.DarkResistance;
        if (buff.BUFFEffectObjName != "Null")
            RemoveBUFFEffectObj(buff.BUFFEffectObjName);
        piece.PieceTimeProcessor.RemoveTimeProcessorAction(buff.TriggerTimeType, buff.BUFFEffect);
        buffList.Remove(buff);
        if(buff.HMove!=0&&piece.CompareTag("Enemy"))
        {
            (piece as Enemy).GetRiskCellList();
        }
    }

    public void RemoveBUFFEffectObj(string objName)
    {
        foreach (var obj in buffObjList)
        {
            if (obj.name.StartsWith(objName))
            {
               /* if (buffObjList.Count == 1)
                    StopBuffObjShow();*/
                PoolManager.Instance.PushObj(objName, obj.gameObject);                
                buffObjList.Remove(obj);
                return;
            }
        }
    }

    public void BUFFCountDown()
    {
        for (int i = 0; i < buffList.Count;)
        {
            buffList[i].AliveTime--;
            if (buffList[i].AliveTime == 0)
                RemoveBUFF(buffList[i]);
            else
                i++;
        }
    }
}
