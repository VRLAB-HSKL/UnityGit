using UnityEngine;

/// <summary>
/// Orbit eines Objekts um das Weltkoordinatensystem
/// </summary>
public class OrbitTransform : MonoBehaviour
{
    /// <summary>
    /// Wo auf der Umlaufbahn befindet sich das Objekt zu Beginn?
    /// 
    /// Der Winnkel wird in Gradmaß angegeben.
    /// </summary>
    [Range(0.0f, 360.0f)]
    [Tooltip("Startposition (als Winkel in Gradmaß) des Objekts")]
    public float startAngle = 180.0f;
    /// <summary>
    /// Wie schnell bewegt sich das Objekt auf der Umlaufbahn?
    /// </summary>
    [Range(0.1f, 100.0f)]
    [Tooltip("Wie schnell bewegt sich das Objekt auf der Umlaufbahn?")]
    public float orbitSpeed = 10.0f;
    /// <summary>
    /// Radius der Umlaufbahn
    /// </summary>
    [Range(0.5f, 5.0f)]
    [Tooltip("Radius der Umlaufbahn")]
    public float orbitTrans = 1.5f;

    /// <summary>
    /// Aktuelle Position des Objekts auf der Umlaufbahn
    /// </summary>
    private float orbitAngle;

    /// <summary>
    /// Wir setzen das Objekt auf seine Umlaufbahn.
    /// </summary>
    void Awake()
    {
        Vector3 trans = new Vector3(orbitTrans, 0.0f, 0.0f);
        transform.Translate(trans);
    }

    /// <summary>
    /// Wir drehen das Objekt auf seine Startposition
    /// </summary>
    void Start()
    {
        this.orbitAngle = startAngle * Mathf.Deg2Rad;
        transform.RotateAround(Vector3.zero, Vector3.up, orbitAngle);
     }

    /// <summary>
    /// Wir verändern den Winkel für die Rotation
    /// </summary>
    void Update()
    {
        // Update für die Position im Orbit
        this.orbitAngle += orbitSpeed * Mathf.Deg2Rad;

        // Rotation um den Ursprung des Weltkoordinatensystems,
        // um die y-Achse (up).
        // Rotation um eine andere Achse: Vektor berechnen und 
        // hier als zweites Argument einsetzen.
        transform.RotateAround(Vector3.zero, Vector3.up, orbitAngle * Time.deltaTime);
    }
}
