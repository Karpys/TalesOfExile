using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipementObject:InventoryObject
{
    private EquipementType m_Type = EquipementType.Null;
    private List<Modifier> m_Modifiers = new List<Modifier>();

    public EquipementObjectData BaseEquipementData => Data as EquipementObjectData;
    public List<Modifier> Modifiers => m_Modifiers;
    public EquipementType Type => m_Type;

    public EquipementObject(EquipementObjectData data) : base(data)
    {
        m_Type = data.EquipementType;

        foreach (Modifier modifier in data.EquipementBaseModifiers)
        {
            m_Modifiers.Add(new Modifier(modifier.Type,modifier.Value));
        }
    }

    public override List<ItemButtonUIParameters> ButtonRequestOptionButton()
    { 
        List<ItemButtonUIParameters> itemButtonParameters = base.ButtonRequestOptionButton();
        itemButtonParameters.Add(new ItemButtonUIParameters(TryEquip,"Equip"));
        return itemButtonParameters;
    }

    private void TryEquip()
    {
        //Todo:Add Equip stats check ?
        EquipementUtils.Equip(this,GameManager.Instance.PlayerEntity);
        Debug.Log("Try Equip");
    }

    //Save Part
    public override string GetSaveData()
    {
        return base.GetSaveData();
    }
}