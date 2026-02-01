using System;
using Control;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    [RequireComponent(typeof(CharacterSelectionManager))]
    public class CharacterSelectionInputBindings : BaseInputBindings
    {
        private CharacterSelectionManager _selectionManager;
        private void Awake()
        {
            _selectionManager = GetComponent<CharacterSelectionManager>();

            var start = controls.FindActionMap("UI").FindAction("Start");
            var select = controls.FindActionMap("UI").FindAction("Select");

            SetHandlers(new()
            {
                (start, StartGame),
                (select, GoBack)
            });
        }

        private void StartGame(InputAction.CallbackContext context)
        {
            Debug.Log("Trying to start game...");
            if(context.performed)
                _selectionManager.StartGame();
        }

        private void GoBack(InputAction.CallbackContext context)
        {
            _selectionManager.GoBack();
        }

        private void Start()
        {
            Init();
        }

    }
}
