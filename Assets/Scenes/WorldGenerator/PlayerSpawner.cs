using Control;
using Input;
using MPlayer;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scenes.WorldGenerator
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject dragonPrefab;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject[] playerSpawnPositions;
        [SerializeField] private GameObject dragonSpawnPosition;

        private bool hasDragonSpawned = false;
        private int playerCount = 0;

        public Tilemap groundTilemap;
        public Tilemap collisionTilemap;

        private void OnEnable()
        {
            var channel = EventsChannel.Get();
            channel.OnPlayerJoined += OnPlayerJoined;
        }

        private void OnDisable()
        {
            var channel = EventsChannel.Get();
            channel.OnPlayerJoined -= OnPlayerJoined;
        }

        private void OnPlayerJoined(
            int playerid,
            InputManager.PlayerData data)
        {
            if (dragonPrefab == null || playerPrefab == null)
            {
                Debug.LogError("Prefabs are not assigned.");
                return;
            }

            if (collisionTilemap == null || groundTilemap == null)
            {
                Debug.LogError("Tilemaps are not assigned.");
                return;
            }

            GameObject obj;

            if (!hasDragonSpawned)
            {
                if (dragonSpawnPosition == null)
                {
                    Debug.LogError("Dragon spawn position is not assigned.");
                    return;
                }

                obj = Instantiate(
                    dragonPrefab,
                    dragonSpawnPosition.transform.position,
                    Quaternion.identity
                );

                hasDragonSpawned = true;
            }
            else
            {
                if (playerSpawnPositions == null || playerSpawnPositions.Length <= playerCount)
                {
                    Debug.LogError("Player spawn positions are not properly assigned or out of bounds.");
                    return;
                }

                obj = Instantiate(
                    playerPrefab,
                    playerSpawnPositions[playerCount].transform.position,
                    Quaternion.identity
                );

                playerCount++;
            }

            Player player = obj.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("Player component is missing on the instantiated object.");
                return;
            }

            player.playerId = playerid;
            player.Movement.collisionTilemap = collisionTilemap;
            player.Movement.groundTilemap = groundTilemap;
            obj.SetActive(true);
        }
    }
}
