using UnityEngine;
using UnityEngine.InputSystem;

public class keyScript : MonoBehaviour
{
    public GameObject inticon;
    public GameObject key;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            inticon.SetActive(true);

            // New Input System check
            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                key.SetActive(false);
                Door.keyfound = true;
                inticon.SetActive(false);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            inticon.SetActive(false);
        }
    }
}
