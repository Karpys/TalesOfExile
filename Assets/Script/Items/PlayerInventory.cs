using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : SingletonMonoBehavior<PlayerInventory>
{
    private List<InventoryObject> m_PlayerInventory = new List<InventoryObject>();
}
