using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipSystem : MonoBehaviour {
    public static EquipSystem Instance { get; set; }

    // -- UI -- //
    [SerializeField] GameObject quickSlotsPanel;

    [SerializeField] GameObject numbersHolder;
    [SerializeField] GameObject toolHolder;
    private List<GameObject> quickSlotsList = new List<GameObject>();
    private GameObject selectedItem;
    private GameObject selectedItemModel;

    private int selectedNumber = -1;


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }


    private void Start() {
        PopulateSlotList();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SelectQuickSlot(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SelectQuickSlot(2);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SelectQuickSlot(3);
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            SelectQuickSlot(4);
        } else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            SelectQuickSlot(5);
        } else if (Input.GetKeyDown(KeyCode.Alpha6)) {
            SelectQuickSlot(6);
        } else if (Input.GetKeyDown(KeyCode.Alpha7)) {
            SelectQuickSlot(7);
        }
    }

    private void PopulateSlotList() {
        foreach (Transform child in quickSlotsPanel.transform) {
            if (child.CompareTag("QuickSlot")) {
                quickSlotsList.Add(child.gameObject);
            }
        }
    }

    public void AddToQuickSlots(GameObject itemToEquip) {
        // Find next free slot
        GameObject availableSlot = FindNextEmptySlot();
        // Set transform of our object
        itemToEquip.transform.SetParent(availableSlot.transform, false);

        InventorySystem.Instance.ReCalculateList();

    }


    private GameObject FindNextEmptySlot() {
        foreach (GameObject slot in quickSlotsList) {
            if (slot.transform.childCount == 0) {
                return slot;
            }
        }
        return new GameObject();
    }

    public bool CheckIfFull() {

        int counter = 0;

        foreach (GameObject slot in quickSlotsList) {
            if (slot.transform.childCount > 0) {
                counter += 1;
            }
        }

        if (counter == 7) {
            return true;
        } else {
            return false;
        }
    }
    private void SelectQuickSlot(int number) {
        if (CheckIfSlotIsFull(number) == true) {

            if (selectedNumber != number) {
                selectedNumber = number;

                //Unselect the previously selected item
                if (selectedItem != null) {
                    selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;
                }

                selectedItem = getSelectedItem(number);
                selectedItem.GetComponent<InventoryItem>().isSelected = true;

                SetEquipedModel(selectedItem);

                //Changing the color
                foreach (Transform child in numbersHolder.transform) {
                    child.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = Color.gray;
                }

                TextMeshProUGUI toBeChanged = numbersHolder.transform.Find("number" + number).transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
                toBeChanged.color = Color.white;
            } else {

                //selecting the same slot i.e deselecting
                selectedNumber = -1;

                //Unselect the previously selected item
                if (selectedItem != null) {
                    selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;
                    selectedItem = null;
                }

                if (selectedItemModel != null) {
                    DestroyImmediate(selectedItemModel.gameObject);
                    selectedItemModel = null;
                }

                //Changing the color
                foreach (Transform child in numbersHolder.transform) {
                    child.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().color = Color.gray;
                }
            }
        }
    }

    private bool CheckIfSlotIsFull(int slotNumber) {
        if (quickSlotsList[slotNumber - 1].transform.childCount > 0) {
            return true;
        } else {
            return false;
        }
    }
    private GameObject getSelectedItem(int slotNumber) {
        return quickSlotsList[slotNumber - 1].transform.GetChild(0).gameObject;
    }

    private void SetEquipedModel(GameObject selectedItem) {

        if (selectedItemModel != null) {
            DestroyImmediate(selectedItemModel.gameObject);
            selectedItemModel = null;
        }


        string selectedItemName = selectedItem.name.Replace("(Clone)", "");
        selectedItemModel = Instantiate(Resources.Load<GameObject>(selectedItemName + "_Model"), new Vector3(0.6f, 0.5f, 0.9f), Quaternion.Euler(90, -15, 75));
        selectedItemModel.transform.SetParent(toolHolder.transform, false);
    }
}
