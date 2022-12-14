using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    private Controls controls;
    public bool isWalking;

    public Vector2 MovementValue { get; private set; }
    public Vector2 LookValue { get; private set; }
    public bool IsSprinting { get; private set; }
    public bool jump { get; private set; }

    public event Action InteractEvent;
    public event Action TorchEvent;
    public event Action CameraEvent;
    public event Action CrouchEvent;
    public event Action ProneEvent;

    private float timer;
    private bool timerStarted;

    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();

    }

    private void Update()
    {
        if (timerStarted)
        {
            timer += Time.deltaTime;
        }
    }

    public void OnMove1(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
        //print(MovementValue);
        if (MovementValue != new Vector2(0, 0))
        {

            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed) IsSprinting = true;
        else if (context.canceled) IsSprinting = false;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookValue = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) jump = true;
        else jump = false;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed) InteractEvent?.Invoke();

    }

    public void OnFlashlight(InputAction.CallbackContext context)
    {
        //Debug.Log("Flashflight Pressed");
        if (context.performed)
        {
            TorchEvent?.Invoke();
        }
    }

    public void OnCamera(InputAction.CallbackContext context)
    {
        if (context.performed) CameraEvent?.Invoke(); Debug.Log("Let there be light");
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if(context.performed) CrouchEvent?.Invoke();

    /*    if (context.performed)
        {
            timer = 0;
            timerStarted = true;
        }
        else if (context.canceled)
        {
            timerStarted = false;
            if (timer < 1f)
            {
                CrouchEvent?.Invoke();
            }
            else
            {
                // prone event;
                ProneEvent?.Invoke();
            }
        }*/
    }

    public void OnProne(InputAction.CallbackContext context)
    {
        if(context.performed) ProneEvent?.Invoke(); 
    }
}
