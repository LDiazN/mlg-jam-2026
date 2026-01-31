using Control;
using UnityEngine;

namespace MPlayer
{
    public class Player : MonoBehaviour
    {
        #region Internal State

        public int playerId;
        public Item item = Item.None;

        public void SetItem(Item newItem)
        {
            var old = item;
            item = newItem;
            EventsChannel.PlayerItemUpdate(this, old, newItem);
        }

        #endregion
    }

    public enum Item
    {
        Match,
        Coin,
        Broom,
        None,
    }
}
