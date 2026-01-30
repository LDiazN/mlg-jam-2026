using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.Input
{
    public class TestInput : BaseInputBindings
    {
        [SerializeField] private int playerId;

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
            var id = context.control.device.deviceId;
            if (context.performed)
                Debug.Log($"I'm player {playerId} with device {id}");
        }
    }
}
