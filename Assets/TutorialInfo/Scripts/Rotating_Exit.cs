using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotatingExit : MonoBehaviour
{
    [Header("Exit Objects")]
    public GameObject exitClosed;
    public GameObject exitOpened;

    [Header("UI Prompts")]
    public GameObject interactText;  // "Press E" prompt
    public GameObject lockedText;     // Optional locked text

    [Header("Settings")]
    public bool locked = true;
    public float autoCloseDelay = 4f;

    private bool opened = false;

    public void UnlockExit() => locked = false;

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("MainCamera")) return;

        if (!opened)
        {
            if (locked)
            {
                if (lockedText != null) lockedText.SetActive(true);
                if (interactText != null) interactText.SetActive(false);
                return;
            }

            if (interactText != null)
                interactText.SetActive(true);

            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
                OpenExit();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("MainCamera")) return;

        if (interactText != null) interactText.SetActive(false);
        if (lockedText != null) lockedText.SetActive(false);
    }

    void OpenExit()
    {
        opened = true;

        if (exitClosed != null) exitClosed.SetActive(false);
        if (exitOpened != null) exitOpened.SetActive(true);
        if (interactText != null) interactText.SetActive(false);

        StartCoroutine(CloseAfterDelay());
    }

    IEnumerator CloseAfterDelay()
    {
        yield return new WaitForSeconds(autoCloseDelay);

        opened = false;

        if (exitClosed != null) exitClosed.SetActive(true);
        if (exitOpened != null) exitOpened.SetActive(false);
    }
}
