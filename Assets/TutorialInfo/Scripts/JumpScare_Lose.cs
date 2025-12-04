using UnityEngine;

public class Jumpscare_Lose : MonoBehaviour
{
    [Header("Scene Names")]
    public string jumpScareSceneName = "Jump scene(Lose)"; // exact name of your lose jumpscare scene
    public string gameOverSceneName = "GameoverUI";       // scene to load after losing

    [Header("Settings")]
    public float scareDuration = 3f; // how long the jumpscare plays

    [Header("Player Reference")]
    public GameObject player; // assign Player object in Inspector

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;

            if (JumpScareManager_Lose.instance != null)
            {
                JumpScareManager_Lose.instance.TriggerJumpScare_Lose(
                    player,
                    scareDuration,
                    jumpScareSceneName,
                    gameOverSceneName
                );
            }
        }
    }
}
