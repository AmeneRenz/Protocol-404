using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFader : MonoBehaviour
{
    [Header("Scene Loader Settings")]
    public string defaultScene; // optional default scene

    /// <summary>
    /// Load a scene by name immediately (no fade)
    /// </summary>
    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("UIFader: Scene name is empty!");
            return;
        }

        // Check if the scene exists in Build Settings
        if (SceneUtility.GetBuildIndexByScenePath(sceneName) == -1)
        {
            Debug.LogError($"UIFader: Scene '{sceneName}' is not in Build Settings!");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Load a scene by build index immediately (no fade)
    /// </summary>
    public void LoadScene(int sceneIndex)
    {
        if (sceneIndex < 0 || sceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError("UIFader: Invalid scene build index: " + sceneIndex);
            return;
        }

        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// Optional: Load the default scene
    /// </summary>
    public void LoadDefaultScene()
    {
        if (!string.IsNullOrEmpty(defaultScene))
            LoadScene(defaultScene);
        else
            Debug.LogWarning("UIFader: Default scene not set!");
    }
}
