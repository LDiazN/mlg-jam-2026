using System;
using Control;
using Scenes.WorldGenerator;
using UnityEngine;

namespace MPlayer
{
    [RequireComponent(typeof(PlayerMovement))]
    public class Player : MonoBehaviour
    {
        #region Internal State

        public int playerId;
        public Item item = Item.None;
        public PlayerType type;
        [SerializeField] private PlayerMovement _movement;
        public PlayerMovement Movement => _movement;

        #endregion

        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
        }

        public void SetItem(
            Item newItem)
        {
            var old = item;
            item = newItem;
            EventsChannel.PlayerItemUpdate(this, old, newItem);
        }
    }


    public enum Item
    {
        Match,
        Coin,
        Broom,
        None,
    }

    public enum PlayerType
    {
        Dragon,
        Normal
    }
}
