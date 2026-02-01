using Control;
using Input;
using UnityEngine;

namespace Scenes.Input
{
    public class InputSpawner : MonoBehaviour
    {
        [SerializeField] private TestInput prefab;

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

        private void OnPlayerJoined(int playerid, InputManager.PlayerData data)
        {
            var obj = Instantiate(prefab);
            obj.playerId = playerid;
        }
    }
}
