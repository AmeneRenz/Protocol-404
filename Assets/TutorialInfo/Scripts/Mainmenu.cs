using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Mainmenu : MonoBehaviour
{
    [Header("Scene to load")]
    public string sceneName = "FLOOR 2";

    [Header("Scene Loader")]
    public UIFader fader; // assign in inspector (UIFader component)

    void Update()
    {
        // Keyboard shortcuts
        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
        {
            OnPlayPressed();
        }

        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            OnQuitPressed();
        }
    }

    public void OnPlayPressed()
    {
        if (fader != null)
            fader.LoadScene(sceneName); // now calls LoadScene instead of FadeAndLoad
        else
            SceneManager.LoadScene(sceneName);
    }

    public void OnQuitPressed()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
