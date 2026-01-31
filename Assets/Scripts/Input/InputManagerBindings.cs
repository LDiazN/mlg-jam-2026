using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    [RequireComponent(typeof(InputManager))]
    public class InputManagerBindings : BaseInputBindings
    {
        #region Inspector Properties

        [SerializeField] private bool collectPlayers;

        #endregion

        #region Internal State

        private InputManager manager;

        #endregion

        private void Awake()
        {
            manager = GetComponent<InputManager>();
            var submit = controls.FindActionMap("UI").FindAction("Submit");
            var cancel = controls.FindActionMap("UI").FindAction("Cancel");
            var jump = controls.FindActionMap("Player").FindAction("Jump");

            SetHandlers(new()
            {
                (submit, HandleAny),
                (cancel, HandleAny),
                (jump, HandleAny)
            });
        }

        private void Start()
        {
            Init();
        }

        private void HandleAny(InputAction.CallbackContext context)
        {
            if (!collectPlayers)
                return;

            bool found = false;
            foreach (var (player, device) in InputManager.PlayerToController)
            {
                if (device == context.control.device.deviceId)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                InputManager.AddPlayer(context.control.device.deviceId);
                Debug.Log($"Adding device: {context.control.device.deviceId} ({context.control.device.name})");
            }
        }
    }
}
