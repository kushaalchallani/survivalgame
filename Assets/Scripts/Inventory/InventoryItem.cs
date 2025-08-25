using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {


    // --- Item Info UI --- //
    private GameObject itemInfoUI;

    private TextMeshProUGUI itemInfoUI_itemName;
    private TextMeshProUGUI itemInfoUI_itemDescription;
    private TextMeshProUGUI itemInfoUI_itemFunctionality;
    private Image itemInfoUI_itemImage;

    [SerializeField] string itemName, itemDescription, itemFunctionality;



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




}