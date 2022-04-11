using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : PieceStatus
{
    protected override void EquipmentInit()
    {
        OneEquipmentInit(weaponID, () => { Weapon = EquipmentManager.Instance.GetNumEquipment(weaponID, 1) as Weapon; },
            () => { Weapon = null; });

        OneEquipmentInit(armorID, () => { Armor = EquipmentManager.Instance.GetNumEquipment(armorID, 1); },
            () => { Armor = null; });

        OneEquipmentInit(ornamentID, () => { Ornament = EquipmentManager.Instance.GetNumEquipment(ornamentID, 1); },
            () => { Ornament = null; });
    }

    protected override void MoveChangeEvent()
    {
        (piece as Enemy).GetRiskCellList();
    }
}
