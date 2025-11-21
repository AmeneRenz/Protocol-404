using UnityEngine;

public class Jumpscare : MonoBehaviour
{
    [Header("Scene Names")]
    public string jumpScareSceneName = "Jump scene"; // exact name of your jump scare scene
    public string floor2SceneName = "FLOOR 2";      // exact name of FLOOR 2 scene

    [Header("Jumpscare Settings")]
    public float scareDuration = 3f; // How long the jumpscare plays

    [Header("Player Reference")]
    public GameObject player; // Drag your Player capsule here in the Inspector

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;

            // Call the persistent manager to handle the jumpscare
            if (JumpScareManager.instance != null)
            {
                JumpScareManager.instance.TriggerJumpScare(player, scareDuration, jumpScareSceneName, floor2SceneName);
            }
        }
    }
}
