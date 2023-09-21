using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform hitEffectPrefab;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Vector2 position = transform.position;
        position.x -= 0.27f;
        Instantiate(hitEffectPrefab, position, Quaternion.identity);

        if (collision.collider.TryGetComponent(out EnemyHealth enemy))
            enemy.TakeDamage();
    }
}
