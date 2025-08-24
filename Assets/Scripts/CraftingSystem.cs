using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour {

    public static CraftingSystem Instance { get; set; }
    [SerializeField] GameObject craftingScreenUI;
    [SerializeField] GameObject toolsScreenUI;
    public bool isOpen;
    public List<string> inventoryItemList = new List<string>();

    //Category Buttons
    Button toolsBtn;

    //Craft Button
    Button craftAxeBtn;

    //Required Items Text
    TextMeshProUGUI AxeReq1, AxeReq2;

    //All BluePrints
    public ItemBlueprint AxeBlueprint = new ItemBlueprint("Axe", 2, "Stone", 3, "Stick", 2);


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        isOpen = false;
        toolsBtn = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBtn.onClick.AddListener(delegate { OpenToolsCategory(); });

        //Axe
        AxeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        AxeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<TextMeshProUGUI>();

        craftAxeBtn = toolsScreenUI.transform.Find("Axe").transform.Find("Craft").GetComponent<Button>();
        craftAxeBtn.onClick.AddListener(delegate { CraftAnyItem(AxeBlueprint); });
    }

    void CraftAnyItem(ItemBlueprint blueprintToCraft) {
        InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);

        if (blueprintToCraft.numOfRequirements == 1) {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.req1, blueprintToCraft.req1Amount);
        } else if (blueprintToCraft.numOfRequirements == 2) {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.req1, blueprintToCraft.req1Amount);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.req2, blueprintToCraft.req2Amount);
        }

        StartCoroutine(calculate());
    }


    public IEnumerator calculate() {
        yield return 0;
        InventorySystem.Instance.ReCalculateList();
        RefreshNeededItems();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.C) && !isOpen) {

            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;

        } else if (Input.GetKeyDown(KeyCode.C) && isOpen) {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);

            if (!InventorySystem.Instance.isOpen) {
                Cursor.lockState = CursorLockMode.Locked;
            }
            isOpen = false;
        }
    }

    private void OpenToolsCategory() {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }

    public void RefreshNeededItems() {
        int stone_count = 0;
        int stick_count = 0;
        inventoryItemList = InventorySystem.Instance.itemList;

        foreach (string itemName in inventoryItemList) {
            switch (itemName) {
                case "Stone":
                    stone_count += 1;
                    break;
                case "Stick":
                    stick_count += 1;
                    break;
            }
        }

        // ----- Axe -----
        AxeReq1.text = "3 Stone [" + stone_count + "]";
        AxeReq2.text = "2 Stick [" + stick_count + "]";

        // Show/hide the button based on resources
        if (stone_count >= 3 && stick_count >= 2) {
            craftAxeBtn.gameObject.SetActive(true);
        } else {
            craftAxeBtn.gameObject.SetActive(false);
        }
    }

}
