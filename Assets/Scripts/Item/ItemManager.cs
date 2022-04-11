using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemManager : Singleton<ItemManager>
{
    private List<UseItem> allUseItemBag;
    private List<Equipment> allEquipmentBag;
    private List<Item> allItemBag;


    public List<UseItem> AllUseItemBag { get => allUseItemBag;}
    public List<Equipment> AllEquipmentBag { get => allEquipmentBag;}

    public List<Item> AllItemBag { get 
        {
            allItemBag.Clear();
            allItemBag.AddRange(allUseItemBag);
            allItemBag.AddRange(allEquipmentBag);
            return allItemBag;
        }
    }

    public ItemManager()
    {
        allItemBag = new List<Item>();
        allUseItemBag = new List<UseItem>();
        allEquipmentBag = new List<Equipment>();
    }

    public void Init()
    {

    }

    public Item CheckItemInBag(int id)
    {
        if(id>1000&&id<2000)
            return CheckUseItemInBag(id);
        else if (id>2000&&id<3000)
            return CheckEquipmentInBag(id);
        return null;
    }

    public UseItem CheckUseItemInBag(int id)
    {
      return  CheckItemInBag(id, allUseItemBag,
        (x,y) =>
        {
            if (x == y.ID)
                return true;
            return false;
        }
        );
    }

    public T CheckItemInBag<T>(int id,List<T> list,Func<int,T,bool> func)where T:Item
    {
        foreach(var item in list)
        {
            if(func(id,item))
                return item;
        }
        return null;
    }

    public Equipment CheckEquipmentInBag(int id)
    {
      return  CheckItemInBag(id, allEquipmentBag,
        (x,y)=>
        {
            if (y.Owner == null && x == y.ID)
                return true;

            return false;
        }
            );
    }

    public void ChangeItemNum(Item item ,int num)
    {        
        if(item.ItemType==ItemType.UseItem)
        {
            //ChangeUseItemNum(item as UseItem,num);
            ChangeItemNum(item as UseItem, allUseItemBag, num);
        }
        else if(item.ItemType==ItemType.Equipment)
        {
            //ChangeEquipmentNum(item as Equipment, num);
            ChangeItemNum(item as Equipment, allEquipmentBag, num);
        }
    }

    public void ChangeItemNum<T>(T item,List<T> list,int num)where T:Item
    {
        T _item = CheckItemInBag(item.ID) as T;
        if (_item == null)
        {
            InsertItem(item, list);
            item.Number += num;
        }
        else
            _item.Number += num;
    }


    /*public void ChangeUseItemNum(UseItem item,int num)
    {
        //UseItem useItem = CheckUseItemInBag(item);
        UseItem useItem = CheckItemInBag(item, allUseItemBag);
        if(useItem==null)
        {
            //allItemBag.Add(item);
            //allUseItemBag.Add(item);
            InsertUseItem(item);
            item.Number += num;
        }
        else
            useItem.Number += num;
    }

    public void ChangeEquipmentNum(Equipment _equip,int num)
    {
        Equipment equip = CheckItemInBag(_equip, allEquipmentBag);
       
        if (equip==null)
        {
            //allItemBag.Add(_equip);
            InsertEquipment(_equip);
            _equip.Number += num;
        }
        else
            equip.Number += num;
    }*/


    public void RemoveItem(Item item)
    {
        //allItemBag.Remove(item);
        if (item.ItemType == ItemType.UseItem)
        {
            allUseItemBag.Remove(item as UseItem);
        }         
        else if (item.ItemType == ItemType.Equipment)
            RemoveEquipment(item as Equipment);
    }
   
    public void InsertItem(Item item)
    {
        if(item.ItemType==ItemType.UseItem)
        {
            InsertUseItem(item as UseItem);
        }
        else if(item.ItemType==ItemType.Equipment)
        {
            InsertEquipment(item as Equipment);
        }
    }

    public void InsertItem<T>(T item,List<T> list) where T:Item
    {
        if(list.Count>0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if(item.ID<list[i].ID)
                {
                    list.Insert(i, item);
                    return;
                }
            }
        }

        list.Add(item);
    }

    public void InsertUseItem(UseItem useItem)
    {

        InsertItem(useItem, allUseItemBag);
    }

    public void InsertEquipment(Equipment equipment)
    {

        InsertItem(equipment, allEquipmentBag);
    }

    public Item GetNumItem(int id,int num)
    {
        if (id > 1000 && id < 2000)
            return UseItemManager.Instance.GetNumUseItem(id, num);
        else if (id > 2000 && id < 3000)
            return EquipmentManager.Instance.GetNumEquipment(id, num);

        return null;
    }

    public void RemoveEquipment(Equipment equipment)
    {
        if(equipment.Owner!=null)
        {
            switch (equipment.EquipmentType)
            {
                case EquipmentType.Weapon:
                    equipment.Owner.pieceStatus.Weapon = null;
                    break;
                case EquipmentType.Armor:
                    equipment.Owner.pieceStatus.Armor = null;
                    break;
                case EquipmentType.Ornament:
                    equipment.Owner.pieceStatus.Ornament = null;
                    break;
                default:
                    break;
            }
        }

        allEquipmentBag.Remove(equipment);
    }

    public Equipment GetEquipmentForPlayerInLoad(int id,Piece piece)
    {
        Equipment equipment = CheckEquipmentInBag(id);
        if (equipment == null)
        {
            return null;
        }
            equipment.Number -= 1;
        if (equipment.Number == 0)
            allEquipmentBag.Remove(equipment);
        Equipment newEquip = EquipmentManager.Instance.GetNumEquipment(id,1);
        if(newEquip!=null)
         InsertEquipment(newEquip);

        return newEquip;
    }

    public Equipment CheckEquipmentForLoad(int id,Piece piece)
    {
        foreach (var item in allEquipmentBag)
        {
            if ((id==item.ID&&item.Owner==null)||item.Owner==piece)
                return item;
        }
        return null;
    }

   /* public Weapon TestPlayerGetWeapon(int id)
    {
        InsertEquipment(EquipmentManager.Instance.GetNumEquipment(id, 1));
        return GetEquipmentForPlayerInLoad(id) as Weapon;
    }

    public Equipment TestPlayerGetOrnament(int id)
    {
        InsertEquipment(EquipmentManager.Instance.GetNumEquipment(id, 1));
        return GetEquipmentForPlayerInLoad(id);
    }*/
}
