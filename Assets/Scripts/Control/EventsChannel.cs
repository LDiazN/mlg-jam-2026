using System;
using MPlayer;
using UnityEngine;

namespace Control
{
    /// Usen Esta clase para anotar todos los eventos importantes del juego
    /// Perder salud, ganar puntos, cosas asi. Vean el ejemplo.
    ///
    /// Para usarlo en otro objetos haces:
    ///
    /// private void OnEnable()
    /// {
    ///     var channel = EventsChannel.Get();
    ///     if (channel)
    ///        channel.OnExampleEvent += MyFunction;
    /// }
    public class EventsChannel : MonoBehaviour
    {
        #region Internal State

        private static EventsChannel _instance;

        #endregion

        #region Events

        public event Action OnExampleEvent;

        // Args: PlayerID, DeviceId
        public event Action<int,int> OnPlayerJoined;

        // Args: Player ID
        public event Action<int> OnPlayerLeft;

        public event Action OnCandleLit;

        public event Action<Ritual> OnRitualComplete;

        public event Action OnDragonWins;

        public event Action OnPlayersWin;

        public event Action<Player> OnPlayerDied;

        public event Action<Item, Item> OnPlayerItemUpdate;

        public event Action<float> OnDragonChargeChanged;

        public event Action OnDragonRageStarted;

        public event Action OnDragonRageFinished;

        #endregion

        private void Awake()
        {
            if (!_instance)
                _instance = this;

            if (_instance != this)
                Destroy(gameObject);
        }

        public static EventsChannel Get() => _instance;

        public static void ExampleEvent() => _instance?.OnExampleEvent?.Invoke();

        public static void PlayerJoined(int playerId, int controllerId) => _instance?.OnPlayerJoined?.Invoke(playerId, controllerId);

        public static void PlayerLeft(int playerId) => _instance?.OnPlayerLeft?.Invoke(playerId);

        public static void CandleLit() => _instance?.OnCandleLit?.Invoke();

        public static void RitualComplete(Ritual ritual) => _instance?.OnRitualComplete?.Invoke(ritual);

        public static void DragonWins() => _instance?.OnDragonWins?.Invoke();

        public static void PlayersWin() => _instance?.OnPlayersWin?.Invoke();

        public static void PlayerDied(Player player) => _instance?.OnPlayerDied?.Invoke(player);

        public static void PlayerItemUpdate(Player player, Item oldItem, Item newItem) => _instance?.OnPlayerItemUpdate?.Invoke(oldItem, newItem);

        // Dragon
        // new charge goes from 0 to 1
        public static void DragonChargeChanged(float newCharge) => _instance?.OnDragonChargeChanged?.Invoke(newCharge);

        public static void DragonRageStarted() => _instance?.OnDragonRageStarted?.Invoke();

        public static void DragonRageFinished() => _instance?.OnDragonRageFinished?.Invoke();
    }
}
