using UnityEngine;
using System.Collections.Generic;

public class Player : Entity
{
    public List<InventoryItem> Inventory = new List<InventoryItem>();
    public string[] Skills;
    public int Money;

    private void Awake()
    {
        health = 100;
    }

    public void AddinventoryItem(InventoryItem item)
    {
        Inventory.Add(item);
    }
}
