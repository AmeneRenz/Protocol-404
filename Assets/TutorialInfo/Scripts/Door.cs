using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [Header("Door Objects")]
    public GameObject door_closed;
    public GameObject door_opened;

    [Header("UI Prompts")]
    public GameObject intText;
    public GameObject lockedText;

    [Header("Audio")]
    public AudioSource openSound;
    public AudioSource closeSound;
    public AudioSource lockedSound; // Locked door sound

    [Header("Settings")]
    public bool locked = true;
    public float autoCloseDelay = 4f;

    private bool opened = false;

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("MainCamera")) return;

        if (!opened)
        {
            if (locked)
            {
                if (lockedText != null) lockedText.SetActive(true);
                if (intText != null) intText.SetActive(false);

                // Play locked sound when player presses E on locked door
                if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame && lockedSound != null)
                    lockedSound.Play();

                return;
            }

            if (intText != null) intText.SetActive(true);

            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
                OpenDoor();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("MainCamera")) return;

        if (intText != null) intText.SetActive(false);
        if (lockedText != null) lockedText.SetActive(false);
    }

    public void UnlockDoor() => locked = false;

    void OpenDoor()
    {
        opened = true;

        if (door_closed != null) door_closed.SetActive(false);
        if (door_opened != null) door_opened.SetActive(true);
        if (intText != null) intText.SetActive(false);

        if (openSound != null) openSound.Play();

        StartCoroutine(CloseAfterDelay());
    }

    IEnumerator CloseAfterDelay()
    {
        yield return new WaitForSeconds(autoCloseDelay);

        opened = false;

        if (door_closed != null) door_closed.SetActive(true);
        if (door_opened != null) door_opened.SetActive(false);

        if (closeSound != null) closeSound.Play();
    }
}
