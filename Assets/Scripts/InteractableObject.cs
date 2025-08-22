using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {
    [SerializeField] string ItemName;

    public string GetItemName() {
        return ItemName;
    }
}