using System.Collections.Generic;
using Input;
using MPlayer;
using Scenes.WorldGenerator;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Control
{
    public class MatchManager : MonoBehaviour
    {
        #region Inspector Properties

        [Header("General")]
        [SerializeField] private Tilemap ground;

        [Header("Dragon")]
        [SerializeField] private List<Transform> dragonSpawners;
        [SerializeField] private List<Transform> dragonItemsSpawners;
        [SerializeField] private GameObject dragonItemPrefab;

        [SerializeField] private Player dragonPrefab;
        [SerializeField] private Tilemap dragonCollision;
        [Min(0)]
        [SerializeField] private int nDragonItems = 5;

        [Header("Players")]
        [Min(0)]
        [SerializeField] private int maxCandles;
        [SerializeField] private List<PlayerUI> playersUIs;
        [SerializeField] private Masks masks;

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

            // Setup Players
            SetupPlayers();
        }

        private void OnEnable()
        {
            var channel = EventsChannel.Get();
            if (!channel) return;

            channel.OnCandleLit += OnCandleLit;
            channel.OnPlayerDied += OnPlayerDied;
            channel.OnDragonRageFinished += SetupDragonItems;
        }

        private void OnDisable()
        {
            var channel = EventsChannel.Get();
            if (!channel) return;

            channel.OnCandleLit -= OnCandleLit;
            channel.OnPlayerDied -= OnPlayerDied;
            channel.OnDragonRageFinished -= SetupDragonItems;
        }

        private void OnPlayerDied(Player obj)
        {
            _currentPlayers--;
            if (_currentPlayers == 1)
                DragonWins();
        }

        private void OnCandleLit()
        {
            _currentCandles++;
            if (_currentCandles == maxCandles && !_candlesRitualComplete)
            {
                _candlesRitualComplete = true;
                EventsChannel.RitualComplete(Ritual.Candles);
                PlayersWin();
            }

            CheckPlayersWin();
        }

        private void PlayersWin()
        {
            EventsChannel.PlayersWin();
            SceneManager.LoadScene("PlayersWin");
        }

        private void DragonWins()
        {
            EventsChannel.DragonWins();
            SceneManager.LoadScene("DragonWins");
        }

        private void CheckPlayersWin()
        {
            if (_candlesRitualComplete)
                EventsChannel.PlayersWin();
        }

        private void SetupDragon()
        {
            // Setup dragon player
            var spawner = dragonSpawners[Random.Range(0, dragonSpawners.Count)];
            var dragon = Instantiate(dragonPrefab);
            dragon.transform.position = spawner.transform.position;
            dragon.type = PlayerType.Dragon;
            dragon.playerId = InputManager.DragonPlayer;
            dragon.Movement.collisionTilemap = dragonCollision;
            dragon.Movement.groundTilemap = ground;

            SetupDragonItems();
        }

        private void SetupDragonItems()
        {
            // Chose different spawners positions
            HashSet<int> used = new();
            while (used.Count < nDragonItems)
            {
                var chose = Random.Range(0, dragonItemsSpawners.Count);
                if (used.Contains(chose))
                    continue;

                used.Add(chose);
            }

            foreach (var spawnerIdx in used)
            {
                var item = Instantiate(dragonItemPrefab);
                item.transform.position = dragonItemsSpawners[spawnerIdx].position;
            }
        }

        private void SetupPlayers()
        {
            foreach (var (playerId, data) in InputManager.PlayerToData)
            {

            }

            SetupPlayerUIs();
        }

        private void SetupPlayerUIs()
        {
            int nextUi = 0;
            foreach (var (id, data) in InputManager.PlayerToData)
            {
                if (data.IsDragon)
                    continue;

                var ui = playersUIs[nextUi];
                ui.playerId = id;
                ui.Mask = masks.masks[data.MaskIndex];
                ui.Inventory = null;
                ui.name = $"P{id}";
                nextUi++;
            }

            for (int i = nextUi; i < playersUIs.Count; i++)
            {
                playersUIs[i].gameObject.SetActive(false);
            }
        }
    }

    public enum Ritual
    {
        Candles,
        Dust,
        Coins
    }
}
