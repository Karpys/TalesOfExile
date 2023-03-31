using UnityEngine;

public class LootLibrary : SingletonMonoBehavior<LootLibrary>
{
    [SerializeField] private DefaultInventoryObject m_DropTest = null;

    //Replace that with a real library and dont use new InventoryObject instead of library class//
    public InventoryObject GetDropTest()
    {
        return m_DropTest;
    }
}