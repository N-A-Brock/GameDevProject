using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;


public class InventoryController : MonoBehaviour
{
    public Button[] toolButtons;
    public Button[] consumableButtons;
    public Button[] otherButtons;

    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;

    public int number;
    InventorySlot[] consumableSlots;
    InventorySlot[] otherSlots;

    InventorySlot toolSlotA;
    InventorySlot toolSlotB;
    InventorySlot lightSlotA;
    InventorySlot lightSlotB;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public void CollectItem(ItemObject collectedObject)
    {
        bool itemFound = false;
        foreach (InventorySlot slotToCheck in CheckItemType(collectedObject))
        {
            if (slotToCheck.itemObj == collectedObject)
            {
                slotToCheck.quantity++;
                itemFound = true;
                Debug.Log("Added to existing stack");
                break;
            }
        }

        if (!itemFound)
        {
            foreach (InventorySlot slotToCheck in CheckItemType(collectedObject))
            {
                if (slotToCheck.itemObj.itemType == ItemObject.ItemType.BLANK)
                {
                    slotToCheck.itemObj = collectedObject;
                }
            }
        }
    }


    public InventorySlot[] CheckItemType (ItemObject collectedObject)
    {
        switch (collectedObject.itemType)
        {
            case ItemObject.ItemType.EQUIPABLE:
                return (new InventorySlot[4] { toolSlotA, toolSlotB, lightSlotA, lightSlotB });
            case ItemObject.ItemType.CONSUMABLE:
                return (consumableSlots);
            case ItemObject.ItemType.NORMAL:
                return (otherSlots);
            default:
                Debug.LogError("How in the fuck did this happen");
                return null;
        }
    }
}

public class InventorySlot
{
    public int quantity;
    public ItemObject itemObj;
    public Button slotButton;
    
}
