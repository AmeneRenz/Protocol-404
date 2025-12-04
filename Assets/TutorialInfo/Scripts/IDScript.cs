using UnityEngine;
using UnityEngine.InputSystem;

public class IDScript : MonoBehaviour
{
    [Header("UI Prompt")]
    public GameObject interactIcon;

    [Header("Assign the exit this key unlocks")]
    public RotatingExit exitToUnlock;

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("MainCamera")) return;

        if (interactIcon != null)
            interactIcon.SetActive(true);

        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            // Unlock the assigned exit
            if (exitToUnlock != null)
                exitToUnlock.UnlockExit();

            // Hide/delete this ID/key object
            gameObject.SetActive(false);

            // Hide interact icon
            if (interactIcon != null)
                interactIcon.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera") && interactIcon != null)
            interactIcon.SetActive(false);
    }
}
