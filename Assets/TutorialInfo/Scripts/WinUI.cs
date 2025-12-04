using UnityEngine;

public class WinUI : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;

            if (WinManager.instance != null)
                WinManager.instance.TriggerWin();
        }
    }
}
