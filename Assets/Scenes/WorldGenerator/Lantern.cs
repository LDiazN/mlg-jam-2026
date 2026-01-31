using System;
using Unity.VisualScripting;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)

    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
