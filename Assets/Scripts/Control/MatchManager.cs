using Input;
using MPlayer;
using UnityEngine;

namespace Control
{
    public class MatchManager : MonoBehaviour
    {
        #region Inspector Properties

        [Min(0)]
        [SerializeField] private int maxCandles;

        #endregion

        #region Internal State

        private MatchManager _instance;
        private int _currentCandles;
        private int _currentPlayers;
        private bool _candlesRitualComplete;

        #endregion

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            if (!_instance)
                Destroy(gameObject);
        }

        private void Start()
        {
            _currentPlayers = InputManager.CurrentPlayers;
        }

        private void OnEnable()
        {
            var channel = EventsChannel.Get();
            if (!channel) return;

            channel.OnCandleLit += OnCandleLit;
            channel.OnPlayerDied += OnPlayerDied;
        }

        private void OnDisable()
        {
            var channel = EventsChannel.Get();
            if (!channel) return;

            channel.OnCandleLit -= OnCandleLit;
            channel.OnPlayerDied -= OnPlayerDied;
        }

        private void OnPlayerDied(Player obj)
        {
            _currentPlayers--;
            if (_currentPlayers == 0)
                DragonWins();
        }

        private void OnCandleLit()
        {
            _currentCandles++;
            if (_currentCandles == maxCandles && !_candlesRitualComplete)
            {
                _candlesRitualComplete = true;
                EventsChannel.RitualComplete(Ritual.Candles);
                // TODO
            }

            CheckPlayersWin();
        }

        private void PlayerDied(Player player)
        {
            _currentPlayers--;
            if (_currentCandles == 0)
                DragonWins();
        }

        private void PlayersWin()
        {
            EventsChannel.PlayersWin();
            // TODO
        }

        private void DragonWins()
        {
            EventsChannel.DragonWins();
            // TODO
        }

        private void CheckPlayersWin()
        {
            if (_candlesRitualComplete)
                EventsChannel.PlayersWin();
        }
    }

    public enum Ritual
    {
        Candles,
        Dust,
        Coins
    }
}
