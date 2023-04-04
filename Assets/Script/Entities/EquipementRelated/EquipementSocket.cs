[System.Serializable]
public class EquipementSocket
{
    public EquipementType Type = EquipementType.Null;
    public EquipementObject equipementObject = null;
    public bool Empty => equipementObject == null;
}