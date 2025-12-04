using UnityEngine;

public class DoorSound : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip openClip;
    public AudioClip closeClip;
    public AudioClip lockedClip;

    [Header("Volume Settings")]
    [Range(0f, 1f)]
    public float volume = 1f;

    // Play door open sound
    public void PlayOpen()
    {
        if (openClip != null)
            AudioSource.PlayClipAtPoint(openClip, transform.position, volume);
    }

    // Play door close sound
    public void PlayClose()
    {
        if (closeClip != null)
            AudioSource.PlayClipAtPoint(closeClip, transform.position, volume);
    }

    // Play locked door sound
    public void PlayLocked()
    {
        if (lockedClip != null)
            AudioSource.PlayClipAtPoint(lockedClip, transform.position, volume);
    }
}
