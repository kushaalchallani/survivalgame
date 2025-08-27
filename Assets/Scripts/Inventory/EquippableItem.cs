using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EquippableItem : MonoBehaviour {
    [SerializeField] Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && InventorySystem.Instance.isOpen == false && CraftingSystem.Instance.isOpen == false && SelectionManager.instance.handIsVisible == false) {
            animator.SetTrigger("hit");
        }
    }
}
