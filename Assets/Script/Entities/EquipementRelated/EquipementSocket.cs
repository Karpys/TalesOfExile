using System;
using KarpysDev.Script.Items;

namespace KarpysDev.Script.Entities.EquipementRelated
{
    [Serializable]
    public class EquipementSocket
    {
        public EquipementType Type = EquipementType.Null;
        public EquipementItem EquipementItem = null;
        public bool Empty => EquipementItem == null;
    }
}