using Control;
using Input;
using MPlayer;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scenes.WorldGenerator
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
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
            var obj = Instantiate(prefab);
            var player = obj.GetComponent<Player>();

            player.playerId = playerid;
            player.Movement.collisionTilemap = collisionTilemap;
            player.Movement.groundTilemap = groundTilemap;
        }
    }
}
