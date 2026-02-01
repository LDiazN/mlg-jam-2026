using Control;
using Scenes.WorldGenerator;
using UnityEngine;
using UnityEngine.UI;

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
        public Color rageColor = Color.magenta;
        public Sprite bar;
        public Color _originalColor;
        private SpriteRenderer _spriteRenderer;

        #endregion

        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        
        private void Start()
        {
            _originalColor = _spriteRenderer.color;
        }

        public void SetItem(
            Item newItem)
        {
            var old = item;
            item = newItem;
            EventsChannel.PlayerItemUpdate(this, old, newItem);
        }
        
        private void OnEnable()
        {
            var channel = EventsChannel.Get();
            if (!channel)
                return;

            channel.OnDragonRageStarted += OnDragonRageStarted;
            channel.OnDragonRageFinished += OnDragonRageFinished;
        }

        private void OnDisable()
        {
            var channel = EventsChannel.Get();
            if (!channel)
                return;

            channel.OnDragonRageStarted -= OnDragonRageStarted;
            channel.OnDragonRageFinished -= OnDragonRageFinished;
        }
        
        private void OnDragonRageFinished()
        {
            _spriteRenderer.color = _originalColor;
        }

        private void OnDragonRageStarted()
        {
            _spriteRenderer.color = rageColor;
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
