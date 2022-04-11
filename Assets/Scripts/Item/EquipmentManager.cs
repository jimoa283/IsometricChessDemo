using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : Singleton<EquipmentManager>
{
    private Dictionary<int, Equipment> equipmentDic;
    private Dictionary<int, Weapon> weaponDic;
    public EquipmentManager()
    {
        equipmentDic = new Dictionary<int, Equipment>();
        weaponDic = new Dictionary<int, Weapon>();
        ParseEquipmentJson();
    }

    private void ParseEquipmentJson()
    {
        TextAsset textEquipment = Resources.Load<TextAsset>("JSON/EquipmentJSON");
        JSONObject jSONObject = new JSONObject(textEquipment.text);
        foreach (var obj in jSONObject.list)
        {
            int id = (int)obj["ID"].n;
            string _name = obj["Name"].str;
            string _elementType = obj["ElementType"].str;
            ElementType elementType = (ElementType)System.Enum.Parse(typeof(ElementType), _elementType);
            string info = obj["Info"].str;
            string spritePath = obj["SpritePath"].str;
            Sprite sprite = Resources.Load<GameObject>(spritePath).GetComponent<SpriteRenderer>().sprite;

            string _itemType = obj["ItemType"].str;
            ItemType itemType = (ItemType)System.Enum.Parse(typeof(ItemType), _itemType);

            int buyPrice =(int)obj["BuyPrice"].n;
            int sellPrice = (int)obj["SellPrice"].n;

            string _equipment = obj["EquipmentType"].str;
            EquipmentType equipmentType = (EquipmentType)System.Enum.Parse(typeof(EquipmentType), _equipment);

            int power = (int)obj["Power"].n;
            int hitRate = (int)obj["HitRate"].n;
            int defense = (int)obj["Defense"].n;
            int speed = (int)obj["Speed"].n;
            int magic = (int)obj["Magic"].n;
            int avoid = (int)obj["Avoid"].n;
            int magicDefense = (int)obj["MagicDefense"].n;
            int vMove = (int)obj["VMove"].n;
            int lucky = (int)obj["Lucky"].n;
            int hMove = (int)obj["HMove"].n;
            int critical = (int)obj["Critical"].n;

            int fireResistance = (int)obj["FireResistance"].n;
            int iceResistance = (int)obj["IceResistance"].n;
            int windResistance = (int)obj["WindResistance"].n;
            int thunderResistance = (int)obj["ThunderResistance"].n;
            int lightResistance = (int)obj["LightResistance"].n;
            int darkResistance = (int)obj["DarkResistance"].n;

            if (equipmentType==EquipmentType.Weapon)
            {
                int skillID =(int) obj["SkillID"].n;
                ActiveSkill activeSkill = SkillManager.Instance.GetActiveSkill(skillID);
                Weapon weapon = new Weapon(id, _name, elementType, info, sprite, itemType,buyPrice,sellPrice ,equipmentType,
                               power,hitRate,defense,speed,magic,avoid,magicDefense,vMove,lucky,hMove,critical,
                               fireResistance, iceResistance, windResistance, thunderResistance, lightResistance, darkResistance,
                               activeSkill);
                weaponDic.Add(id, weapon);
            }
            else
            {
                Equipment equipment = new Equipment(id, _name, elementType, info, sprite, itemType,buyPrice,sellPrice ,equipmentType, 
                                                power,hitRate,defense,speed,magic,avoid,magicDefense,vMove,lucky,hMove,critical,
                                                fireResistance,iceResistance,windResistance,thunderResistance,lightResistance,darkResistance);
                equipmentDic.Add(id, equipment);
            }          
        }  
    }

    public Equipment GetEquipment(int id)
    {
        if(weaponDic.ContainsKey(id))
        {
            return GetWeapon(id);
        }
        else if (equipmentDic.ContainsKey(id))
        {
            return CloneEquipment(equipmentDic[id]);
        }            
        return null;
    }

    public Equipment GetNumEquipment(int id, int num)
    {
        Equipment equipment = GetEquipment(id);
        if (equipment != null)
            equipment.Number += num;
        return equipment;
    }

    public Weapon GetWeapon(int id)
    {
        if (weaponDic.ContainsKey(id))
            return CloneWeapon(weaponDic[id]);

        return null;
    }

    private Equipment CloneEquipment(Equipment ori)
    {
        return new Equipment(ori.ID, ori.Name, ori.Type, ori.Info, ori.Sprite, ori.ItemType,ori.BuyPrice,ori.SellPrice,ori.EquipmentType,
                             ori.Power,ori.HitRate,ori.Defense,ori.Speed,ori.Magic,ori.Avoid,ori.MagicDefense,
                             ori.VMove,ori.Lucky,ori.HMove,ori.Critical,ori.FireResistance,ori.IceResistance,ori.WindResistance,ori.ThunderResistance,
                             ori.LightResistance,ori.DarkResistance);
    }

    private Weapon CloneWeapon(Weapon ori)
    {
        return new Weapon(ori.ID, ori.Name, ori.Type, ori.Info, ori.Sprite, ori.ItemType,ori.BuyPrice,ori.SellPrice ,ori.EquipmentType,
                       ori.Power, ori.HitRate, ori.Defense, ori.Speed, ori.Magic, ori.Avoid, ori.MagicDefense,
                       ori.VMove,ori.Lucky,ori.HMove ,ori.Critical, ori.FireResistance, ori.IceResistance, ori.WindResistance, ori.ThunderResistance,
                             ori.LightResistance, ori.DarkResistance,SkillManager.Instance.GetActiveSkill(ori.ActiveSkill.ID));
    }
}
