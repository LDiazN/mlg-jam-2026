using UnityEngine;

namespace Audio
{
    public class AudioStarter : MonoBehaviour
    {
        [SerializeField] private AudioClip backgroundMusic;

        private void Start()
        {
            AudioManager.PlayBackground(backgroundMusic);
        }
    }
}
