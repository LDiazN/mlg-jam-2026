using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    #region Variables
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float defaultValueVolume = 0.5f;
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    public void SetMasterVolume()
    {
        audioMixer.SetFloat("Master", Mathf.Log10(defaultValueVolume) * 20);
        PlayerPrefs.SetFloat("masterVolume", defaultValueVolume);
    }
    public void SetMusicVolume()
    {
        audioMixer.SetFloat("Music", Mathf.Log10(defaultValueVolume) * 20);
        PlayerPrefs.SetFloat("musicVolume", defaultValueVolume);
    }
    public void SetSFXVolume()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(defaultValueVolume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", defaultValueVolume);
    }
    private void LoadVolume()
    {
        PlayerPrefs.GetFloat("masterVolume", defaultValueVolume);
        PlayerPrefs.GetFloat("musicVolume", defaultValueVolume);
        PlayerPrefs.GetFloat("SFXVolume", defaultValueVolume);
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }
}
