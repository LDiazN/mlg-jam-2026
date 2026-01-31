using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.Input
{
    public class TestInput : BaseInputBindings
    {
        public int playerId;

        private void Awake()
        {
            var jump = controls.FindActionMap("Player").FindAction("Jump");
            var submit = controls.FindActionMap("UI").FindAction("Submit");
            SetHandlers(new ()
            {
                (jump, Hello),
                (submit, Hello)
            });
        }

        private void Start()
        {
            Init();
        }

        private void Hello(InputAction.CallbackContext context)
        {
            if (!IsMine(context))
                return;

            var id = context.control.device.deviceId;
            if (context.performed)
                Debug.Log($"I'm player {playerId} with device {id}");
        }

        private bool IsMine(InputAction.CallbackContext context) => InputManager.IsMine(context, playerId);
    }
}
