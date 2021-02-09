using UnityEngine;

/// <summary>
/// Interaktive Definition von Zielpunkten
/// 
/// Die einzelnen
/// Punkte sind durch GameObjects gegeben, die im Editor
/// zugewiesen werden k�nnen.
/// 
/// wir k�nnen die Zielpunkte zentral, mit Hilfe dieser Klasse,
/// visualisieren oder ausblenden.
/// </summary>
public class InteractiveWaypoints : MonoBehaviour
{
    /// <summary>
    /// Array mit den Wegpunkten
    /// </summary>
	public GameObject[] waypoints;
    /// <summary>
    /// Sollen die Zielobjekte gerendert werden w�hrend der Laufzeit?
    /// </summary>
    public bool showTheWaypoints = true;

    /// <summary>
    /// Instanzen der Renderer f�r die Zielobjekte
    /// 
    /// Wir k�nnen zentral, mit einem Schalter im Editor, entscheiden,
    /// ob die Zielobjekte dargestellt werden.
    /// </summary>
    private MeshRenderer[] ren;

    /// <summary>
    /// Renderer einstellen und alles vorbereiten
    /// </summary>
    private void Awake()
    {
        if (waypoints.Length > 1)
        {
            this.ren = new MeshRenderer[waypoints.Length];

            for (int i = 0; i < waypoints.Length; i++)
            {
                this.ren[i] = waypoints[i].GetComponent(typeof(MeshRenderer)) as MeshRenderer;
                this.ren[i].enabled = showTheWaypoints;
            }
        }
        else
            Debug.Log("Fehler - Keine GameObjects als Zielobjekte in der Szene!");
    }
}