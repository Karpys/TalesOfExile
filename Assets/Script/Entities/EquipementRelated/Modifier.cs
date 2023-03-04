using UnityEngine;

[System.Serializable]
public class Modifier
{
    public ModifierType Type = ModifierType.UpPhysical;
    public string Value = string.Empty;
    public float FloatValue => float.Parse(Value);

    public Modifier(ModifierType type, string value)
    {
        Type = type;
        Value = value;
    }
}