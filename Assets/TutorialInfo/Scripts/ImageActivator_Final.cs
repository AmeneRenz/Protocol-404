using UnityEngine;
using System.Collections;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class ImageActivator_Final : MonoBehaviour
{
    public GameObject sign1, sign2, sign3, sign4;
    public string playerTag = "Player"; // tag of the player root object

    private bool playerInside = false;
    private bool signsShown = false;
    private int collidersInside = 0; // count how many colliders of the player are inside

    void Start()
    {
        // Make sure signs start hidden
        if (sign1) sign1.SetActive(false);
        if (sign2) sign2.SetActive(false);
        if (sign3) sign3.SetActive(false);
        if (sign4) sign4.SetActive(false);

        Debug.Log("[ImageActivator_Final] Started. Looking for player tag: " + playerTag);
    }

    void OnTriggerEnter(Collider other)
    {
        // Ignore camera or other unwanted colliders
        if (other.gameObject.CompareTag("MainCamera") || other.gameObject.name.ToLower().Contains("camera")) return;

        // Only consider colliders from the player root
        if (!other.transform.root.CompareTag(playerTag)) return;

        collidersInside++;
        playerInside = true;

        Debug.Log("[ImageActivator_Final] Player ENTERED trigger. Colliders inside: " + collidersInside);

        // Start input coroutine if signs not shown
        if (!signsShown) StartCoroutine(CheckInputCoroutine());
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.transform.root.CompareTag(playerTag)) return;

        collidersInside--;
        if (collidersInside <= 0)
        {
            playerInside = false;
            collidersInside = 0;
            Debug.Log("[ImageActivator_Final] Player LEFT trigger. Colliders inside: " + collidersInside);
        }
        else
        {
            Debug.Log("[ImageActivator_Final] Collider left but player still inside. Colliders inside: " + collidersInside);
        }
    }

    private IEnumerator CheckInputCoroutine()
    {
        while (playerInside && !signsShown)
        {
            // New Input System
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                ShowAllSigns();
                yield break;
            }
#elif ENABLE_LEGACY_INPUT_MANAGER
            if (Input.GetKeyDown(KeyCode.E))
            {
                ShowAllSigns();
                yield break;
            }
#else
#if ENABLE_INPUT_SYSTEM
            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                ShowAllSigns();
                yield break;
            }
#endif
#endif
            yield return null; // wait one frame
        }
    }

    private void ShowAllSigns()
    {
        if (signsShown) return; // prevent multiple activations

        signsShown = true;
        Debug.Log("[ImageActivator_Final] Showing all signs.");

        if (sign1) sign1.SetActive(true);
        if (sign2) sign2.SetActive(true);
        if (sign3) sign3.SetActive(true);
        if (sign4) sign4.SetActive(true);
    }
}
