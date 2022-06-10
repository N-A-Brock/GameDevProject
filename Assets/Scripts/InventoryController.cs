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
        Debug.Log("This is where the fun begins");


        InventorySlot toolSlotA = new InventorySlot(0, 0, blankObject, uiScript.toolAssemblies[0].transform.Find("ButtonDisplay").GetComponent<Button>(), uiScript.toolAssemblies[0].transform.Find("ButtonDisplay").GetComponent<Button>(), uiScript.toolAssemblies[0].GetComponentInChildren<TextMeshProUGUI>());
        InventorySlot toolSlotB = new InventorySlot(0, 0, blankObject, uiScript.toolAssemblies[1].transform.Find("ButtonDisplay").GetComponent<Button>(), uiScript.toolAssemblies[0].transform.Find("ButtonUse").GetComponent<Button>(), uiScript.toolAssemblies[1].GetComponentInChildren<TextMeshProUGUI>());
        InventorySlot lightSlotA = new InventorySlot(0, 0, blankObject, uiScript.toolAssemblies[2].transform.Find("ButtonDisplay").GetComponent<Button>(), uiScript.toolAssemblies[0].transform.Find("ButtonUse").GetComponent<Button>(), uiScript.toolAssemblies[2].GetComponentInChildren<TextMeshProUGUI>());
        InventorySlot lightSlotB = new InventorySlot(0, 0, blankObject, uiScript.toolAssemblies[3].transform.Find("ButtonDisplay").GetComponent<Button>(), uiScript.toolAssemblies[0].transform.Find("ButtonUse").GetComponent<Button>(), uiScript.toolAssemblies[3].GetComponentInChildren<TextMeshProUGUI>());
        this.toolSlots = new InventorySlot[] { toolSlotA, toolSlotB, lightSlotA, lightSlotB };

        InventorySlot consumableSlotA = new InventorySlot(0, null, blankObject, uiScript.consumableAssemblies[0].transform.Find("ButtonDisplay").GetComponent<Button>(), uiScript.consumableAssemblies[0].transform.Find("ButtonUse").GetComponent<Button>(), uiScript.consumableAssemblies[0].GetComponentInChildren<TextMeshProUGUI>());
        InventorySlot consumableSlotB = new InventorySlot(0, null, blankObject, uiScript.consumableAssemblies[1].transform.Find("ButtonDisplay").GetComponent<Button>(), uiScript.consumableAssemblies[0].transform.Find("ButtonUse").GetComponent<Button>(), uiScript.consumableAssemblies[1].GetComponentInChildren<TextMeshProUGUI>());
        InventorySlot consumableSlotC = new InventorySlot(0, null, blankObject, uiScript.consumableAssemblies[2].transform.Find("ButtonDisplay").GetComponent<Button>(), uiScript.consumableAssemblies[0].transform.Find("ButtonUse").GetComponent<Button>(), uiScript.consumableAssemblies[2].GetComponentInChildren<TextMeshProUGUI>());
        this.consumableSlots = new InventorySlot[] { consumableSlotA, consumableSlotB, consumableSlotC };

        InventorySlot otherSlotA = new InventorySlot(0, null, blankObject, uiScript.otherAssemblies[0].transform.Find("ButtonDisplay").GetComponent<Button>(), uiScript.otherAssemblies[0].transform.Find("ButtonUse").GetComponent<Button>(), uiScript.otherAssemblies[0].GetComponentInChildren<TextMeshProUGUI>());
        InventorySlot otherSlotB = new InventorySlot(0, null, blankObject, uiScript.otherAssemblies[1].transform.Find("ButtonDisplay").GetComponent<Button>(), uiScript.otherAssemblies[0].transform.Find("ButtonUse").GetComponent<Button>(), uiScript.otherAssemblies[1].GetComponentInChildren<TextMeshProUGUI>());
        InventorySlot otherSlotC = new InventorySlot(0, null, blankObject, uiScript.otherAssemblies[2].transform.Find("ButtonDisplay").GetComponent<Button>(), uiScript.otherAssemblies[0].transform.Find("ButtonUse").GetComponent<Button>(), uiScript.otherAssemblies[2].GetComponentInChildren<TextMeshProUGUI>());
        InventorySlot otherSlotD = new InventorySlot(0, null, blankObject, uiScript.otherAssemblies[3].transform.Find("ButtonDisplay").GetComponent<Button>(), uiScript.otherAssemblies[0].transform.Find("ButtonUse").GetComponent<Button>(), uiScript.otherAssemblies[3].GetComponentInChildren<TextMeshProUGUI>());
        InventorySlot otherSlotE = new InventorySlot(0, null, blankObject, uiScript.otherAssemblies[4].transform.Find("ButtonDisplay").GetComponent<Button>(), uiScript.otherAssemblies[0].transform.Find("ButtonUse").GetComponent<Button>(), uiScript.otherAssemblies[0].GetComponentInChildren<TextMeshProUGUI>());
        InventorySlot otherSlotF = new InventorySlot(0, null, blankObject, uiScript.otherAssemblies[5].transform.Find("ButtonDisplay").GetComponent<Button>(), uiScript.otherAssemblies[0].transform.Find("ButtonUse").GetComponent<Button>(), uiScript.otherAssemblies[1].GetComponentInChildren<TextMeshProUGUI>());
        InventorySlot otherSlotG = new InventorySlot(0, null, blankObject, uiScript.otherAssemblies[6].transform.Find("ButtonDisplay").GetComponent<Button>(), uiScript.otherAssemblies[0].transform.Find("ButtonUse").GetComponent<Button>(), uiScript.otherAssemblies[2].GetComponentInChildren<TextMeshProUGUI>());
        InventorySlot otherSlotH = new InventorySlot(0, null, blankObject, uiScript.otherAssemblies[7].transform.Find("ButtonDisplay").GetComponent<Button>(), uiScript.otherAssemblies[0].transform.Find("ButtonUse").GetComponent<Button>(), uiScript.otherAssemblies[3].GetComponentInChildren<TextMeshProUGUI>());
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
        slotToUpdate.slotText.text = slotToUpdate.quantity.ToString();

    }
}

public class InventorySlot
{
    public InventorySlot(int quan, int? intamn, ItemObject defaultItemObj, Button slotDisButt, Button slotUseButt, TextMeshProUGUI slotTxt)
    {
        itemObj = defaultItemObj;
        quantity = quan;
        slotDisplayButton = slotDisButt;
        slotUseButton = slotUseButt;
        slotText = slotTxt;

        AssignToButtonScript();

    }

    public void AssignToButtonScript()
    {
        slotDisplayButton.GetComponent<InvButtonScript>().attatchedSlot = this;
    }

    public int internalAmount;
    public int quantity;
    public ItemObject itemObj;
    public Button slotDisplayButton;
    public Button slotUseButton;
    public TextMeshProUGUI slotText;
    
}
