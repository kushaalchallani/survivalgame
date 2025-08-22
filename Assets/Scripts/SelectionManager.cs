using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour {
    [SerializeField] GameObject interaction_Info_UI;
    TextMeshProUGUI interaction_text;

    private void Start() {
        interaction_text = interaction_Info_UI.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            var selectionTransform = hit.transform;

            if (selectionTransform.GetComponent<InteractableObject>()) {
                interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                interaction_Info_UI.SetActive(true);
            } else {
                interaction_Info_UI.SetActive(false);
            }
        } else {
            interaction_Info_UI.SetActive(false);
        }
    }
}