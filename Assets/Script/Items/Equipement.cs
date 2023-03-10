using System;
using System.Collections.Generic;

[System.Serializable]
public class Equipement:InventoryObject
{
    public string Description = String.Empty;
    public EquipementType Type = EquipementType.Null;
    public List<Modifier> Modifiers = new List<Modifier>();

    public Equipement(Equipement equipement)
    {
        Description = equipement.Description;
        Type = equipement.Type;

        foreach (Modifier modifier in equipement.Modifiers)
        {
            Modifiers.Add(new Modifier(modifier.Type,modifier.Value));
        }
    }
}