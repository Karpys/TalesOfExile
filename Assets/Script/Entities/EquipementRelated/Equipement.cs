using System;
using System.Collections.Generic;

[System.Serializable]
public class Equipement
{
    public string Description = String.Empty;
    public EquipementType Type = EquipementType.Null;
    public List<Modifier> Modifiers = new List<Modifier>();

    public Equipement(string description, EquipementType type, List<Modifier> modifiers)
    {
        Description = description;
        Type = type;

        foreach (Modifier modifier in modifiers)
        {
            Modifiers.Add(new Modifier(modifier.Type,modifier.Value));
        }
    }
}