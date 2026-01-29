using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))] // for non-spatialized audio
public class AudioManager : MonoBehaviour
{
    #region Inspector Properties

    [Tooltip("Used for playing spatialized audio")]
    [SerializeField] private AudioSource sourcePrefab;

    [Tooltip("Used for UI-like audios that are always played at the same intensity")]
    [SerializeField] private AudioSource source;
    [Tooltip("Used for background music")]
    [SerializeField] private AudioSource persistentSource;

    #endregion

    #region Internal State

    private static AudioManager _instance;

    #endregion

    private void Awake()
    {
        if (!_instance)
            _instance = this;
        if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public static void Play(AudioClip clip, float minPitch = 1, float maxPitch = 1, float volume = 1, float start = 0)
    {
        if (!_instance)
            return;

        var source = _instance.source;
        source.clip = clip;
        source.pitch = RandomPitch(minPitch, maxPitch);
        source.spatialize = false;
        source.volume = volume;
        source.time = start;
        source.Play();
    }

    public static void PlaySpatial(AudioClip clip, Vector3 position, float minPitch = 1, float maxPitch = 1,
        float volume = 1, float start = 0)
    {
        if (!_instance)
            return;

        var source = Instantiate(_instance.sourcePrefab);

        source.transform.position = position;
        source.spatialize = true;
        source.clip = clip;
        source.spatialBlend = 1f;
        source.volume = volume;
        source.pitch = RandomPitch(minPitch, maxPitch);
        source.Play();

        Destroy(source.gameObject,
            clip.length * (Time.timeScale < 0.009999999776482582 ? 0.01f : Time.timeScale));
    }

    public static void PlayBackground(AudioClip clip, float volume = 1)
    {
        if (!_instance)
            return;

        var source = _instance.persistentSource;
        source.clip = clip;
        source.volume = volume;
        source.loop = true;
        source.time = 0;
        source.Play();
    }

    public static void BackgroundVolume(float volume)
    {
        if (!_instance)
            return;
        _instance.persistentSource.volume = volume;
    }

    private static float RandomPitch(float min, float max) => Mathf.Approximately(min, max) ? min : Random.Range(min, max);
}
