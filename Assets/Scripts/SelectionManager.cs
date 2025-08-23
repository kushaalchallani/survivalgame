using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour {
    [SerializeField] GameObject interaction_Info_UI;
    TextMeshProUGUI interaction_text;

    private void Start() {
        interaction_text = interaction_Info_UI.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update() {
        if (Camera.main == null) return;

        Vector3 mousePos = Input.mousePosition;
        if (mousePos.x < 0 || mousePos.x > Screen.width || mousePos.y < 0 || mousePos.y > Screen.height) {
            interaction_Info_UI.SetActive(false);
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            var selectionTransform = hit.transform;

            if (selectionTransform.GetComponent<InteractableObject>() && selectionTransform.GetComponent<InteractableObject>().playerInRange) {
                interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                interaction_Info_UI.SetActive(true);
            } else { // if not looking at an interactable object 
                interaction_Info_UI.SetActive(false);
            }
        } else { // if not looking at anything
            interaction_Info_UI.SetActive(false);
        }
    }
}