using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [Header("Door Objects")]
    public GameObject door_closed;
    public GameObject door_opened;

    [Header("UI Prompts")]
    public GameObject intText;
    public GameObject lockedtext;

    [Header("Audio")]
    public AudioSource openSound;
    public AudioSource closeSound;

    [Header("Settings")]
    public bool locked = true;     // Door starts locked
    private bool opened = false;

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("MainCamera")) return;

        if (!opened)
        {
            if (locked)
            {
                lockedtext.SetActive(true);
                return;
            }

            // Show interact text
            intText.SetActive(true);

            // Input check
            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                OpenDoor();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(false);
            lockedtext.SetActive(false);
        }
    }

    public void UnlockDoor()
    {
        locked = false;
    }

    void OpenDoor()
    {
        opened = true;

        door_closed.SetActive(false);
        door_opened.SetActive(true);
        intText.SetActive(false);

        if (openSound != null)
            openSound.Play();

        StartCoroutine(CloseAfterDelay());
    }

    System.Collections.IEnumerator CloseAfterDelay()
    {
        yield return new WaitForSeconds(4f);

        opened = false;

        door_closed.SetActive(true);
        door_opened.SetActive(false);

        if (closeSound != null)
            closeSound.Play();
    }
}
