using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private PlayerInput playerInput;
    private Rigidbody2D rb;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.localScale.x > 0 && playerInput.MovementVector.x < 0)
            Rotate();
        else if (transform.localScale.x < 0 && playerInput.MovementVector.x > 0)
            Rotate();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + playerInput.MovementVector * speed * Time.fixedDeltaTime);
    }

    private void Rotate()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }
}
