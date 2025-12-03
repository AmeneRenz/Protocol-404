using UnityEngine;
using UnityEngine.InputSystem;

public class PickupSound : MonoBehaviour
{
    public AudioClip pickupClip;
    public float volume = 1f;

    public void PlayPickupSound()
    {
        if (pickupClip != null)
            AudioSource.PlayClipAtPoint(pickupClip, transform.position, volume);
    }
}
