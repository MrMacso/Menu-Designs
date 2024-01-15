using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public static PlayerInput PlayerInputComponent { get; set; }

    public bool MenuOpenCloseInput { get; private set; }
    bool IsControlEnabled;
    public Vector2 NavigationInput { get; set; }

    InputAction _navigationAction;
    InputAction _menuOpenCloseAction;

    void Awake()
    {
        if(Instance == null)
        Instance = this;

        PlayerInputComponent= GetComponent<PlayerInput>();
        _navigationAction = PlayerInputComponent.actions["Navigate"];
        _menuOpenCloseAction = PlayerInputComponent.actions["MenuOpenClose"];
        DisableControl();
    }

    void Update()
    {
        if (IsControlEnabled)
        { 
            NavigationInput = _navigationAction.ReadValue<Vector2>();

            MenuOpenCloseInput = _menuOpenCloseAction.WasPressedThisFrame();
        }
    }
    public void EnableControl()
    {
        PlayerInputComponent.actions.FindAction("Navigate").Enable();
        IsControlEnabled = true;
    }
    public void DisableControl()
    {
        PlayerInputComponent.actions.FindAction("Navigate").Disable();
        IsControlEnabled = false;
    }
}
