using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HydrationBar : MonoBehaviour {
    [SerializeField] TextMeshProUGUI hydrationCounter;
    [SerializeField] GameObject playerState;
    private float currentHydration, maxHydration;
    private Slider slider;
    void Awake() {
        slider = GetComponent<Slider>();
    }

    void Update() {
        currentHydration = playerState.GetComponent<PlayerState>().currentHydrationPercent;
        maxHydration = playerState.GetComponent<PlayerState>().maxHydrationPercent;

        float fillValue = currentHydration / maxHydration;
        slider.value = fillValue;
        hydrationCounter.text = $"{currentHydration}%";
    }
}
