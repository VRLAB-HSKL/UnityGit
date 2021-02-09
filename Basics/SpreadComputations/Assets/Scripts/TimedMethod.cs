using UnityEngine;
using System.Collections;

/// <summary>
/// Klasse, die eine Coroutine startet.
/// 
/// Wir geben Text in die Konsole aus.
/// </summary>
public class TimedMethod : MonoBehaviour
{
    /// <summary>
    /// Zeit zwischen den Aufrufen
    /// </summary>
    [Range(2.0f, 10.0f)]
    [Tooltip("Zeit zwischen den Aufrufen")]
    public float delaySeconds = 5.0f;

	private void Start ()
    {
		StartCoroutine (Tick ());
	}
	/// <summary>
    /// Coroutine, die gestartet wird und nichts anderes macht als zu warten.
    /// </summary>
    /// <returns></returns>
	private IEnumerator Tick()
    {
		// Endlos-Schleife
		while (true) {
			Debug.Log("Tick " + Time.time);
			yield return new WaitForSeconds(delaySeconds);
		}
	}
}
