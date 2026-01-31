using System;
using System.Collections.Generic;
using DG.Tweening;
using Input;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Control
{

    public class CharacterSelectionManager : MonoBehaviour
    {
        #region Inspector Properties

        [SerializeField] private GameObject cardContainer;
        [SerializeField] private PlayerCard dragonPrefab;
        [SerializeField] private PlayerCard playerPrefab;
        [SerializeField] private PlayerCard joinCard;
        [SerializeField] private TextMeshProUGUI pressToStart;
        [SerializeField] private Color greyedColor = Color.grey;


        #endregion

        #region Internal State

        private readonly List<PlayerCard> cards = new();
        private Color originalPressToStartColor = Color.white;

        #endregion

        private void Start()
        {
            originalPressToStartColor = pressToStart.color;
            UpdatePressToStart();
        }

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

        private void OnPlayerJoined(int playerId, int deviceId)
        {
            Debug.Log($"Player {playerId} Joined with device {deviceId}");
            // Total players == 1 implies this is the first player
            var isTiger = InputManager.TotalPlayers == 1;
            PlayerCard newCard = isTiger ? Instantiate(dragonPrefab) : Instantiate(playerPrefab);

            cards.Add(joinCard);
            var joinCardPos = joinCard.transform.position;
            newCard.playerId = playerId;
            newCard.transform.SetParent(cardContainer.transform);
            newCard.transform
                .DOPunchScale(newCard.transform.localScale * 1.2f, 0.25f)
                .OnComplete(() =>
                {
                    var position = newCard.transform.position;
                    position.y = joinCardPos.y;

                    newCard.transform.position = position;
                })
                .Play();

            joinCard.transform.SetParent(null);
            joinCard.transform.SetParent(cardContainer.transform);

            if (InputManager.IsFull())
                joinCard.gameObject.SetActive(false);

            if (InputManager.TotalPlayers == 1)
                InputManager.DragonPlayer = playerId;

            UpdatePressToStart();
        }

        private void UpdatePressToStart()
        {
            if (InputManager.TotalPlayers >= 2)
                pressToStart.color = originalPressToStartColor;
            else
                pressToStart.color = greyedColor;
        }

        public void StartGame()
        {
            if (InputManager.TotalPlayers < 2)
            {
                // TODO Add sound or animation
                pressToStart.rectTransform.DOShakePosition(0.25f, new Vector3(10, 0, 0)).Play();
            }

            // TODO start game
        }

        public void GoBack()
        {
            InputManager.DragonPlayer = -1;
            InputManager.PlayerToController.Clear();
        }
    }
}
