using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // <- new input system

public class Door : MonoBehaviour
{
    public GameObject door_closed, door_opened, intText;
    public AudioSource open, close;
    public bool opened;

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("MainCamera")) return;

        if (opened == false)
        {
            intText.SetActive(true);

            // Use the new Input System. Use wasPressedThisFrame to emulate GetKeyDown
            bool pressedE = false;
            if (Keyboard.current != null && Keyboard.current.eKey != null)
            {
                pressedE = Keyboard.current.eKey.wasPressedThisFrame;
            }

            // Optional: allow gamepad confirm button (A / buttonSouth)
            if (!pressedE && Gamepad.current != null)
            {
                pressedE = Gamepad.current.buttonSouth.wasPressedThisFrame;
            }

            if (pressedE)
            {
                door_closed.SetActive(false);
                door_opened.SetActive(true);
                intText.SetActive(false);
                // if you have an AudioSource assigned, uncomment to play:
                // open.Play();
                StartCoroutine(repeat());
                opened = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(false);
        }
    }

    IEnumerator repeat()
    {
        yield return new WaitForSeconds(4.0f);
        opened = false;
        door_closed.SetActive(true);
        door_opened.SetActive(false);
        // if you have an AudioSource assigned, uncomment to play:
        // close.Play();
    }
}
