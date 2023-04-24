using UnityEngine;

[System.Serializable]
public class Modifier
{
    public ModifierType Type = ModifierType.UpPhysical;
    public string Value = string.Empty;
    public float FloatValue => Value.ToFloat();

    public Modifier(ModifierType type, string value)
    {
        Type = type;
        Value = value;
    }

    public Modifier(string saveData)
    {
        string[] splitData = saveData.Split('|');
        Type = (ModifierType)splitData[0].ToInt();
        Value = splitData[1];
    } 

    public string ToSave()
    {
        return (int)Type + "|" + Value;
    }
}