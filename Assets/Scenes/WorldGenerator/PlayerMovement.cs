using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace Scenes.WorldGenerator
{
    public class PlayerMovement : BaseInputBindings
    {
        public int playerId;
        public Tilemap groundTilemap;
        public Tilemap collisionTilemap;


        private void Awake()
        {
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
            if (!IsMine(context))
                return;

            Vector2 direction = context.ReadValue<Vector2>();
            Move(direction);
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
            InputManager.IsMine(context, playerId);
        
       
    }
}
