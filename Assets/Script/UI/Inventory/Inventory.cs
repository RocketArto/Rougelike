using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : ScriptableObject
{
    public event Action<Dictionary<int, GameObject>> // sau nay se viet mot class rieng
        OnInventoryUpdated;

    [SerializeField]
    private List<GameObject> inventoryItems;

    [field: SerializeField]

    public int Size { get; private set; } = 10;

    public void Initialize() { }
}