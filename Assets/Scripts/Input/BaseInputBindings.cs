using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    /// <summary>
    /// This class implements input modes and bindings.
    ///
    /// Inherit this class and create all the bindings
    /// you specifically need in that subclass
    /// </summary>
    public abstract class BaseInputBindings : MonoBehaviour
    {
        #region Inspector Properties

        [Tooltip("Input configurations currently in use")] [SerializeField]
        protected InputActionAsset controls;

        [Tooltip("If input should start active for this component")]
        [SerializeField] private bool inputStartsActive = true;

        #endregion

        #region Internal State

        private List<(InputAction, Action<InputAction.CallbackContext>)> handlers;

        private bool inputActive;

        public bool InputActive
        {
            get => inputActive;
            set => SetInputActive(value);
        }

        #endregion

        private void OnDestroy()
        {
            // Handlers are not automatically removed on teardown because the actions are stored in the action map,
            // which is a scriptable object and has a different lifetime to normal game objects.
            foreach(var (action, handler) in handlers)
                action.RemoveHandler(handler);
            handlers.Clear();
        }

        protected void SetHandlers(List<(InputAction, Action<InputAction.CallbackContext>)> newHandlers)
        {
            if (handlers != null)
                foreach (var (action, handler) in handlers)
                    action.RemoveHandler(handler);

            handlers = newHandlers;
        }

        private void SetInputActive(bool isActive)
        {
            inputActive = isActive;
            foreach (var (action, handler) in handlers)
            {
                if (isActive)
                    action.AddHandler(handler);
                else
                    action.RemoveHandler(handler);
            }
        }

        protected void Init()
        {
            SetInputActive(inputStartsActive);
        }
    }

}
