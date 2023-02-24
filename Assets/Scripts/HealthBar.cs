using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    Damageable playerDamageable;
    public GameObject thisObject;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamageable = player.GetComponent<Damageable>();
    }

    private void Start()
    {
        healthBar.maxValue = playerDamageable.MaxHealth ;
        healthBar.value = playerDamageable.Health;
    }
    


    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerHealChange(int health, int maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }
}
