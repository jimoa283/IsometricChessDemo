using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionData 
{
    public Piece owner;
    public List<Cell> targetCells;
    public List<Piece> targetPieces;
    public LookDirection lookDirection;
    public BaseActionData baseActionData;

    public ActionData(BaseActionData baseActionData,Piece owner,List<Cell> targetCells,List<Piece> targetPieces)
    {
        //power = useItem.Power;
        this.owner = owner;
        this.targetCells = new List<Cell>(targetCells);
        this.targetPieces = new List<Piece>(targetPieces);
        lookDirection = owner.pieceStatus.lookDirection;
        this.baseActionData = baseActionData;
    }

   /* public ActionData(Skill skill,Piece owner,List<Cell> targetCells,List<Piece> targetPieces)
    {
        //power = skill.Power;
        this.owner = owner;
        this.targetCells = targetCells;
        this.targetPieces = targetPieces;
        //launchType = skill.LaunchType;
        //ImpactEffects = skill.Effects;
        //animName = skill.AnimName;
        //vfxName = skill.VFXObjName;
        lookDirection = owner.lookDirection;
        this.baseActionDate = baseActionData;
    }*/
}
