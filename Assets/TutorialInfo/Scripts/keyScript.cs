using UnityEngine;
using UnityEngine.InputSystem;

public class KeyScript : MonoBehaviour
{
    [Header("UI")]
    public GameObject intIcon;

    [Header("Assign the door this key unlocks")]
    public Door doorToUnlock;

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("MainCamera")) return;

        intIcon.SetActive(true);

        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            doorToUnlock.UnlockDoor();   // Unlock the assigned door
            gameObject.SetActive(false); // Hide/delete key
            intIcon.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
            intIcon.SetActive(false);
    }
}
