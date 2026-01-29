using QFSW.QC;
using UnityEngine;

namespace Scenes.Audio
{
    public class TestAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip background;
        [SerializeField] private AudioClip spatial;
        [SerializeField] private AudioClip ui;


        [Command("aud.bg", "play background music")]
        private void Background() => AudioManager.PlayBackground(background);

        [Command("aud.sp", "play spatial sound")]
        private void PlaySpatial() => AudioManager.PlayAtPosition(spatial, transform.position);

        [Command("aud.ui", "play ui sound")]
        private void PlayUI() => AudioManager.Play(ui);
    }
}
