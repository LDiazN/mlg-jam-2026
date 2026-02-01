using System.Collections.Generic;
using Input;
using MPlayer;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Control
{
    public class MatchManager : MonoBehaviour
    {
        #region Inspector Properties

        [SerializeField] private List<Transform> dragonSpawners;
        [Min(0)]
        [SerializeField] private int maxCandles;
        [SerializeField] private Player dragonPrefab;
        [SerializeField] private Tilemap dragonCollision;
        [SerializeField] private Tilemap ground;

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
            _currentPlayers = InputManager.TotalPlayers;

            // Setup match now: init players and dragon
            SetupDragon();
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

        private void SetupDragon()
        {
            var spawner = dragonSpawners[Random.Range(0, dragonSpawners.Count)];
            var dragon = Instantiate(dragonPrefab);
            dragon.transform.position = spawner.transform.position;
            dragon.type = PlayerType.Dragon;
            dragon.playerId = InputManager.DragonPlayer;
            dragon.Movement.collisionTilemap = dragonCollision;
            dragon.Movement.groundTilemap = ground;
        }
    }

    public enum Ritual
    {
        Candles,
        Dust,
        Coins
    }
}
