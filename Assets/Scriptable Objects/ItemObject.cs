using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemObject : ScriptableObject
{
    public enum ItemType
    {
        OTHER,
        CONSUMABLE,
        EQUIPABLE,
        BLANK
    }
    public string itemName;
    public string itemDesc;

    public bool hasInternalStorage;
    public int internalStorage;

    public int maxAmountHeld;

    public Sprite itemSprite;
    public ItemType itemType;
    public GameObject itemObject;

    public Vector3 colliderCenter;
    public Vector3 colliderSize;

    public int useSelector;

    public ItemObject useTarget;

    public int ConsumableUseAmount()
    {
        switch (itemName)
        {
            case "Oil":
                return (60);
            case "Ammo":
                return (1);
            case "Capacitor":
                return (1);
            default:
                return (1);
                break;
        }
    }
}
