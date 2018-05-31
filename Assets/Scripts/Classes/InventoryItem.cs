using UnityEngine;
using System.Collections;

// 描述库存项目的脚本对象
public class InventoryItem : ScriptableObject
{
    public Sprite itemImage;
    public string itemName;
    public int cost;
    public int strength;
}