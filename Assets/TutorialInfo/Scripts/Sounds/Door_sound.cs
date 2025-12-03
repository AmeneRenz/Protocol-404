using UnityEngine;

public class DoorSound : MonoBehaviour
{
    public AudioSource audioSource;

    [Header("Clips")]
    public AudioClip openClip;
    public AudioClip closeClip;
    public AudioClip lockedClip;     // NEW

    public float volume = 1f;

    public void PlayOpen()
    {
        if (audioSource != null && openClip != null)
            audioSource.PlayOneShot(openClip, volume);
    }

    public void PlayClose()
    {
        if (audioSource != null && closeClip != null)
            audioSource.PlayOneShot(closeClip, volume);
    }

    public void PlayLocked()
    {
        if (audioSource != null && lockedClip != null)
            audioSource.PlayOneShot(lockedClip, volume);
    }
}
