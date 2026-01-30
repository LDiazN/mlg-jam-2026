using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    [RequireComponent(typeof(InputManager))]
    public class InputManagerBindings : BaseInputBindings
    {
        #region Internal State

        private InputManager manager;

        #endregion

        private void Awake()
        {
            manager = GetComponent<InputManager>();
            var submit = controls.FindActionMap("UI").FindAction("Submit");
            var cancel = controls.FindActionMap("UI").FindAction("Cancel");

            SetHandlers(new()
            {
                (submit, HandleAny),
                (cancel, HandleAny)
            });
        }

        private void Start()
        {
            Init();
        }

        private void HandleAny(InputAction.CallbackContext context)
        {
            bool found = false;
            foreach (var (player, device) in manager.PlayerToController)
            {
                if (device == context.control.device.deviceId)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
                manager.AddPlayer(context.control.device.deviceId);
        }
    }
}
