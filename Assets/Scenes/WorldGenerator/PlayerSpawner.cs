using Control;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scenes.WorldGenerator
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        public Tilemap groundTilemap;
        public Tilemap collisionTilemap;
        public float cooldownTime = 1f;

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
            int deviceid)
        {
            var obj = Instantiate(prefab);
            var player = obj.GetComponent<PlayerMovement>();
            
            player.playerId = playerid;
            player.collisionTilemap = collisionTilemap;
            player.groundTilemap = groundTilemap;
            player.cooldownTime = cooldownTime;
        }
    }
}
