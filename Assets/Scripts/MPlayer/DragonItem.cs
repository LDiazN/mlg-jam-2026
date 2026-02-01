using System;
using UnityEngine;

namespace MPlayer
{
    public class DragonItem : MonoBehaviour
    {
        public int energy = 20;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Here from trigger");
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("Here for collision");
        }
    }
}
