using UnityEngine;

/// <summary>
/// Klasse, die das Spiel beendet wenn es keine Targets mehr gibt
/// und der auf ein Kollisions-Event reagiert.
/// </summary>
public class HitManager : MonoBehaviour
{
    /// <summary>
    /// Array mit den Target-Objekten
    /// </summary>
	public GameObject[] targets;
    /// <summary>
    /// Anzahl der Targets
    /// 
    /// Bei jedem Treffer wird diese Zahl um 1 verkleinert.
    /// </summary>
	private int numberOfTargets;
    /// <summary>
    /// Ist das Spiel beendet?
    /// 
    /// Wird auf true gesetzt, wenn alle Targets getroffen wurden.
    /// </summary>
	private bool gameOver = false;

    /// <summary>
    /// Vor allen Start-Funktionen
    /// </summary>
	void Awake() {
		numberOfTargets = targets.Length;
	}

	// Wir beenden das Spiel, falls es keine Targets mehr gibt,
	// falls der Player von der Ebene fällt.
	void Update() {	
		if (gameOver)
        {
			Application.Quit ();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }

    /// <summary>
    /// Callback, falls wir in ein Kollisions-Event eintreten.
    /// Wir löschen getroffene targets aus der Liste.
    /// </summary>
    /// <param name="collision">Daten über die aufgetretene Kollision</param>
	void OnCollisionEnter(Collision collision)
    {
		if (collision.rigidbody)
        {
			for (int i=0; i<targets.Length; i++) {
			   if (collision.gameObject.Equals (targets[i]))
                {
				   Destroy (targets[i]);
				   numberOfTargets--;
					if (numberOfTargets == 0)
						gameOver = true;
			   }
			}
		}
	}
}
