using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipementObject:InventoryObject
{
    private EquipementType m_Type = EquipementType.Null;
    private Modifier[] m_ItemBaseModifiers = null;
    private Modifier[] m_AdditionalModifiers = Array.Empty<Modifier>();

    private bool m_IsEquiped = false;
    public EquipementObjectData BaseEquipementData => Data as EquipementObjectData;
    public Modifier[] ItemModifiers => GetAllModifiers();
    
    public EquipementType Type => m_Type;

    public EquipementObject(EquipementObjectData data) : base(data)
    {
        m_Type = data.EquipementType;

        m_ItemBaseModifiers = new Modifier[data.EquipementBaseModifiers.Count];

        for (int i = 0; i < data.EquipementBaseModifiers.Count; i++)
        {
            Modifier modifier = data.EquipementBaseModifiers[i];
            m_ItemBaseModifiers[i] = new Modifier(modifier.Type, modifier.Value);
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
        m_IsEquiped = true;
        Debug.Log("Try Equip");
    }

    public void UnEquip()
    {
        m_IsEquiped = false;
    }

    private Modifier[] GetAllModifiers()
    {
        Modifier[] modifiers = new Modifier[m_ItemBaseModifiers.Length + m_AdditionalModifiers.Length];

        for (int i = 0; i < m_ItemBaseModifiers.Length; i++)
        {
            modifiers[i] = m_ItemBaseModifiers[i];
        }

        for (int i = 0; i < m_AdditionalModifiers.Length; i++)
        {
            modifiers[i + m_ItemBaseModifiers.Length] = m_AdditionalModifiers[i];
        }

        return modifiers;
    }

    //Save Part
    public override string GetSaveData()
    {
        string saveData = base.GetSaveData();
        saveData += " " + m_IsEquiped;
        return saveData;
    }
}