using System;
using System.Collections;
using UnityEngine;

public class PlayerState : MonoBehaviour {
    public static PlayerState Instance { get; set; }

    // ---- Player Health ----
    public float currentHealth;
    public float maxHealth;


    // ---- Player Calories----  
    public float currentCalories;
    public float maxCalories;
    float distanceTraveled = 0;
    Vector3 lastPosition;
    public GameObject playerBody;



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
        currentCalories = maxCalories;
        currentHydrationPercent = maxHydrationPercent;

        StartCoroutine(depleteHydration());
    }

    IEnumerator depleteHydration() {
        while (true) {
            yield return new WaitForSeconds(10);
            currentHydrationPercent -= 1;
        }
    }

    void Update() {
        //Calories decrease based on distance traveled
        distanceTraveled += Vector3.Distance(playerBody.transform.position, lastPosition);
        lastPosition = playerBody.transform.position;

        if (distanceTraveled >= 10) {
            distanceTraveled = 0;
            currentCalories -= 1;
        }

        //Testing health decrease
        if (Input.GetKeyDown(KeyCode.N)) {
            currentHealth -= 10;
        }
    }

    public void setHealth(float newHealth) {
        currentHealth = newHealth;
    }

    public void setCalories(float newCalories) {
        currentCalories = newCalories;
    }

    public void setHydration(float newHydration) {
        currentHydrationPercent = newHydration;
    }
}
