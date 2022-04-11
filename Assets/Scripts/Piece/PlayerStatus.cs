using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : PieceStatus
{
    protected override void EquipmentInit()
    {
        OneEquipmentInit(weaponID, () => { if (weapon == null||weaponID != weapon.ID) Weapon = ItemManager.Instance.GetEquipmentForPlayerInLoad(weaponID, piece) as Weapon;
            ;
        },
              () => { Weapon = null; });

        OneEquipmentInit(armorID, () => { if (armor == null||armorID != armor.ID) Armor = ItemManager.Instance.GetEquipmentForPlayerInLoad(armorID, piece);
            ;
        },
            () => { Armor = null; });

        OneEquipmentInit(ornamentID, () => { if (ornament == null||ornamentID != ornament.ID) Ornament = ItemManager.Instance.GetEquipmentForPlayerInLoad(ornamentID, piece);
            ;
        },
            () => { Ornament = null; });
    }

    
}
