using UnityEngine;
using UnityEngine.InputSystem;

public class lightScript : MonoBehaviour
{
    public GameObject flashlight_ground;
    public GameObject inticon;
    public GameObject flashlight_player;

    private bool pickedUp = false;

    void OnTriggerStay(Collider other)
    {
        if (pickedUp) return;

        if (other.CompareTag("MainCamera"))
        {
            inticon.SetActive(true);

            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                flashlight_ground.SetActive(false);
                flashlight_player.SetActive(true);
                inticon.SetActive(false);
                pickedUp = true;
                this.enabled = false;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera") && !pickedUp)
        {
            inticon.SetActive(false);
        }
    }
}
