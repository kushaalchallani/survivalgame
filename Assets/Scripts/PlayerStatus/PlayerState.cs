using UnityEngine;

public class PlayerState : MonoBehaviour {
    public static PlayerState Instance { get; set; }

    // ---- Player Health ----
    public float currentHealth;
    public float maxHealth;


    // ---- Player Calories----  
    public float currentCalories;
    public float maxCalories;


    // ---- Player Hydartion----  
    public float currentHydrationPercent;
    public float maxHydrationPercent;


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    void Start() {
        currentHealth = maxHealth;
    }

    void Update() {
        //Testing health decrease
        if (Input.GetKeyDown(KeyCode.N)) {
            currentHealth -= 10;
        }
    }
}
