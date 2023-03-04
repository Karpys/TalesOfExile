[System.Serializable]
public class Modifier
{
    public ModifierType Type = ModifierType.UpPhysical;
    public float Value = 0f;

    public Modifier(ModifierType type, float value)
    {
        Type = type;
        Value = value;
    }
}