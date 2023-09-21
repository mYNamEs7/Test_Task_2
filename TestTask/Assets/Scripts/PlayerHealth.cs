using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnPlayerDeath;
    public static PlayerHealth Instance { get; private set; }

    [SerializeField] private Slider healthBar;
    [SerializeField] private float damage = 20f;

    private void Awake()
    {
        Time.timeScale = 1f;

        Instance = this;
        healthBar.maxValue = 100;
        healthBar.value = GameData.Instance.health;
    }

    public void TakeDamage()
    {
        healthBar.value -= damage;

        if (healthBar.value <= 0f)
        {
            OnPlayerDeath?.Invoke();
            Destroy(gameObject);
            Time.timeScale = 0f;
        }
    }

    public float GetHeath() => healthBar.value;
}
