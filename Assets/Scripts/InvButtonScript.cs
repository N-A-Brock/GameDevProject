using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InvButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InventorySlot attatchedSlot;
    public TextMeshProUGUI descName;
    public TextMeshProUGUI descBody;

    // Start is called before the first frame update
    void Start()
    {
        descName = GameObject.Find("ItemTitle (TMP)").GetComponent<TextMeshProUGUI>();
        descBody = GameObject.Find("ItemDescription (TMP)").GetComponent<TextMeshProUGUI>();
    }

    public void DisplayClick()
    {
        attatchedSlot.slotUseButton.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void UseClick()
    {
        attatchedSlot.itemObj.UseItem(attatchedSlot.itemObj.useSelector);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if ((this.name == "ButtonDisplay") && !(attatchedSlot.itemObj.itemType == ItemObject.ItemType.BLANK))
        {
            descName.text = attatchedSlot.itemObj.itemName;
            descBody.text = attatchedSlot.itemObj.itemDesc;
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.name == "ButtonUse")
        {
            attatchedSlot.slotDisplayButton.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
