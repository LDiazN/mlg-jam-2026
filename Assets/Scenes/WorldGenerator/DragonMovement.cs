using UnityEngine;

namespace Scenes.WorldGenerator
{

    public class DragonMovement : PlayerMovement
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected override bool CanMove(
            Vector2 direction)
        {
            Vector3Int position =
                groundTilemap.WorldToCell(transform.position + (Vector3)direction);

            bool hasGroundTile =
                groundTilemap.HasTile(position);

            return hasGroundTile;
        }
    }
}