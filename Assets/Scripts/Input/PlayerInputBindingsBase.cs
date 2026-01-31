using MPlayer;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    [RequireComponent(typeof(Player))]
    public class PlayerInputBindingsBase : BaseInputBindings
    {
        #region Internal State

        private Player _player;

        #endregion

        private void Awake()
        {
            _player = GetComponent<Player>();
            AwakeInit();
        }

        protected virtual void AwakeInit() {}

        protected bool IsMine(InputAction.CallbackContext context) => InputManager.IsMine(context, _player.playerId);
    }
}
