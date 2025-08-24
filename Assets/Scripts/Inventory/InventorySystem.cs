using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour {

    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;
    private List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();
    private GameObject itemToAdd;
    private GameObject whatSlotToEquip;
    public bool isOpen;


    //Pickup Alert
    [SerializeField] GameObject pickupAlert;
    [SerializeField] TextMeshProUGUI pickupName;
    [SerializeField] Image pickupImage;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }


    void Start() {
        isOpen = false;
        PopulateSlotList();
    }


    void Update() {

        if (Input.GetKeyDown(KeyCode.Tab) && !isOpen) {

            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;

        } else if (Input.GetKeyDown(KeyCode.Tab) && isOpen) {
            inventoryScreenUI.SetActive(false);

            if (!CraftingSystem.Instance.isOpen) {
                Cursor.lockState = CursorLockMode.Locked;
            }
            isOpen = false;
        }
    }

    private void PopulateSlotList() {
        foreach (Transform child in inventoryScreenUI.transform) {
            if (child.CompareTag("Slot")) {
                slotList.Add(child.gameObject);
            }
        }
    }
    public void AddToInventory(string itemName) {

        whatSlotToEquip = FindNextEmptySlot();
        itemToAdd = Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);

        itemList.Add(itemName);

        TriggerPickupPopup(itemName, itemToAdd.GetComponent<Image>().sprite);

        ReCalculateList();
        CraftingSystem.Instance.RefreshNeededItems();
    }

    public bool CheckIfFull() {
        int counter = 0;

        foreach (GameObject slot in slotList) {
            if (slot.transform.childCount > 0) {
                counter += 1;
            }
        }

        if (counter == slotList.Count) {
            return true;
        } else {
            return false;
        }
    }

    private GameObject FindNextEmptySlot() {
        foreach (GameObject slot in slotList) {
            if (slot.transform.childCount == 0) {
                return slot;
            }
        }
        return new GameObject();
    }

    public void RemoveItem(string nameToRemove, int amountToRemove) {
        int counter = amountToRemove;
        for (var i = slotList.Count - 1; i >= 0; i--) {
            if (slotList[i].transform.childCount > 0) {
                if (slotList[i].transform.GetChild(0).name == nameToRemove + "(Clone)" && counter != 0) {
                    Destroy(slotList[i].transform.GetChild(0).gameObject);
                    counter--;
                }
            }
        }

        ReCalculateList();
        CraftingSystem.Instance.RefreshNeededItems();
    }

    public void ReCalculateList() {
        itemList.Clear();
        foreach (GameObject slot in slotList) {
            if (slot.transform.childCount > 0) {
                itemList.Add(slot.transform.GetChild(0).name.Replace("(Clone)", ""));
            }
        }
    }

    void TriggerPickupPopup(string itemName, Sprite itemSprite) {
        pickupAlert.SetActive(true);
        pickupName.text = itemName;
        pickupImage.sprite = itemSprite;
    }
}