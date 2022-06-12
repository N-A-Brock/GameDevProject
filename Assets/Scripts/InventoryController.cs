using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryController : MonoBehaviour
{
    public Canvas uiCanvas;
    public UIController uiScript;

    public int number;

    public ItemObject blankObject;

    InventorySlot[] toolSlots; //tool, tool, light, light
    InventorySlot[] consumableSlots;
    InventorySlot[] otherSlots;

    InventorySlot toolSlotA, toolSlotB, lightSlotA, lightSlotB;
    InventorySlot consumableSlotA, consumableSlotB, consumableSlotC;
    InventorySlot otherSlotA, otherSlotB, otherSlotC, otherSlotD, otherSlotE, otherSlotF, otherSlotG, otherSlotH;
    
    // Start is called before the first frame update
    void Start()
    {
        uiScript = uiCanvas.GetComponent<UIController>();
        ConstructSlots();
    }

    void ConstructSlots()
    {
        InventorySlot toolSlotA = new InventorySlot(0, 0, blankObject, uiScript.toolDisplayButtons[0], uiScript.toolUseButtons[0]);
        InventorySlot toolSlotB = new InventorySlot(0, 0, blankObject, uiScript.toolDisplayButtons[1], uiScript.toolUseButtons[1]);
        InventorySlot lightSlotA = new InventorySlot(0, 0, blankObject, uiScript.toolDisplayButtons[2], uiScript.toolUseButtons[2]);
        InventorySlot lightSlotB = new InventorySlot(0, 0, blankObject, uiScript.toolDisplayButtons[3], uiScript.toolUseButtons[3]);
        this.toolSlots = new InventorySlot[] { toolSlotA, toolSlotB, lightSlotA, lightSlotB };

        InventorySlot consumableSlotA = new InventorySlot(0, null, blankObject, uiScript.consumableDisplayButtons[0], uiScript.consumableUseButtons[0]);
        InventorySlot consumableSlotB = new InventorySlot(0, null, blankObject, uiScript.consumableDisplayButtons[1], uiScript.consumableUseButtons[1]);
        InventorySlot consumableSlotC = new InventorySlot(0, null, blankObject, uiScript.consumableDisplayButtons[2], uiScript.consumableUseButtons[2]);
        this.consumableSlots = new InventorySlot[] { consumableSlotA, consumableSlotB, consumableSlotC };

        InventorySlot otherSlotA = new InventorySlot(0, null, blankObject, uiScript.otherDisplayButtons[0], uiScript.otherUseButtons[0]);
        InventorySlot otherSlotB = new InventorySlot(0, null, blankObject, uiScript.otherDisplayButtons[0], uiScript.otherUseButtons[0]);
        InventorySlot otherSlotC = new InventorySlot(0, null, blankObject, uiScript.otherDisplayButtons[0], uiScript.otherUseButtons[0]);
        InventorySlot otherSlotD = new InventorySlot(0, null, blankObject, uiScript.otherDisplayButtons[0], uiScript.otherUseButtons[0]);
        InventorySlot otherSlotE = new InventorySlot(0, null, blankObject, uiScript.otherDisplayButtons[0], uiScript.otherUseButtons[0]);
        InventorySlot otherSlotF = new InventorySlot(0, null, blankObject, uiScript.otherDisplayButtons[0], uiScript.otherUseButtons[0]);
        InventorySlot otherSlotG = new InventorySlot(0, null, blankObject, uiScript.otherDisplayButtons[0], uiScript.otherUseButtons[0]);
        InventorySlot otherSlotH = new InventorySlot(0, null, blankObject, uiScript.otherDisplayButtons[0], uiScript.otherUseButtons[0]);
        this.otherSlots = new InventorySlot[] { otherSlotA, otherSlotB, otherSlotC, otherSlotD, otherSlotE, otherSlotF, otherSlotG, otherSlotH };
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CollectItem(ItemObject collectedObject)
    {
        //This boolean determines if the foreach loop finds an item in an existing slot.
        //If true, the if statement following the foreach statement will not run.
        //If false, the if statement will delete a blank slot, or determine that all slots are already used.
        bool itemFound = false;

        //Determines what category (Tool, Consumable, Other) the collected object belongs to.
        //Then checks each inventory slot of that type to see if the collected object already exists in the inventory
        foreach (InventorySlot slotToCheck in CheckItemType(collectedObject)) 
        {
            if (slotToCheck.itemObj == collectedObject)
            {
                if (slotToCheck.quantity < collectedObject.maxAmountHeld)
                {
                    slotToCheck.quantity++;
                    UpdateInventory(slotToCheck);
                    Debug.Log("Item added to existing slot");
                    itemFound = true;
                    break;
                }
                else
                {
                    Debug.Log("You cannot hold more of this item");
                }
            }
        }
        //This boolean determines whether the 3rd if statement runs.
        bool blankFound = false;

        //Checks relevant slots for empty slot, if found, add collected object.
        //If no empty slot found, return message stating so.
        if (!itemFound) 
        {
            foreach (InventorySlot slotToCheck in CheckItemType(collectedObject))
            {
                if (slotToCheck.itemObj.itemType == ItemObject.ItemType.BLANK)
                {
                    slotToCheck.itemObj = collectedObject;
                    slotToCheck.quantity = 1;
                    UpdateInventory(slotToCheck);
                    blankFound = true;
                    Debug.Log("Item added to empty slot");
                    break;
                }
            }
        }

        //Checks if the collected object has not been found in the inventory, and that a blank slot is unavailable.
        //If so, conveys that the inventory is full.
        if (!itemFound && !blankFound)
        {
            Debug.Log("Inventory Full");
        }
    }


    public InventorySlot[] CheckItemType (ItemObject collectedObject) //Returns the correct slots in an array based on the collected object's item type
    {
        switch (collectedObject.itemType)
        {
            case ItemObject.ItemType.EQUIPABLE:
                return (toolSlots);
            case ItemObject.ItemType.CONSUMABLE:
                return (consumableSlots);
            case ItemObject.ItemType.OTHER:
                return (otherSlots);
            default:
                Debug.LogError("How in the fuck did this happen");
                return null;
        }
    }


    public void UpdateInventory(InventorySlot slotToUpdate)
    {
        slotToUpdate.slotDisplayButton.image.sprite = slotToUpdate.itemObj.itemSprite;//variables being assigned are null
        slotToUpdate.slotDisplayText.text = slotToUpdate.quantity.ToString();

    }


    public void UpdateInternalQuantity(InventorySlot slotToUpdate)
    {
        slotToUpdate.slotDisplayButton.transform.Find("IntText").GetComponent<TextMeshProUGUI>().text = (slotToUpdate.internalAmount + "/" + slotToUpdate.itemObj.maxAmountHeld);
    }


    public void UseItem(InventorySlot slotToUpdate)
    {
        switch (slotToUpdate.itemObj.itemType)
        {
            case ItemObject.ItemType.OTHER:
                Debug.Log("Eventual animation reference i guess");
                break;

            case ItemObject.ItemType.CONSUMABLE:
                Debug.Log("Consumable used");
                foreach (InventorySlot slot in toolSlots)
                {
                    if (slot.itemObj = slotToUpdate.itemObj.useTarget)
                    {
                        //slot.internalAmount = 
                    }
                }
                break;

            case ItemObject.ItemType.EQUIPABLE:
                break;
            case ItemObject.ItemType.BLANK:
                break;
            default:
                break;
        }
    }
}

public class InventorySlot
{

    public int internalAmount; //For items that can hold some quantity of item inside itself, this value denotes the numerator of that fraction (denominator found in itemObject.
    public int quantity;
    public ItemObject itemObj;
    public Button slotDisplayButton;
    public Button slotUseButton;
    public TextMeshProUGUI slotDisplayText;
    public TextMeshProUGUI slotUseText;


    public InventorySlot(int quan, int? intamn, ItemObject defaultItemObj, Button slotDisButt, Button slotUseButt)
    {
        itemObj = defaultItemObj;
        quantity = quan;
        slotDisplayButton = slotDisButt;
        slotUseButton = slotUseButt;
        slotDisplayText = slotDisButt.GetComponentInChildren<TextMeshProUGUI>();
        slotUseText = slotUseButt.GetComponentInChildren<TextMeshProUGUI>();
        AssignToButtonScript();
        switch (itemObj.itemType) //This doesnt work
        {
            case ItemObject.ItemType.OTHER:
                slotUseText.text = "Hold";
                break;
            case ItemObject.ItemType.CONSUMABLE:
                slotUseText.text = "Use";
                break;
            case ItemObject.ItemType.EQUIPABLE:
                slotUseText.text = "Equip";
                break;
            case ItemObject.ItemType.BLANK:
                break;
            default:
                break;
        }
    }

    public void AssignToButtonScript()
    {
        slotDisplayButton.GetComponent<InvButtonScript>().attatchedSlot = this;
        slotUseButton.GetComponent<InvButtonScript>().attatchedSlot = this;
        
    }
}
