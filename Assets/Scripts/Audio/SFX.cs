using Control;
using MPlayer;
using UnityEngine;

namespace Audio
{
    public class SFX : MonoBehaviour
    {
        [SerializeField] private AudioClip candleLit;
        [SerializeField] private AudioClip playerDied;
        [SerializeField] private AudioClip dragonRage;

        private void OnEnable()
        {
            var channel = EventsChannel.Get();
            if (!channel)
                return;

            channel.OnCandleLit += OnCandleLit;
            channel.OnPlayerDied += OnPlayerDied;
            channel.OnDragonRageStarted += OnDragonRageStarted;
        }


        private void OnDisable()
        {
            var channel = EventsChannel.Get();
            if (!channel)
                return;

            channel.OnCandleLit -= OnCandleLit;
            channel.OnPlayerDied -= OnPlayerDied;
            channel.OnDragonRageStarted -= OnDragonRageStarted;
        }

        private void OnCandleLit() => AudioManager.Play(candleLit);

        private void OnPlayerDied(Player obj) => AudioManager.Play(playerDied);

        private void OnDragonRageStarted() => AudioManager.Play(dragonRage);
    }
}
