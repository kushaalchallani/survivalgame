using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {

    // --- Is this item trashable --- //
    public bool isTrashable;

    // --- Item Info UI --- //
    private GameObject itemInfoUI;

    private TextMeshProUGUI itemInfoUI_itemName;
    private TextMeshProUGUI itemInfoUI_itemDescription;
    private TextMeshProUGUI itemInfoUI_itemFunctionality;
    private Image itemInfoUI_itemImage;

    [SerializeField] string itemName, itemDescription, itemFunctionality;

    // --- Consumption --- //
    private GameObject itemPendingConsumption;
    [SerializeField] bool isConsumable;

    [SerializeField] float healthEffect;
    [SerializeField] float caloriesEffect;
    [SerializeField] float hydrationEffect;



    private void Start() {
        itemInfoUI = InventorySystem.Instance.ItemInfoUI;
        itemInfoUI_itemName = itemInfoUI.transform.Find("itemName").GetComponent<TextMeshProUGUI>();
        itemInfoUI_itemDescription = itemInfoUI.transform.Find("itemDescription").GetComponent<TextMeshProUGUI>();
        itemInfoUI_itemFunctionality = itemInfoUI.transform.Find("itemFunctionality").GetComponent<TextMeshProUGUI>();
        itemInfoUI_itemImage = itemInfoUI.transform.Find("itemIcon").GetComponent<Image>();
    }

    // Triggered when the mouse enters into the area of the item that has this script.
    public void OnPointerEnter(PointerEventData eventData) {
        itemInfoUI.SetActive(true);
        itemInfoUI_itemName.text = itemName;
        itemInfoUI_itemDescription.text = itemDescription;
        itemInfoUI_itemFunctionality.text = itemFunctionality;
        itemInfoUI_itemImage.sprite = gameObject.GetComponent<Image>().sprite;
    }

    public void OnPointerExit(PointerEventData eventData) {
        itemInfoUI.SetActive(false);
    }

    // Triggered when the mouse is clicked over the item that has this script.
    public void OnPointerDown(PointerEventData eventData) {
        //Right Mouse Button Click on
        if (eventData.button == PointerEventData.InputButton.Right) {
            if (isConsumable) {
                // Setting this specific gameobject to be the item we want to destroy later
                itemPendingConsumption = gameObject;
                consumingFunction(healthEffect, caloriesEffect, hydrationEffect);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Right) {
            if (isConsumable && itemPendingConsumption == gameObject) {
                DestroyImmediate(gameObject);
                InventorySystem.Instance.ReCalculateList();
                CraftingSystem.Instance.RefreshNeededItems();
            }
        }
    }

    private void consumingFunction(float healthEffect, float caloriesEffect, float hydrationEffect) {
        itemInfoUI.SetActive(false);

        healthEffectCalculation(healthEffect);

        caloriesEffectCalculation(caloriesEffect);

        hydrationEffectCalculation(hydrationEffect);

    }

    private static void healthEffectCalculation(float healthEffect) {
        // --- Health --- //

        float healthBeforeConsumption = PlayerState.Instance.currentHealth;
        float maxHealth = PlayerState.Instance.maxHealth;

        if (healthEffect != 0) {
            if ((healthBeforeConsumption + healthEffect) > maxHealth) {
                PlayerState.Instance.setHealth(maxHealth);
            } else {
                PlayerState.Instance.setHealth(healthBeforeConsumption + healthEffect);
            }
        }
    }


    private static void caloriesEffectCalculation(float caloriesEffect) {
        // --- Calories --- //

        float caloriesBeforeConsumption = PlayerState.Instance.currentCalories;
        float maxCalories = PlayerState.Instance.maxCalories;

        if (caloriesEffect != 0) {
            if ((caloriesBeforeConsumption + caloriesEffect) > maxCalories) {
                PlayerState.Instance.setCalories(maxCalories);
            } else {
                PlayerState.Instance.setCalories(caloriesBeforeConsumption + caloriesEffect);
            }
        }
    }


    private static void hydrationEffectCalculation(float hydrationEffect) {
        // --- Hydration --- //

        float hydrationBeforeConsumption = PlayerState.Instance.currentHydrationPercent;
        float maxHydration = PlayerState.Instance.maxHydrationPercent;

        if (hydrationEffect != 0) {
            if ((hydrationBeforeConsumption + hydrationEffect) > maxHydration) {
                PlayerState.Instance.setHydration(maxHydration);
            } else {
                PlayerState.Instance.setHydration(hydrationBeforeConsumption + hydrationEffect);
            }
        }
    }




}