using UnityEngine;

namespace Scenes.Collider
{
    public class Test : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Entering to a trigger from " + name);
        }
    }
}
