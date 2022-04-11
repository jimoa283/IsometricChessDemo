using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PieceStatus : MonoBehaviour
{
    protected Piece piece;

    [SerializeField]private int level;

    public int basePower;
    public int baseHitRate;
    public int baseDefense;
    public int baseSpeed;
    public int baseMagic;
    public int baseAvoid;
    public int baseMagicDefense;
    public int baseVMove;
    public int baseLucky;
    public int baseHMove;   //平行移动力
    public int baseFireResistance;
    public int baseIceResistance;
    public int baseWindResistance;
    public int baseThunderResistance;
    public int baseLightResistance;
    public int baseDarkResistance;

    public int equipPower;
    public int equipHitRate;
    public int equipDefense;
    public int equipSpeed;
    public int equipMagic;
    public int equipAvoid;
    public int equipMagicDefense;
    public int equipVMove;
    public int equipLucky;
    public int equipHMove;   //平行移动力
    public int equipFireResistance;
    public int equipIceResistance;
    public int equipWindResistance;
    public int equipThunderResistance;
    public int equipLightResistance;
    public int equipDarkResistance;

    public int buffPower;
    public int buffHitRate;
    public int buffDefense;
    public int buffSpeed;
    public int buffMagic;
    public int buffAvoid;
    public int buffMagicDefense;
    public int buffVMove;
    public int buffLucky;
    public int buffHMove;
    public int buffFireResistance;
    public int buffIceResistance;
    public int buffWindResistance;
    public int buffThunderResistance;
    public int buffLightResistance;
    public int buffDarkResistance;

    public int critical;

    [SerializeField]private bool canMove = true;

    public int BuffPower { get 
        {
            if (BattleGameManager.Instance.isInBattleScene&&piece.currentCell.topCellAttachment != null)
                return buffPower + piece.currentCell.topCellAttachment.attachmentPower;
            else
                return buffPower;
        } }
    public int BuffHitRate
    {
        get
        {
            if (BattleGameManager.Instance.isInBattleScene && piece.currentCell.topCellAttachment != null)
                return buffHitRate + piece.currentCell.topCellAttachment.attachmentHitRate;
            else
                return buffHitRate;
        }
    }
    public int BuffDefense
    {
        get
        {
            if (BattleGameManager.Instance.isInBattleScene && piece.currentCell.topCellAttachment != null)
                return buffDefense + piece.currentCell.topCellAttachment.attachmentDefense;
            else
                return buffDefense;
        }
    }
    public int BuffSpeed
    {
        get
        {
            if (BattleGameManager.Instance.isInBattleScene && piece.currentCell.topCellAttachment != null)
                return buffSpeed + piece.currentCell.topCellAttachment.attachmentSpeed;
            else
                return buffSpeed;
        }
    }
    public int BuffMagic
    {
        get
        {
            if (BattleGameManager.Instance.isInBattleScene && piece.currentCell.topCellAttachment != null)
                return buffMagic + piece.currentCell.topCellAttachment.attachmentMagic;
            else
                return buffMagic;
        }
    }
    public int BuffAvoid
    {
        get
        {
            if (BattleGameManager.Instance.isInBattleScene && piece.currentCell.topCellAttachment != null)
                return buffAvoid + piece.currentCell.topCellAttachment.attachmentAvoid;
            else
                return buffAvoid;
        }
    }
    public int BuffMagicDefense
    {
        get
        {
            if (BattleGameManager.Instance.isInBattleScene && piece.currentCell.topCellAttachment != null)
                return buffMagicDefense + piece.currentCell.topCellAttachment.attachmentMagicDefense;
            else
                return buffMagicDefense;
        }
    }
    public int BuffVMove
    {
        get
        {
            if (BattleGameManager.Instance.isInBattleScene && piece.currentCell.topCellAttachment != null)
                return buffVMove + piece.currentCell.topCellAttachment.attachmentVMove;
            else
                return buffVMove;
        }
    }
    public int BuffLucky
    {
        get
        {
            if (BattleGameManager.Instance.isInBattleScene && piece.currentCell.topCellAttachment != null)
                return buffLucky + piece.currentCell.topCellAttachment.attachmentLucky;
            else
                return buffLucky;
        }
    }
    public int BuffHMove
    {
        get
        {
            if (BattleGameManager.Instance.isInBattleScene && piece.currentCell.topCellAttachment != null)
                return buffHMove + piece.currentCell.topCellAttachment.attachmentHMove;
            else
                return buffHMove;
        }
    }

    public int BuffFireResistance
    {
        get
        { 
            if (BattleGameManager.Instance.isInBattleScene && piece.currentCell.topCellAttachment != null)
                return buffFireResistance + piece.currentCell.topCellAttachment.attachmentFireResistance;
            else
                return buffFireResistance;
        }
    }

    public int BuffIceResistance
    {
        get
        {
            if (BattleGameManager.Instance.isInBattleScene && piece.currentCell.topCellAttachment != null)
                return buffIceResistance + piece.currentCell.topCellAttachment.attachmentIceResistance;
            else
                return buffIceResistance;
        }
    }

    public int BuffWindResistance
    {
        get
        {
            if (BattleGameManager.Instance.isInBattleScene && piece.currentCell.topCellAttachment != null)
                return buffWindResistance + piece.currentCell.topCellAttachment.attachmentWindResistance;
            else
                return buffWindResistance;
        }
    }

    public int BuffThunderResistance
    {
        get
        {
            if (BattleGameManager.Instance.isInBattleScene && piece.currentCell.topCellAttachment != null)
                return buffThunderResistance + piece.currentCell.topCellAttachment.attachmentThunderResistance;
            else
                return buffThunderResistance;
        }
    }

    public int BuffLightResistance
    {
        get
        {
            if (BattleGameManager.Instance.isInBattleScene && piece.currentCell.topCellAttachment != null)
                return buffLightResistance + piece.currentCell.topCellAttachment.attachmentLightResistance;
            else
                return buffLightResistance;
        }
    }

    public int BuffDarkResistance
    {
        get
        {
            if (BattleGameManager.Instance.isInBattleScene && piece.currentCell.topCellAttachment != null)
                return buffDarkResistance + piece.currentCell.topCellAttachment.attachmentDarkResistance;
            else
                return buffDarkResistance;
        }
    }

    public int Power { get=> Mathf.Clamp(equipPower + basePower + BuffPower, 0, 999); }

    public int HitRate { get=> Mathf.Clamp(equipHitRate + baseHitRate + BuffHitRate, 0, 999);}

    public int Defense { get => Mathf.Clamp(equipDefense + baseDefense + BuffDefense, 0, 999); }

    public int Speed { get => Mathf.Clamp(equipSpeed + baseSpeed + BuffSpeed, 0, 999); }

    public int Magic { get => Mathf.Clamp(equipMagic + baseMagic + BuffMagic, 0, 999); }

    public int Avoid { get => Mathf.Clamp(equipAvoid + baseAvoid + BuffAvoid, 0, 999); }

    public int MagicDefense { get => Mathf.Clamp(equipMagicDefense + baseMagicDefense + BuffMagicDefense, 0, 999); }

    public int VMove { get => Mathf.Clamp(equipVMove + baseVMove + BuffVMove, 0, 999); }

    public int Lucky { get =>Mathf.Clamp(equipLucky + baseLucky+BuffLucky,0,999); }

    public int HMove { get =>Mathf.Clamp(equipHMove + baseHMove+BuffHMove,0,999); }

    public int FireResistance { get => (baseFireResistance + equipFireResistance + BuffFireResistance); }
    public int IceResistance { get => (baseIceResistance + equipIceResistance + BuffIceResistance); }
    public int WindResistance { get => (baseWindResistance + equipWindResistance + BuffWindResistance); }
    public int ThunderResistance { get => (baseThunderResistance + equipThunderResistance + BuffThunderResistance); }
    public int LightResistance { get => (baseLightResistance + equipLightResistance + BuffLightResistance); }
    public int DarkResistance { get => (baseDarkResistance + equipDarkResistance + BuffDarkResistance); }


    public string pieceName;
    public string pieceProfessce;

    public int waitTime;
    public int oriWaitTime = 100;
    public int realWaitTimer;
    public int supposeWaitTimer;

    public int isFly; 

    public LookDirection lookDirection;

    public Sprite pieceSprite;

    [SerializeField]private int currentExp;
    public List<int> levelUpExpList=new List<int>();

    public List<Skill> skillList = new List<Skill>();
    public List<int> LevelUpSkillList = new List<int>(); 

    [SerializeField]protected Weapon weapon;
    protected Equipment armor;
    protected Equipment ornament;

    public int weaponID;
    public int armorID;
    public int ornamentID;

    public int maxHealth;
    [SerializeField] private int currentHealth;

    //public List<BUFF> buffList;

    public float damageRate;
    public float beDamageRate;

    public int baseThreat;

    public int CurrentHealth
    {
        get => currentHealth;
        set
        {

            currentHealth = value;
            if (currentHealth == 0)
                BattleManager.Instance.AddDiePiece(piece);
        }
    }

    public Weapon Weapon { get => weapon;
        set
        {
            if (weapon != null)
                SubEquipmentValue(weapon);

            weapon = value;
            if(value!=null)
            {
                AddEquipmentValue(value);
                weaponID = value.ID;
                weapon.ActiveSkill.SetPiece(piece);
            }
            else
            {
                weaponID = -1;
            }
        }
     }
    public Equipment Armor { get => armor; 
        set
        {
            if (armor != null)
                SubEquipmentValue(armor);

            armor = value;
            if(value!=null)
            {
                AddEquipmentValue(value);
                armorID = value.ID;
            }
            else
            {
                armorID = -1;
            }
        }
    }
    public Equipment Ornament { get => ornament; 
        set
        {
            if (ornament != null)
                SubEquipmentValue(ornament);

            ornament = value;
            if(value!=null)
            {
                AddEquipmentValue(value);
                ornamentID = value.ID;
            }
            else
            {
                ornamentID = -1;
            }
        }
    }

    public int Level { get => level;
        set
        {
            if (value > 100)
                value = 99;

            if(value>=level)
            {
                for (int i = level; i < value; i++)
                {
                    GetSkillForLevelUp(i + 1);
                }
            }
            
            level = value;
        }
     }

    public int CurrentExp { get => currentExp;
        set
        {
            currentExp =value;
            if (currentExp >= levelUpExpList[Level])
            {
                Level += 1;
                CurrentExp -= levelUpExpList[level-1];                
            }
        }
     }

    public bool CanMove
    {
        get => canMove;
        set
        {
            bool temp = canMove;          
            canMove = value;
            if (value != temp)
            {
                MoveChangeEvent();
            }
               
        }
    }

    public void Init(Piece piece)
    {
        this.piece = piece;       
        //buffList = new List<BUFF>();
        waitTime = oriWaitTime;
        lookDirection = LookDirection.Up;
        currentHealth = maxHealth;
        AllLevelExpInit();
        GetAllSkillForInit();
        EquipmentInit();
    }



    public void AddEquipmentValue(Equipment equipment)
    {
        equipPower += equipment.Power;
        equipHitRate += equipment.HitRate;
        equipDefense += equipment.Defense;
        equipSpeed += equipment.Speed;
        equipMagic += equipment.Magic;
        equipAvoid += equipment.Avoid;
        equipMagicDefense += equipment.MagicDefense;
        equipHMove += equipment.HMove;
        equipLucky += equipment.Lucky;
        equipVMove += equipment.VMove;
        equipFireResistance += equipment.FireResistance;
        equipIceResistance += equipment.IceResistance;
        equipWindResistance += equipment.WindResistance;
        equipThunderResistance += equipment.ThunderResistance;
        equipLightResistance += equipment.LightResistance;
        equipDarkResistance += equipment.DarkResistance;
        critical += equipment.Critical;
        equipment.Owner = piece;
    }

    public void SubEquipmentValue(Equipment equipment)
    {
        equipPower -= equipment.Power;
        equipHitRate -= equipment.HitRate;
        equipDefense -= equipment.Defense;
        equipSpeed -= equipment.Speed;
        equipMagic -= equipment.Magic;
        equipAvoid -= equipment.Avoid;
        equipMagicDefense -= equipment.MagicDefense;
        equipVMove -= equipment.VMove;
        equipLucky -= equipment.Lucky;
        equipHMove -= equipment.HMove;
        equipFireResistance -= equipment.FireResistance;
        equipIceResistance -= equipment.IceResistance;
        equipWindResistance -= equipment.WindResistance;
        equipThunderResistance -= equipment.ThunderResistance;
        equipLightResistance -= equipment.LightResistance;
        equipDarkResistance -= equipment.DarkResistance;
        critical -= equipment.Critical;
        equipment.Owner = null;
    }

    public void GetAllSkillForInit()
    {
        skillList.Clear();
        for (int i = 1; i <= Level; i++)
        {
            GetSkillForLevelUp(i);
        }
    }

    public void RemoveSkill(Skill skill)
    {
        if(skill.SkillType==SkillType.Passive)
        {
            PassiveSkill passiveSkill = skill as PassiveSkill;
        }
    }

    public void GetSkillForLevelUp(int level)
    {
        if (LevelUpSkillList[level] != 0)
        {
            SkillManager.Instance.GetSkill(LevelUpSkillList[level], piece);
        }
            
    }

    public void AllLevelExpInit()
    {
        levelUpExpList[1] = 100;
        int i ;
        for (i = 2; i <=20; i++)
        {
            levelUpExpList[i] = (int)(levelUpExpList[i - 1] * 1.1);
        }
        for(;i<99;i++)
        {
            levelUpExpList[i] = (int)(levelUpExpList[i - 1] * 1.05);
        }
    }

    protected virtual  void EquipmentInit()
    {
        
    }

    protected void OneEquipmentInit(int id, UnityAction action1, UnityAction action2)
    {
        if (id != 0)
        {
            action1?.Invoke();
        }
        else
        {
            action2.Invoke();
        }
    }

    protected virtual void MoveChangeEvent()
    {

    }


}
