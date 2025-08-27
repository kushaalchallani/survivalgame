using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EquippableItem : MonoBehaviour {
    [SerializeField] Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            animator.SetTrigger("hit");
        }
    }
}
