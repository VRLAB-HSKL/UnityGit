using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Erstes Beispiel einer C# Klasse in einer Unity-Anwendung
/// </summary>
public class QuitApplication : MonoBehaviour {

    /// <summary>
    /// Update wird pro Frame aufgerufen.
    /// </summary>
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
            // Im Editor mit Application.Quit() nicht aufgerufen.
            #if UNITY_EDITOR
           UnityEditor.EditorApplication.isPlaying = false;
           #endif
        }
    }


}
