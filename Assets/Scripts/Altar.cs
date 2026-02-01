using System;
using Control;
using MPlayer;
using Unity.VisualScripting;
using UnityEngine;

public class Altar : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private bool _isLit;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isLit)
            return;

        var player = other.gameObject.GetComponent<Player>();
        if (!player || player.type == PlayerType.Dragon || player.item != Item.Match)
            return;

        EventsChannel.CandleLit();
        player.SetItem(Item.None);

        _spriteRenderer.color = Color.magenta;
    }
}
