using System.Collections;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [Header("Background Music")]
    public AudioClip bgmClip;
    [Range(0f, 1f)]
    public float bgmVolume = 0.5f;

    [Header("Random Ambient Sounds")]
    public AudioClip flickerClip;
    public AudioClip knockClip;
    [Range(0f, 1f)]
    public float ambientVolume = 1f;

    [Header("Random Time Settings")]
    public Vector2 flickerDelayRange = new Vector2(5f, 15f);  
    public Vector2 knockDelayRange = new Vector2(5f, 15f);   

    private AudioSource bgmSource;

    void Awake()
    {
        // Create an AudioSource for BGM immediately
        if (bgmClip != null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.clip = bgmClip;
            bgmSource.loop = true;
            bgmSource.volume = bgmVolume;
            bgmSource.playOnAwake = false; // not needed, we call Play manually
            bgmSource.Play(); // play immediately as soon as Awake is called
        }
    }

    void Start()
    {
        // Start random ambient sounds
        StartCoroutine(RandomFlickerSound());
        StartCoroutine(RandomKnockSound());
    }

    IEnumerator RandomFlickerSound()
    {
        while (true)
        {
            float delay = Random.Range(flickerDelayRange.x, flickerDelayRange.y);
            yield return new WaitForSeconds(delay);

            if (flickerClip != null)
                AudioSource.PlayClipAtPoint(flickerClip, transform.position, ambientVolume);
        }
    }

    IEnumerator RandomKnockSound()
    {
        while (true)
        {
            float delay = Random.Range(knockDelayRange.x, knockDelayRange.y);
            yield return new WaitForSeconds(delay);

            if (knockClip != null)
                AudioSource.PlayClipAtPoint(knockClip, transform.position, ambientVolume);
        }
    }
}
