using NavMeshPlus.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    public event Action OnAllEnemiesDeath;
    public List<Enemy> Enemies { get; private set; } = new List<Enemy>();

    [SerializeField] private List<Enemy> enemyPrefabList;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector2 position = new(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-4f, 4f));
            var enemy = Instantiate(enemyPrefabList[UnityEngine.Random.Range(0, enemyPrefabList.Count)], position, Quaternion.identity);
            Enemies.Add(enemy);
        }

        EnemyHealth.OnEnemyDeath += EnemyHealth_OnEnemyDeath;
    }

    private void EnemyHealth_OnEnemyDeath(Enemy enemy)
    {
        Enemies.Remove(enemy);

        if (Enemies.Count == 0)
        {
            OnAllEnemiesDeath?.Invoke();
            Time.timeScale = 0f;
        }
    }

    private void OnDestroy()
    {
        EnemyHealth.OnEnemyDeath -= EnemyHealth_OnEnemyDeath;
    }
}
