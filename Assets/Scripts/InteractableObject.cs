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
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && SelectionManager.instance.onTarget) {
            Debug.Log("Interacted with " + ItemName);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            playerInRange = false;
        }
    }
}