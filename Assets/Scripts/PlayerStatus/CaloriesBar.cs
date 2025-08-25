using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CaloriesBar : MonoBehaviour {
    [SerializeField] TextMeshProUGUI caloriesCounter;
    [SerializeField] GameObject playerState;
    private float currentCalories, maxCalories;
    private Slider slider;
    void Awake() {
        slider = GetComponent<Slider>();
    }

    void Update() {
        currentCalories = playerState.GetComponent<PlayerState>().currentCalories;
        maxCalories = playerState.GetComponent<PlayerState>().maxCalories;

        float fillValue = currentCalories / maxCalories;
        slider.value = fillValue;
        caloriesCounter.text = $"{currentCalories}/{maxCalories}";
    }
}
