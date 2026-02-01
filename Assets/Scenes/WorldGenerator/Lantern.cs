using MPlayer;
using UnityEngine;

namespace Scenes.WorldGenerator
{
    public class Lantern : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)

        {
            var player = other.gameObject.GetComponent<Player>();

            if (!player || player.type == PlayerType.Dragon)
                return;

            player.SetItem(Item.Match);
        }
    }
}
