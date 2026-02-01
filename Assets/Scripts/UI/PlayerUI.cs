using System;
using Control;
using Input;
using MPlayer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private Image mask;
        [SerializeField] private Image inventory;
        [SerializeField] private Sprite bengal;

        public int playerId;

        public string PlayerName
        {
            get => playerName.text;
            set => playerName.text = value;
        }

        public Sprite Mask
        {
            get => mask.sprite;
            set => mask.sprite = value;
        }

        public Sprite Inventory
        {
            get => inventory.sprite;
            set
            {
                if (!value)
                {
                    inventory.sprite = null;
                    inventory.color = Color.black;
                }
                else
                {
                    inventory.sprite = value;
                    inventory.color = Color.red;
                }
            }
        }

        private void OnEnable()
        {
            var channel = EventsChannel.Get();
            if (!channel)
                return;

            channel.OnPlayerItemUpdate += OnPlayerItemUpdate;
        }

        private void OnDisable()
        {
            var channel = EventsChannel.Get();
            if (!channel)
                return;

            channel.OnPlayerItemUpdate += OnPlayerItemUpdate;
        }

        private void OnPlayerItemUpdate(Player player, Item oldItem, Item newItem)
        {
            if (player.playerId != playerId)
                return;

            if (newItem == Item.None)
                Inventory = null;
            else if (newItem == Item.Match)
                Inventory = bengal;
        }
    }
}
