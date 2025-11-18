using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;   // New Input System

public class Door : MonoBehaviour
{
    public GameObject door_closed, door_opened, intText, lockedtext;
    public AudioSource open, close;
    public bool opened, locked;
    public static bool keyfound;

    void Start()
    {
        keyfound = false;   
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("MainCamera")) return;

        if (!opened)
        {
            if (!locked)
            {
                intText.SetActive(true);

                // New Input System Check
                bool pressedE = false;

                if (Keyboard.current != null && Keyboard.current.eKey != null)
                    pressedE = Keyboard.current.eKey.wasPressedThisFrame;

                // gamepad support (A button = buttonSouth)
                if (!pressedE && Gamepad.current != null)
                    pressedE = Gamepad.current.buttonSouth.wasPressedThisFrame;

                if (pressedE)
                {
                    door_closed.SetActive(false);
                    door_opened.SetActive(true);
                    intText.SetActive(false);

                    if (open != null)
                        open.Play();

                    StartCoroutine(repeat());
                    opened = true;
                }
            }
            else
            {
                lockedtext.SetActive(true);
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

    IEnumerator repeat()
    {
        yield return new WaitForSeconds(4f);

        opened = false;
        door_closed.SetActive(true);
        door_opened.SetActive(false);

        if (close != null)
            close.Play();
    }

    void Update()
    {
        if(keyfound == true)
        {
            locked = false;
        }
    }
}
