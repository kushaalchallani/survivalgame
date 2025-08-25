using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour {
    [SerializeField] TextMeshProUGUI healthCounter;
    [SerializeField] GameObject playerState;
    private float currentHealth, maxHealth;
    private Slider slider;
    void Awake() {
        slider = GetComponent<Slider>();
    }

    void Update() {
        currentHealth = playerState.GetComponent<PlayerState>().currentHealth;
        maxHealth = playerState.GetComponent<PlayerState>().maxHealth;

        float fillValue = currentHealth / maxHealth;
        slider.value = fillValue;
        healthCounter.text = $"{currentHealth}/{maxHealth}";
    }
}
