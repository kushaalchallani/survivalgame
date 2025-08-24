using System;
using System.Collections.Generic;
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
    Text AxeReq1, AxeReq2;

    //All BluePrints


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
        AxeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<Text>();
        AxeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<Text>();

        craftAxeBtn = toolsScreenUI.transform.Find("Axe").transform.Find("Craft").GetComponent<Button>();
        craftAxeBtn.onClick.AddListener(delegate { CraftAnyItem(); });
    }

    void CraftAnyItem() {
        throw new NotImplementedException();
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
}
