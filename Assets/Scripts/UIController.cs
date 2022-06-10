using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{


    public GameObject inventoryPanel;
    public GameObject pausePanel;
    public GameObject hudPanel;

    public GameObject[] toolAssemblies;
    public GameObject[] consumableAssemblies;
    public GameObject[] otherAssemblies;



    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;

    public GameObject toolTipParent;
    public TextMeshProUGUI toolTipText;
    public Image toolTipReticle;


    public bool inventoryEnabled;
    public bool pauseEnabled;




    private void OnEnable()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleInventory()
    {
        Debug.Log(inventoryEnabled);
        if (inventoryEnabled)
        {
            inventoryEnabled = false;
            Debug.Log("Set False");
            inventoryPanel.SetActive(false);

        }
        else
        {
            inventoryEnabled = true;
            Debug.Log("Set True");
            inventoryPanel.SetActive(true);
        }
        
    }

    public void TogglePause()
    {
        if (pauseEnabled)
        {
            pauseEnabled = false;
            pausePanel.SetActive(false);
        }
        else if (!pauseEnabled)
        {
            pauseEnabled = true;
            pausePanel.SetActive(true);
        }
    }
}
