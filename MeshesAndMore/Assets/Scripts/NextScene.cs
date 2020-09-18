using UnityEngine;

/// <summary>
/// Wechseln einer Szene
/// </summary>
public class NextScene   : MonoBehaviour
{
    /// <summary>
    /// Name der Zielszene
    /// </summary>
    [Tooltip("Name der Szene, zu der gewechselt werden soll")] 
    public System.String sceneName;

    [Tooltip("Wechseltaste")]
    public KeyCode changeKey = KeyCode.Space;

    /// <summary>
    /// Wechseln der Szene
    /// </summary>
	void Update () {
        if (Input.GetKeyUp(changeKey))
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName.ToString());
    }
}
