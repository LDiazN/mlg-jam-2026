using Input;
using MPlayer;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace Scenes.WorldGenerator
{
    [RequireComponent(typeof(Player))]
    public class PlayerMovement : BaseInputBindings
    {
        private Player player;
        public Tilemap groundTilemap;
        public Tilemap collisionTilemap;
        private Vector2 _lastDirection;


        private void Awake()
        {
            player = GetComponent<Player>();

            var move = controls.FindActionMap("Player").FindAction("Move");
            SetHandlers(new()
            {
                (move, CheckMovementOwnership)
            });
        }

        private void Start()
        {
            Init();
        }


        private void CheckMovementOwnership(
            InputAction.CallbackContext context)
        {
            if (!context.performed || !IsMine(context))
                return;

            Vector2 direction = context.ReadValue<Vector2>();
            // Edge-detect: step once when transitioning from idle -> input
            bool wasIdle = _lastDirection == Vector2.zero;
            _lastDirection = direction;
            if (wasIdle && direction != Vector2.zero)
            {
                Move(direction);
            }
        }

        private void Move(
            Vector2 direction)
        {
            if (CanMove(direction))
            {
                transform.position += (Vector3)direction;
            }
        }

        protected virtual bool CanMove(
            Vector2 direction)
        {
            Vector3Int position =
                groundTilemap.WorldToCell(transform.position + (Vector3)direction);

            bool hasGroundTile =
                groundTilemap.HasTile(position);
            bool hasCollision =
                collisionTilemap.HasTile(position);

            return hasGroundTile && !hasCollision;
        }

        private bool IsMine(
            InputAction.CallbackContext context) =>
            InputManager.IsMine(context, player.playerId);
    }
}
