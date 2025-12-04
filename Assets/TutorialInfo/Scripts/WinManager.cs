using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public static WinManager instance;

    [Header("Scene After Triggering WIN")]
    public string winUIScreen = "WinUI";

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

    // Trigger Win UI from any scene
    public void TriggerWin(string winSceneName = null)
    {
        string finalScene = string.IsNullOrEmpty(winSceneName) ? winUIScreen : winSceneName;
        SceneManager.LoadScene(finalScene);
    }
}
