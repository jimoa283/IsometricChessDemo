using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class PieceDetailInfoUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private Image pieceImage;
    private GameObject battleIcon;
    private Text levelText;
    private Text expText;

    private Text pieceNameText;
    private Text pieceProfess;
    private Text hpText;
    private Image healthBar;

    private Text fireResistanceText;
    private Text iceResistanceText;
    private Text windResistanceText;
    private Text thunderResistanceText;
    private Text lightResistanceText;
    private Text darkResistanceText;

    private PieceAbilitySlot powerSlot;
    private PieceAbilitySlot hitRateSlot;
    private PieceAbilitySlot defenseSlot;
    private PieceAbilitySlot speedSlot;
    private PieceAbilitySlot magicSlot;
    private PieceAbilitySlot avoidSlot;
    private PieceAbilitySlot magicDefenseSlot;
    private PieceAbilitySlot jumpSlot;
    private PieceAbilitySlot luckySlot;
    private PieceAbilitySlot moveSlot;

    private EquipmentSlot[] equipmentSlots;

    private SkillSlot[] skillSlots;

    private GameObject arrow;
    private ELementDetailShowUI eLementDetailShow;

    private int equipIndex;
    private int equipLimitIndex;

    private int skillIndex;
    private int skillLimitIndex;

    private int bagIndex;
    private UnityAction[] changeActions;

    private Piece currentPiece;

    private bool canControllBag;

    private PieceBuffShowSlot[] pieceBuffShowSlots;

    private RectTransform pieceDAIRT;
    private RectTransform pieceSkillRT;
    private RectTransform buffListRT;

    public int EquipIndex { get => equipIndex;
        set
        {
            equipmentSlots[equipIndex].CancelSelect();
            ChangeSelectEquip(value);
        }
    }
    public int SkillIndex { get => skillIndex;
        set
        {
            skillSlots[SkillIndex].CancelSelect();
            ChangeSelectSkill(value);
        }
     }

    public int BagIndex { get => bagIndex;
        set
        {
            bagIndex = (value+changeActions.Length)%changeActions.Length;
            changeActions[bagIndex].Invoke();
        }
     }

    public void Init()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        pieceDAIRT = transform.GetChildTransform("pieceDAI").GetComponent<RectTransform>();
        pieceSkillRT = transform.GetChildTransform("pieceSkill").GetComponent<RectTransform>();
        buffListRT = transform.GetChildTransform("BuffShowContent").GetComponent<RectTransform>();
        pieceImage = transform.GetChildTransform("pieceImage").GetComponent<Image>();
        battleIcon = transform.GetChildTransform("BattleIcon").gameObject;
        levelText = transform.GetChildTransform("Level").GetComponent<Text>();
        expText = transform.GetChildTransform("Exp").GetComponent<Text>();

        pieceNameText = transform.GetChildTransform("pieceName").GetComponent<Text>();
        pieceProfess = transform.GetChildTransform("pieceProfessceName").GetComponent<Text>();
        hpText = transform.GetChildTransform("HPValue").GetComponent<Text>();
        healthBar = transform.GetChildTransform("pieceHealthBar").GetComponent<Image>();

        Transform resistanceContent = transform.GetChildTransform("ResistanceContext");
        fireResistanceText = resistanceContent.GetChildTransform("FireResistanceText").GetComponent<Text>();
        iceResistanceText = resistanceContent.GetChildTransform("IceResistanceText").GetComponent<Text>();
        windResistanceText = resistanceContent.GetChildTransform("WindResistanceText").GetComponent<Text>();
        thunderResistanceText = resistanceContent.GetChildTransform("ThunderResistanceText").GetComponent<Text>();
        lightResistanceText = resistanceContent.GetChildTransform("LightResistanceText").GetComponent<Text>();
        darkResistanceText = resistanceContent.GetChildTransform("DarkResistanceText").GetComponent<Text>();

        powerSlot = transform.GetChildTransform("Power").GetComponent<PieceAbilitySlot>();
        powerSlot.Init();

        hitRateSlot = transform.GetChildTransform("Hit").GetComponent<PieceAbilitySlot>();
        hitRateSlot.Init();

        defenseSlot = transform.GetChildTransform("Defense").GetComponent<PieceAbilitySlot>();
        defenseSlot.Init();

        speedSlot = transform.GetChildTransform("Speed").GetComponent<PieceAbilitySlot>();
        speedSlot.Init();

        magicSlot = transform.GetChildTransform("Magic").GetComponent<PieceAbilitySlot>();
        magicSlot.Init();

        avoidSlot = transform.GetChildTransform("Avoid").GetComponent<PieceAbilitySlot>();
        avoidSlot.Init();

        magicDefenseSlot = transform.GetChildTransform("MagicDefense").GetComponent<PieceAbilitySlot>();
        magicDefenseSlot.Init();

        jumpSlot = transform.GetChildTransform("Jump").GetComponent<PieceAbilitySlot>();
        jumpSlot.Init();

        luckySlot = transform.GetChildTransform("Lucky").GetComponent<PieceAbilitySlot>();
        luckySlot.Init();

        moveSlot = transform.GetChildTransform("Move").GetComponent<PieceAbilitySlot>();
        moveSlot.Init();

        equipmentSlots = GetComponentsInChildren<EquipmentSlot>();
        foreach(var slot in equipmentSlots)
        {
            slot.Init();
        }
        skillSlots = GetComponentsInChildren<SkillSlot>();
        foreach(var slot in skillSlots)

        {
            slot.Init();
        }

        pieceBuffShowSlots = GetComponentsInChildren<PieceBuffShowSlot>();
        foreach (var slot in pieceBuffShowSlots)
        {
            slot.Init();
        }

        arrow = transform.Find("Arrow").gameObject;
        eLementDetailShow = arrow.transform.Find("ActionInfoUI").GetComponent<ELementDetailShowUI>();
        eLementDetailShow.Init();

        changeActions = new UnityAction[] { ChangeToEquipmentBag, ChangeToSkillBag };

        canControllBag = false;

        bagIndex = 0;

        equipIndex = 0;
        equipLimitIndex = 1;

        skillIndex = 0;
        skillLimitIndex = 1;
    }

    private void Update()
    {
        if(!canControllBag)
        {
            if(Input.GetKeyDown(KeyCode.J))
            {
                canControllBag = true;
                bagIndex = 0;
                arrow.gameObject.SetActive(true);
                ChangeToEquipmentBag();
            }
        }
        if(canControllBag)
        {
            if(bagIndex==0)
            {
                if (Input.GetKeyDown(KeyCode.W))
                    --EquipIndex;
                if (Input.GetKeyDown(KeyCode.S))
                    ++EquipIndex;

                if(Input.GetKeyDown(KeyCode.I))
                {
                    arrow.gameObject.SetActive(false);
                    equipmentSlots[equipIndex].CancelSelect();
                    canControllBag = false;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.W))
                    --SkillIndex;
                if (Input.GetKeyDown(KeyCode.S))
                    ++SkillIndex;

                if (Input.GetKeyDown(KeyCode.I))
                {
                    arrow.gameObject.SetActive(false);
                    skillSlots[skillIndex].CancelSelect();
                    canControllBag = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.A))
                --BagIndex;

            if (Input.GetKeyDown(KeyCode.D))
                ++BagIndex;
        }
    }

    public void OpenDetailInfoUI(Piece piece)
    {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f);
        pieceSkillRT.localPosition = new Vector3(pieceSkillRT.localPosition.x+270, pieceSkillRT.localPosition.y);
        pieceDAIRT.localPosition = new Vector3(pieceDAIRT.localPosition.x + 270, pieceDAIRT.localPosition.y);
        buffListRT.localPosition = new Vector3(buffListRT.localPosition.x + 270, buffListRT.localPosition.y);
        pieceSkillRT.DOLocalMoveX(pieceSkillRT.localPosition.x - 270, 0.5f);
        pieceDAIRT.DOLocalMoveX(pieceDAIRT.localPosition.x - 270, 0.5f);
        buffListRT.DOLocalMoveX(buffListRT.localPosition.x - 270, 0.5f);
        ChangePieceInfo(piece);
    }

    public void ChangePieceInfo(Piece piece)
    {
        SetBaseInfo(piece);
        SetHealthValue(piece.pieceStatus);
        SetPieceResistance(piece.pieceStatus);
        SetPieceAbilityValue(piece.pieceStatus);
        SetEquip(piece.pieceStatus);
        SetSkill(piece.pieceStatus.skillList);
        ShowPieceBUFF(piece.PieceBUFFList);
        currentPiece = piece;
        canControllBag = false;
        arrow.gameObject.SetActive(false);
    }

    private void SetBaseInfo(Piece piece)
    {
        pieceImage.sprite = piece.pieceStatus.pieceSprite;
        if(piece.CompareTag("Player")&&PieceQueueManager.Instance.PieceList.Contains(piece))
        {
            battleIcon.SetActive(true);
        }
        else
        {
            battleIcon.SetActive(false);
        }
        levelText.text = "等级<size=25>" + piece.pieceStatus.Level + "</size>";
        expText.text = "经验值" + piece.pieceStatus.CurrentExp;
        pieceNameText.text = piece.pieceStatus.pieceName;
    }

     private void ShowPieceBUFF(PieceBUFFList pieceBUFFList)
    {
        int i = 0;
        for (i = 0; i < pieceBuffShowSlots.Length; i++)
        {
            if (i == pieceBUFFList.BuffList.Count)
                break;
            pieceBuffShowSlots[i].ShowPieceBuff(pieceBUFFList.BuffList[i]);
        }
        for (; i < pieceBuffShowSlots.Length; i++)
        {
            pieceBuffShowSlots[i].gameObject.SetActive(false);
        }
    }

    private void SetHealthValue(PieceStatus pieceStatus)
    {
        healthBar.fillAmount = (float)pieceStatus.CurrentHealth / pieceStatus.maxHealth;
        hpText.text = "<size=20>" +pieceStatus.CurrentHealth + "</size>/" +pieceStatus.maxHealth;
    }

    private void SetPieceResistance(PieceStatus pieceStatus)
    {
        fireResistanceText.text = pieceStatus.FireResistance+"%";
        iceResistanceText.text = pieceStatus.IceResistance + "%";
        windResistanceText.text = pieceStatus.WindResistance + "%";
        thunderResistanceText.text = pieceStatus.ThunderResistance + "%";
        lightResistanceText.text = pieceStatus.LightResistance + "%";
        darkResistanceText.text = pieceStatus.DarkResistance + "%";
    }

    private void SetPieceAbilityValue(PieceStatus pieceStatus)
    {
        powerSlot.SetValue(pieceStatus.basePower+pieceStatus.equipPower, pieceStatus.BuffPower);
        hitRateSlot.SetValue(pieceStatus.baseHitRate+pieceStatus.equipHitRate, pieceStatus.BuffHitRate);
        defenseSlot.SetValue(pieceStatus.baseDefense+pieceStatus.equipAvoid, pieceStatus.BuffDefense);
        speedSlot.SetValue(pieceStatus.baseSpeed+pieceStatus.equipSpeed, pieceStatus.BuffSpeed);
        magicDefenseSlot.SetValue(pieceStatus.baseMagicDefense+pieceStatus.equipMagicDefense, pieceStatus.BuffMagicDefense);
        jumpSlot.SetValue(pieceStatus.baseVMove+pieceStatus.equipVMove, pieceStatus.BuffVMove);
        avoidSlot.SetValue(pieceStatus.baseAvoid+pieceStatus.equipAvoid, pieceStatus.BuffAvoid);
        magicSlot.SetValue(pieceStatus.baseMagic+pieceStatus.equipMagic, pieceStatus.BuffMagic);
        luckySlot.SetValue(pieceStatus.baseLucky+pieceStatus.equipLucky, pieceStatus.BuffLucky);
        moveSlot.SetValue(pieceStatus.baseHMove+pieceStatus.equipHMove, pieceStatus.BuffHMove);
    }

    private void SetEquip(PieceStatus pieceStatus)
    {
        if (SetOneEquip(pieceStatus.Weapon, equipmentSlots[0]))
            equipLimitIndex = 1;

        if (SetOneEquip(pieceStatus.Armor, equipmentSlots[1]))
            equipLimitIndex = 2;

        if (SetOneEquip(pieceStatus.Ornament, equipmentSlots[2]))
            equipLimitIndex = 3;
        
    }

    private bool SetOneEquip(Equipment equipment,EquipmentSlot equipmentSlot)
    {
        if (equipment != null)
        {
            equipmentSlot.SetEquipment(equipment);
            return true;
        }           
        else
        {
            equipmentSlot.ClearSlot();
            return false;
        }
    }

    private void SetSkill(List<Skill> list)
    {
        int i = 0;
        for ( i = 0; i < skillSlots.Length; i++)
        {
            if (list.Count < i + 1)
                break;
            skillSlots[i].SetSkillSlot(list[i]);
        }

        skillLimitIndex = i;
        for (;  i< skillSlots.Length; i++)
        {
            skillSlots[i].ClearSlot();
        }
    }

    private void ChangeToEquipmentBag()
    {
        skillSlots[skillIndex].CancelSelect();
        ChangeArrowPos(false);
        eLementDetailShow.transform.localPosition = new Vector2(170, 85);
        ChangeSelectEquip(0);
    }

    private void ChangeToSkillBag()
    {
        equipmentSlots[equipIndex].CancelSelect();
        ChangeArrowPos(true);
        eLementDetailShow.transform.localPosition = new Vector2(-144, -39);
        ChangeSelectSkill(0);
    }

    private void ChangeSelectEquip(int num)
    {
        int temp = num > equipIndex ? 1 : -1;
        equipIndex = (num + equipLimitIndex) % equipLimitIndex;
        while(equipmentSlots[equipIndex].equipment==null)
        {
            equipIndex = (equipIndex + temp + equipLimitIndex) % equipLimitIndex;
        }
        equipmentSlots[equipIndex].SelectSlot();
        eLementDetailShow.ChangeEquipmentInfoShow(equipmentSlots[equipIndex].equipment);
        arrow.transform.position = new Vector3(arrow.transform.position.x, equipmentSlots[EquipIndex].transform.position.y);
    }

    private void ChangeSelectSkill(int num)
    {       
        skillIndex = (num + skillLimitIndex) % skillLimitIndex;
        skillSlots[SkillIndex].SelectSlot();
        eLementDetailShow.ChangeSkillInfoShow(skillSlots[skillIndex].skill);
        arrow.transform.position = new Vector3(arrow.transform.position.x, skillSlots[SkillIndex].transform.position.y);
        if (skillIndex == 9)
            ChangeEleDetailShowPos(false);
        else if (skillIndex == 8)
            ChangeEleDetailShowPos(true);
    }

    private void ChangeEleDetailShowPos(bool isDown)
    {
        if (!isDown)
            eLementDetailShow.transform.localPosition = new Vector2(eLementDetailShow.transform.localPosition.x, 35);
        else
            eLementDetailShow.transform.localPosition = new Vector2(eLementDetailShow.transform.localPosition.x, -39);
    }

    private void ChangeArrowPos(bool isRight)
    {
        if (isRight)
            arrow.transform.localPosition = new Vector2(99, arrow.transform.localPosition.y);
        else
            arrow.transform.localPosition = new Vector3(-207, arrow.transform.localPosition.y);
    }
}
