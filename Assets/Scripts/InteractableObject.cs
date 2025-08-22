using UnityEngine;

public class InteractableObject : MonoBehaviour {
    [SerializeField] string ItemName;

    public string GetItemName() {
        return ItemName;
    }
}