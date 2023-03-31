using System;
using System.Collections.Generic;

[System.Serializable]
public class Equipement:InventoryObject
{
    private string m_Description = String.Empty;
    private EquipementType m_Type = EquipementType.Null;
    private List<Modifier> m_Modifiers = new List<Modifier>();

    public List<Modifier> Modifiers => m_Modifiers;
    public EquipementType Type => m_Type;
    public Equipement(Equipement equipement):base(null)
    {
        m_Description = equipement.m_Description;
        m_Type = equipement.m_Type;

        foreach (Modifier modifier in equipement.m_Modifiers)
        {
            m_Modifiers.Add(new Modifier(modifier.Type,modifier.Value));
        }
    }
}