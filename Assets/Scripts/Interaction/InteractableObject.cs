using UnityEngine;

public class InteractableObject : MonoBehaviour {

    public bool playerInRange;
    [SerializeField] string ItemName;

    public string GetItemName() {
        return ItemName;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            playerInRange = true;
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && SelectionManager.instance.onTarget && SelectionManager.instance.selectedObject == gameObject) {
            //if the inventory system is not full we add the item to the inventory and destroy it from the world
            if (!InventorySystem.Instance.CheckIfFull()) {
                InventorySystem.Instance.AddToInventory(ItemName);
                Destroy(gameObject);
            } else {
                Debug.Log("Inventory Full");
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            playerInRange = false;
        }
    }
}