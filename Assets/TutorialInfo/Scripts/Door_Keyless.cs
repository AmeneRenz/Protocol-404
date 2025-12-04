using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door_Keyless : MonoBehaviour
{
    [Header("Door Objects")]
    public GameObject door_closed;
    public GameObject door_opened;

    [Header("UI Prompt")]
    public GameObject intText;

    [Header("Audio")]
    public AudioSource openSound;
    public AudioSource closeSound;

    private bool opened = false;

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("MainCamera")) return;

        if (!opened)
        {
            if (intText != null)
                intText.SetActive(true);

            bool pressedE = false;

            // Keyboard
            if (Keyboard.current != null && Keyboard.current.eKey != null)
                pressedE = Keyboard.current.eKey.wasPressedThisFrame;

            // Gamepad "A"
            if (!pressedE && Gamepad.current != null)
                pressedE = Gamepad.current.buttonSouth.wasPressedThisFrame;

            if (pressedE)
            {
                OpenDoor();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera") && intText != null)
            intText.SetActive(false);
    }

    void OpenDoor()
    {
        opened = true;

        if (door_closed != null) door_closed.SetActive(false);
        if (door_opened != null) door_opened.SetActive(true);
        if (intText != null) intText.SetActive(false);

        // Play open sound if assigned
        if (openSound != null) openSound.Play();

        StartCoroutine(CloseDoor());
    }

    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(4f);

        opened = false;

        if (door_closed != null) door_closed.SetActive(true);
        if (door_opened != null) door_opened.SetActive(false);

        // Play close sound if assigned
        if (closeSound != null) closeSound.Play();
    }
}
