using System;
using UnityEngine.InputSystem;

namespace Input
{
    public static class InputActionExtensions
    {
        public static void AddHandler(this InputAction action, Action<InputAction.CallbackContext> handler)
        {
            action.performed += handler;
            action.canceled += handler;
            action.started += handler;
        }

        public static void RemoveHandler(this InputAction action, Action<InputAction.CallbackContext> handler)
        {
            action.performed -= handler;
            action.canceled -= handler;
            action.started -= handler;
        }
    }
}
