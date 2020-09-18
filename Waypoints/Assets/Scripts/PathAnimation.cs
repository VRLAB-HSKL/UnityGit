using UnityEngine;

/// <summary>
/// Bewegung eines Objekts entlang einer Parameterkurve
/// </summary>
public abstract class PathAnimation : MonoBehaviour
{
    [Range(8, 1024)]
    [Tooltip("Radius")]
    public int NumberOfPoints = 64;
    /// <summary>
    /// Ist das Objekt näher beim Ziel als distance,
    /// wird das nächste Ziel verwendet.
    /// </summary>
    [Range(0.1f, 10.0f)]
    [Tooltip("Bei welchem Abstand gilt das Ziel als erreicht?")]
    public float distance = 1.0f;
    /// <summary>
    /// Geschwindigkeit der Bewegung
    /// </summary>
    [Range(0.1f, 100.0f)]
    [Tooltip("Geschwindigkeit der Bewegung")]
    public float speed = 5.0f;
    
    /// <summary>
    /// Instanz der Klasse WaypointManager.
    /// 
    /// Die Berechnung von Positionen und die Verwaltung
    /// der Zielpunkte erfolgt in dieser C#-Klasse.
    /// Sie ist *nicht* von MonoBehaviour abgeleitet!
    /// </summary>
    protected WaypointManager manager = null;

    protected Vector3[] waypoints;

    /// <summary>
    /// Komponente WayPointManager abfragen und speichern.
    /// Wir fragen auch das erste Ziel ab.
    /// </summary>
    protected virtual void Awake()
    {
        ComputePath();
        this.manager = new WaypointManager(this.waypoints, distance);
        // Bewegtes Objekt auf den ersten Zielpunkt setzen
        transform.position = manager.GetWaypoint();
	}

    /// <summary>
    /// Wir verwenden FixedUpdate, da wir mit Time.deltaTime arbeiten.
    /// </summary>
	protected virtual void FixedUpdate ()
    {
        // Objekt mit Hilfe von MoveTowards bewegen
        transform.position = this.manager.Move(
            transform.position,
            speed * Time.deltaTime); 
	}

    // Erster Versuch mit einem Kreis
    protected abstract void ComputePath();
 }
