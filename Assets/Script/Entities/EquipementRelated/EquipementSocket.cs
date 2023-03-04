[System.Serializable]
public class EquipementSocket
{
    public EquipementType Type = EquipementType.Null;
    public Equipement Equipement = null;
    public bool Empty => Equipement.Type == EquipementType.Null || Equipement == null;
}