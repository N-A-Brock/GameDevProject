using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemObject : ScriptableObject
{
    public enum ItemType
    {
        NORMAL,
        CONSUMABLE,
        EQUIPABLE,
        BLANK
    }
    public string itemName;
    public string itemDesc;

    public Texture itemSprite;
    public ItemType itemType;
    public GameObject itemModel;


}
