using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public Vector2 NavigationInput { get; set; }

    InputAction _navigationAction;

    public static PlayerInput PlayerInput { get; set; }

    void Awake()
    {
        if(Instance == null)
        Instance = this;

        PlayerInput= GetComponent<PlayerInput>();
        _navigationAction = PlayerInput.actions["Navigate"];
    }

    void Update()
    {
        NavigationInput = _navigationAction.ReadValue<Vector2>();
    }
}
