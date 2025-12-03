using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Mainmenu : MonoBehaviour
{
    [Header("Scene to load")]
    public string sceneName = "FLOOR 2";

    [Header("Scene Loader")]
    public UIFader fader; // assign in inspector (UIFader component)

    [Header("Background Music")]
    public AudioSource bgmSource;   // Drag an AudioSource here
    public AudioClip bgmClip;       // Drag your BGM audio file here
    public float bgmVolume = 0.7f;  // Volume control

    void Start()
    {
        // Play BGM when menu loads
        if (bgmSource != null && bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.volume = bgmVolume;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

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
            fader.LoadScene(sceneName);
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
