using System;
using System.Collections.Generic;
using Control;
using MPlayer;
using QFSW.QC;
using UnityEngine.InputSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Input
{
    /// <summary>
    /// Most input is handled locally from the player prefab.
    ///
    /// This module handles things like active controller scheme (gamepad or keyboard) and rising events
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        #region Inspector Properties

        [SerializeField] private InputActionAsset controls;
        [Tooltip("Default input mode used by the player. " +
                 "UI = controlling ui (character won't move). " +
                 "Player = controlling character, UI is unresponsive")]
        [SerializeField] private InputMode defaultMode = InputMode.Player;
        [SerializeField] private bool clearControllersOnDestroy;
        [Range(2,4)]
        [SerializeField] private int maxPlayers = 4;
        [SerializeField] private Masks masks;

        public int MaxPlayers => maxPlayers;

        #endregion

        #region Internal State

        private InputActionMap _playerActionMap;
        private InputActionMap _uiActionMap;
        private static InputManager _instance;
        private string _currentControlScheme = "Keyboard&Mouse";
        private InputMode _currentMode;
        public static string CurrentControlScheme => _instance?._currentControlScheme;

        // Player ID to device id playerid to controllerid
        public struct PlayerData
        {
            public int ControllerId;
            public int MaskIndex;
            public bool IsDragon;
        }

        private static readonly Dictionary<int, PlayerData> _playerToData = new();
        public static Dictionary<int, PlayerData> PlayerToData => _playerToData;

        private static int _playerCounter = 1;
        public static int TotalPlayers => _playerToData.Count;
        public static int DragonPlayer = -1;

        #endregion

        private void Awake()
        {
            if (!_instance)
                _instance = this;
            if (_instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _playerActionMap = controls.FindActionMap("Player");
            _uiActionMap = controls.FindActionMap("UI");
        }

        private void Start()
        {
            SetInputModeInternal(defaultMode);
        }

        private void OnEnable()
        {
            _playerActionMap.actionTriggered += OnActionTriggered;
            _uiActionMap.actionTriggered += OnActionTriggered;
        }

        private void OnDisable()
        {
            _playerActionMap.actionTriggered -= OnActionTriggered;
            _uiActionMap.actionTriggered -= OnActionTriggered;
        }

        private void OnDestroy()
        {
            if (clearControllersOnDestroy)
                _playerToData.Clear();
            _playerCounter = 0;
        }

        public static void SetInputMode(InputMode newMode) => _instance?.SetInputModeInternal(newMode);

        public static InputMode GetInputMode() => _instance?._currentMode ?? InputMode.Player;

        private void OnActionTriggered(InputAction.CallbackContext context)
        {
            try
            {
                if (_currentControlScheme.Contains(context.control.device.name)) return;
            }
            catch (IndexOutOfRangeException)
            {
                return; // I think this is a unity bug, can't do anything about this
            }

            var oldScheme = _currentControlScheme is "Keyboard" or "Mouse" ? "Keyboard&Mouse" : context.control.device.name;
            _currentControlScheme = context.control.device.name;
        }

        private void SetInputModeInternal(InputMode newMode)
        {
            _currentMode = newMode;

            switch (newMode)
            {
                case InputMode.Player:
                    foreach(var action in _playerActionMap.actions)
                        action.Enable();
                    foreach(var action in _uiActionMap.actions)
                        action.Disable();
                    break;
                case InputMode.UI:
                    foreach(var action in _uiActionMap.actions)
                        action.Enable();
                    foreach(var action in _playerActionMap.actions)
                        action.Disable();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newMode), newMode, null);
            }
        }

        public static bool IsMine(InputAction.CallbackContext context, int playerID)
        {
            if (!_instance)
                return true;

            if (!_playerToData.TryGetValue(playerID, out var v))
                return false;

            return v.ControllerId == context.control.device.deviceId;
        }

        public static void AddPlayer(int deviceId)
        {
            var playerId = _playerCounter++;
            var isDragon = _playerToData.Count == 0;
            var maskIdx = isDragon ? 0 : ChooseUnusedMask();

            PlayerData data = new(){ControllerId = deviceId, MaskIndex = maskIdx, IsDragon = isDragon};
            _playerToData[playerId] = data;
            EventsChannel.PlayerJoined(playerId, data);
        }

        private static int ChooseUnusedMask()
        {
            if (!_instance)
                return 0;

            while (true)
            {
                var maskIdx = Random.Range(0, _instance.masks.masks.Count);
                var valid = true;
                foreach(var (v, data) in _playerToData)
                    if (data.MaskIndex == maskIdx)
                    {
                        valid = false;
                        break;
                    }

                if (valid)
                    return maskIdx;
            }
        }

        public static void RemovePlayer(int playerId)
        {
            var isHere = _playerToData.TryGetValue(playerId, out var device);
            if (isHere)
            {
                _playerToData.Remove(playerId);
                EventsChannel.PlayerLeft(playerId);
            }
        }

        /// If already at max capacity
        public static bool IsFull()
        {
            if (!_instance)
                return false;

            return TotalPlayers == _instance.MaxPlayers;
        }

        #region Commands

        [Command("in.show", "show all connected controllers")]
        private string ShowJoinedPlayers()
        {

            var s = "Player ID - Device ID\n";
            foreach (var (player, controller) in _playerToData)
                s += $"{player} - {controller}\n";

            return s;
        }
        #endregion
    }

    public enum InputMode
    {
        Player,
        UI
    }
}
