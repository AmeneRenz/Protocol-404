using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpScareManager_Lose : MonoBehaviour
{
    public static JumpScareManager_Lose instance;

    [Header("Scene After Jumpscare")]
    public string gameOverScene = "GameoverUI";

    public Vector3 lastPlayerPosition; // Save death position

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

    // Trigger jumpscare with optional custom GameOver scene
    public void TriggerJumpScare_Lose(GameObject player, float scareDuration, string jumpScareScene, string customGameOverSceneName = null)
    {
        lastPlayerPosition = player.transform.position;

        // Use custom scene if provided, otherwise default
        string finalScene = string.IsNullOrEmpty(customGameOverSceneName) ? gameOverScene : customGameOverSceneName;

        StartCoroutine(HandleJumpScare_Lose(player, scareDuration, jumpScareScene, finalScene));
    }

    private IEnumerator HandleJumpScare_Lose(GameObject player, float scareDuration, string jumpScareScene, string finalScene)
    {
        // Load the jumpscare scene additively
        var loadOp = SceneManager.LoadSceneAsync(jumpScareScene, LoadSceneMode.Additive);
        yield return loadOp;

        // Wait for the scare duration
        yield return new WaitForSeconds(scareDuration);

        // Unload jumpscare scene
        SceneManager.UnloadSceneAsync(jumpScareScene);

        // Load the GameOver scene
        SceneManager.LoadScene(finalScene);
    }
}
