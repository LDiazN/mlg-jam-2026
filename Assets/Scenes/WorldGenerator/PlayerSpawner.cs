using Control;
using Input;
using Input;
using MPlayer;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scenes.WorldGenerator
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject[] playerSpawnPositions;

        public Tilemap groundTilemap;
        public Tilemap collisionTilemap;

        private void Start()
        {
            int playerCount = 0;
            foreach (var kvp in InputManager.PlayerToController)
            {
                int playerId = kvp.Key;

                GameObject obj = Instantiate(
                    playerPrefab,
                    playerSpawnPositions[playerCount].transform.position,
                    Quaternion.identity
                );

                Player player = obj.GetComponent<Player>();

                player.playerId = playerId;
                player.Movement.collisionTilemap = collisionTilemap;
                player.Movement.groundTilemap = groundTilemap;

                playerCount++;
                obj.SetActive(true);
            }
        }
    }

}
