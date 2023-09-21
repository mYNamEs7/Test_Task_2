using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyHealth : MonoBehaviour
{
    public static event Action<Enemy> OnEnemyDeath;

    [Header("Health")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float damage = 20f;

    [Header("Items")]
    [SerializeField] private List<Item> itemList = new();

    private void Awake()
    {
        healthBar.value = healthBar.maxValue = maxHealth;
    }

    public void TakeDamage()
    {
        healthBar.value -= damage;

        if (healthBar.value <= 0f)
        {
            OnEnemyDeath?.Invoke(GetComponent<Enemy>());
            Instantiate(itemList[UnityEngine.Random.Range(0, itemList.Count)], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
