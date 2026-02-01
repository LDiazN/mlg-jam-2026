using MPlayer;
using UnityEngine;

public class Match : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)

    {
        if (!other.gameObject.CompareTag("Player")) return;

        Player player = other.gameObject.GetComponent<Player>();
        player.SetItem(Item.Match);
    }
}
