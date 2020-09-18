using UnityEngine;

/// <summary>
/// Beenden der Anwendung mit ESC
/// </summary>
public class QuitApplication : MonoBehaviour {

    /// <summary>
    /// Update wird pro Frame aufgerufen.
    /// </summary>
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
            // Im Editor wird Application.Quit() nicht aufgerufen.
            #if UNITY_EDITOR
           UnityEditor.EditorApplication.isPlaying = false;
           #endif
        }
    }


}
