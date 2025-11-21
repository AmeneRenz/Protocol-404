using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpScareManager : MonoBehaviour
{
    public static JumpScareManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerJumpScare(GameObject player, float scareDuration, string jumpScareSceneName, string floor2SceneName)
    {
        StartCoroutine(HandleJumpScare(player, scareDuration, jumpScareSceneName));
    }

    private IEnumerator HandleJumpScare(GameObject player, float scareDuration, string jumpScareSceneName)
    {
        // Save the player's current position
        Vector3 savedPos = player.transform.position;

        var loadOp = SceneManager.LoadSceneAsync(jumpScareSceneName, LoadSceneMode.Additive);
        yield return loadOp;

        Scene jumpScene = SceneManager.GetSceneByName(jumpScareSceneName);
        SceneManager.SetActiveScene(jumpScene);

        // Wait for scare duration
        yield return new WaitForSeconds(scareDuration);

        // Unload ONLY the jumpscare scene
        SceneManager.UnloadSceneAsync(jumpScareSceneName);

        // Restore player position
        player.transform.position = savedPos;
    }
}
