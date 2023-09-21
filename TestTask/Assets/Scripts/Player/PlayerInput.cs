using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public event Action OnShootButtonClick;

    [SerializeField] private Joystick joystick;
    [SerializeField] private Button shootButton;

    private void Awake()
    {
        shootButton.onClick.AddListener(() =>
        {
            OnShootButtonClick?.Invoke();
        });
    }

    public Vector2 MovementVector
    {
        get
        {
            Vector2 inputVector = new(joystick.Horizontal, joystick.Vertical);
            return inputVector.normalized;
        }
    }
}
