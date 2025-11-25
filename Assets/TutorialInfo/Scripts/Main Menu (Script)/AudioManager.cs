// Assets/Scripts/AudioManager.cs
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioClip backgroundMusic;
    [Range(0, 1)] public float musicVolume = 0.6f;

    private AudioSource src;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            src = gameObject.AddComponent<AudioSource>();
            src.clip = backgroundMusic;
            src.loop = true;
            src.playOnAwake = false;
            src.volume = musicVolume;
            if (backgroundMusic != null) src.Play();
        }
        else
        {
            // If another instance exists, and its clip differs, keep the old one; otherwise destroy duplicate
            Destroy(gameObject);
        }
    }
}
