using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{

    public static Sound Instance;
    private AudioSource _audioSource;
    public bool isSoundEnabled = true;

    private void Awake()
    {
        makeSingleton();
        _audioSource = GetComponent<AudioSource>();
    }
    private void makeSingleton()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void ToggleSound()
    {
        isSoundEnabled = !isSoundEnabled;
    }

    public void PlaySoundFX(AudioClip clip, float volume)
    {
        if (isSoundEnabled)
        {
            _audioSource.PlayOneShot(clip, volume);
        }
    }
}
