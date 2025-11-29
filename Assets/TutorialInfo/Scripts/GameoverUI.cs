using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameoverUI : MonoBehaviour
{
    [Header("Scene Names")]
    public string restartScene = "FLOOR 2";
    public string mainMenuScene = "Mainmenu";

    [Header("Transitions")]
    public UIFader fader; // optional fade reference

    void Update()
    {
        if (Keyboard.current == null) return;

        // YES → Restart the game
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            RestartGame();
        }

        // NO → Return to Main Menu
        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            ReturnToMainMenu();
        }
    }

    // Called by UI Button (YES)
    public void RestartGame()
    {
        if (fader != null)
            fader.LoadScene(restartScene);
        else
            SceneManager.LoadScene(restartScene);
    }

    // Called by UI Button (NO)
    public void ReturnToMainMenu()
    {
        if (fader != null)
            fader.LoadScene(mainMenuScene);
        else
            SceneManager.LoadScene(mainMenuScene);
    }
}