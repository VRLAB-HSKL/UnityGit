using UnityEngine;

/// <summary>
/// Bewegung eines Objekts entlang eines Kreise
/// </summary>
public class Circle : PathAnimation
{
    /// <summary>
    /// Radius des Kreises
    /// </summary>
    [Range(3.0f, 12.0f)]
    [Tooltip("Radius")]
    public float Radius = 6.0f;


    /// <summary>
    /// Berechnung der Punkte für einen Kreise mit Mittelpunkt im Ursprung
    /// </summary>
    protected override void ComputePath()
    {
        waypoints = new Vector3[NumberOfPoints];
        float x = 0.0f;
        float delta = (2.0f * Mathf.PI) / (float)NumberOfPoints;

        for (int i=0; i< NumberOfPoints; i++)
        {
            waypoints[i].x = Radius * Mathf.Cos(x);
            waypoints[i].y = 0.0f;
            waypoints[i].z = Radius * Mathf.Sin(x);
            x += delta;
        }
    }
}
