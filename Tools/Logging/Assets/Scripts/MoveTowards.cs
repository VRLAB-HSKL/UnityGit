using UnityEngine;
// Wir loggen mit log4net
using log4net;

/// <summary>
/// Ein Objekt, dem diese Klasse hinzugefügt wird 
/// verfolgt ein Zielobjekt mit Hilfe von einfacher Physik.der Funktion
/// 
/// 
/// Ein GameObject, das diese Klasse verwenden soll
/// muss eine Rigidbody Component besitzen.
/// Dies wird nmit Hilfe von RequireComponent sichergestellt.
/// </summary>
/// 
[RequireComponent(typeof(Rigidbody))]
public class MoveTowards : MonoBehaviour
{
    /// <summary>
    /// Position und Orientierung des verfolgten Objekts
    /// </summary>
    [Tooltip("Das verfolgte Objekt")]
    public Transform playerTransform;
    /// <summary>
    /// Geschwindigkeit des Objekts
    /// </summary>
    [Tooltip("Geschwindigkeit")]
    public float speed = 10.0F;
    /// <summary>
    /// Soll der Vektor zwischen Ziel und dem aktuellen Objekt angezeigt werden?
    /// </summary>
    [Tooltip("Anzeige des Vektors, der für die Verfolgung berechnet wird")] 
	public bool showRay = false;

    /// <summary>
    /// Instanz eines Loggers
    /// </summary>
    private static readonly ILog Log = LogManager.GetLogger(typeof(MoveTowards));

    private void Start()
    {
        Log.Debug(">>> Start");
        Log.Info("Info-Ausgabe in Start");
        Log.Debug("<<< Start");
    }

    /// <summary>
    /// Bewegung in FixedUpdate (Time.deltaTime!)
    /// 
    /// Erster Schritt: Keyboard abfragen und bewegen.
    /// Zweiter Schritt: überprüfen, ob wir im zulässigen Bereich sind.
    /// </summary>
    private void FixedUpdate ()
    {
        Log.Debug(">>> FixedUpdate");
        // Schrittweite
        float stepSize = speed * Time.deltaTime;

        Vector3 source = transform.position;
		Vector3 target = playerTransform.position;

        // Neue Position berechnen
		transform.position = Vector3.MoveTowards(source, target, stepSize);
        transform.LookAt(playerTransform);
        if (showRay)
			Debug.DrawRay(transform.position, 100.0f * transform.forward, Color.red);
        Log.Info("Neue Position des Objekts: " + transform.position.ToString());
        Log.Debug("<<< FixedUpdate");
    }
}
