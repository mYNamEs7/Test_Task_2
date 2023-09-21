using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float visibilityArea;
    [SerializeField] private float cooldown;

    private PlayerHealth player;
    private NavMeshAgent agent;
    private bool canAttack;
    private float cooldownTimer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerHealth>();
    }

    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        if (player == null) return;

        Cooldown();

        if (Vector2.Distance(transform.position, player.transform.position) < 2f && canAttack)
        {
            Attack();
            canAttack = false;
        }

        if (Vector2.Distance(transform.position, player.transform.position) < visibilityArea)
            MoveAndRotate();
    }

    private void MoveAndRotate()
    {
        agent.SetDestination(player.transform.position);

        if (transform.position.x > player.transform.position.x && transform.localScale.x > 0f)
            Rotate();
        else if (transform.position.x < player.transform.position.x && transform.localScale.x < 0f)
            Rotate();
    }

    private void Attack()
    {
        player.TakeDamage();
    }

    private void Cooldown()
    {
        if (cooldownTimer <= 0f)
        {
            canAttack = true;
            cooldownTimer = cooldown;
        }
        else
        {
            if (!canAttack)
                cooldownTimer -= Time.deltaTime;
            else if (cooldownTimer != cooldown)
                cooldownTimer = cooldown;
        }
    }

    private void Rotate()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }
}
