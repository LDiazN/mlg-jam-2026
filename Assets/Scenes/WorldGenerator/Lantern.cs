using System;
using Unity.VisualScripting;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)

    {
        if (!other.gameObject.CompareTag("Player")) return;

        Destroy(gameObject);
    }
}
