using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider healthSlider;
    public TMP_Text healthBarText;
    Damageable playerDamageable;
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            Debug.Log("No Player found in the scene. Make sure it has tag 'Player'");
        playerDamageable = player.GetComponent<Damageable>();
        
    }
    private void Start()
    {
        healthSlider.value = CalculateSliderPrecentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = "HP " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }
    private void OnEnable()
    {
        playerDamageable.healthChange.AddListener(OnPlayerHealthChange);
    }
    private void OnDisable()
    {
        playerDamageable.healthChange.RemoveListener(OnPlayerHealthChange);
    }

    private void OnPlayerHealthChange(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPrecentage(newHealth, maxHealth);
        healthBarText.text = "HP " + newHealth + " / " + maxHealth;
    }

    private float CalculateSliderPrecentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

}
