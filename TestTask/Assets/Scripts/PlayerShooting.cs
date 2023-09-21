using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float visibilityArea;
    [SerializeField] private GameObject[] guns;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        playerInput.OnShootButtonClick += PlayerInput_OnShootButtonClick;
    }

    private void PlayerInput_OnShootButtonClick()
    {
        foreach (var enemy in EnemySpawner.Instance.Enemies)
        {
            if(Vector2.Distance(transform.position, enemy.transform.position) < visibilityArea)
            {
                Fire();

                break;
            }
        }
    }

    private void Fire()
    {
        foreach (var gun in guns)
        {
            if (gun.activeInHierarchy && BulletsUI.Instance.Bullets > 0)
            {
                BulletsUI.Instance.Shoot();

                float rotationZ = transform.localScale.x < 0 ? 180f : 0f;
                Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.Euler(Vector3.forward * rotationZ));

                break;
            }
        }
    }
}
