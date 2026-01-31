using System.Collections.Generic;
using Input;
using UI;
using UnityEngine;

namespace Control
{

    public class CharacterSelectionManager : MonoBehaviour
    {
        #region Inspector Properties

        [SerializeField] private GameObject cardContainer;
        [SerializeField] private PlayerCard dragonPrefab;
        [SerializeField] private PlayerCard playerPrefab;
        [SerializeField] private PlayerCard joinCard;

        #endregion

        #region Internal State

        private readonly List<PlayerCard> cards = new();

        #endregion

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
            PlayerCard newCard = InputManager.TotalPlayers == 1 ? Instantiate(dragonPrefab) : Instantiate(playerPrefab);
            cards.Add(joinCard);
            newCard.playerId = playerId;
            newCard.transform.SetParent(cardContainer.transform);
            if (InputManager.IsFull())
                joinCard.gameObject.SetActive(false);
        }


    }
}
