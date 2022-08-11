using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    private Controls controls;
    public Vector2 MovementValue { get; private set; }
    public Vector2 LookValue { get; private set; }
    public bool IsSprinting { get; private set; }
    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }
    public void OnMove1(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed) IsSprinting  = true;
        else if (context.canceled) IsSprinting = false;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookValue = context.ReadValue<Vector2>();
    }
}
