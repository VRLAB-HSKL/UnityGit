using UnityEngine;

/// <summary>
/// Ein Objekt, dem diese Klasse hinzugefügt wird 
/// verfolgt ein
/// Zielobjekt mit Hilfe von einfacher Physik.
/// Dafür wird die Rigidbody Component des
/// GameObjects verwendet und ein Geschwindigkeitsvektor
/// zu dieser Component hinzugefügt.
/// 
/// Ein GameObject, das diese Klasse verwenden soll
/// muss eine Rigidbody Component besitzen!
/// Dies stellen wir mit Hilfe von RequireComponent sicher.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class SeekTarget : MonoBehaviour
{
    /// <summary>
    /// Welches Objekt verfolgen wir?
    /// </summary>
    [Tooltip("Welches Objekt verfolgen wir?")]
    public GameObject player;
    /// <summary>
    /// Beschleunigung für die Verfolgung
    /// </summary>
    [Range(10.0f, MAX_ACCELERATION)]
    [Tooltip("Beschleunigung des Verfolgers")]
    public float acceleration = 100.0f;
    /// <summary>
    /// Wenn dieser Wert auf true gesetzt wird werden
    /// die beiden Geschwindigkeitsvektoren angezeigt,
    /// die die Bewegung definieren.
    /// </summary>
    [Tooltip("Sollen Vektoren dargestellt werden, die die Vektoren visualisieren?")]
    public bool showVelocityVector = false;

    /// <summary>
    /// Maximale Beschleunigung.
    /// 
    /// Je größer diese Zahl, desto schneller bewegt
    /// sich der Seeker auf das Target hin.
    /// </summary>
    private const float MAX_ACCELERATION = 500.0f;

    /// <summary>
    /// Rigidbody-Component des Objekts, das wir beeinflussen.
    /// </summary>
    private Rigidbody rbComp;

    /// <summary>
    /// Abfragen der Rigidbody Component des Seekers
    /// </summary>
    private void Awake()
    {
        rbComp = GetComponent(typeof(Rigidbody)) as Rigidbody;
        if (rbComp == null)
          Debug.LogError("Der Seeker hat keine RigidBody Component!");
    }

    /// <summary>
    /// Bewegung berechnen
    /// 
    /// Falls showVectors TRUE ist werden die Vektoren
    /// ausgegeben. Sollte diese Ausgabe nicht zu sehen sein
    /// muss im Player die Option "Gizmo Drawing" aktiviert sein!
    /// </summary>
    private void FixedUpdate()
    {
		Vector3 source = transform.position;
		Vector3 target = player.transform.position;

		Vector3 seekVelocity = Seek(source, target, acceleration * Time.deltaTime, MAX_ACCELERATION * Time.deltaTime);
        Debug.Log("Geschwindigkeitsvektor" + seekVelocity);
        // Kraftvektor zur Rigidbody-Component des Ziels hinzufügen
        // und damit das Ziel steuern.
        rbComp.AddForce(seekVelocity, ForceMode.VelocityChange);

        // Orientierung des Objekts so einstellen, dass das Ziel "angesehen" wird.
        transform.LookAt(player.transform);

        if (showVelocityVector)
            Debug.DrawRay(transform.position, 10.0f*rbComp.velocity, Color.blue);
	}

    /// <summary>
    /// Berechnen des Geschwindigkeitsvektors für das Verfolgen
    /// 
    /// Wir berechnen den Richtungsvektor zum Ziel und multiplizieren
    /// diesen normierten Vektor mit der gewünschten Geschwindigkeit.
    /// 
    /// Als neuen Geschwindigkeitsvektor für den Seeker verwenden
    /// wir die Differenz zwischen dieser Verfolgungsrichtung und
    /// dem aktuellen Geschwindigkeitsvektor des Seekers.
    /// </summary>
    /// <param name="source">Ausgangsposition</param>
    /// <param name="target">Position des verfolgten Objekts</param>
    /// <param name="maxVelocity">Maximaler Betrag der Geschwindigkeit, die wir verwenden möchten</param>
    /// <returns>Bewegungsrichtung</returns>
	private Vector3 Seek(Vector3 source, Vector3 target, 
                        float velocity, float maxVelocity)
    {
        // Richtungsvektor zum Ziel hin (normiert)
        Vector3 directionToTarget = Vector3.Normalize( target - source );
        // Geschwindigkeitsvektor
		Vector3 velocityToTarget = velocity * directionToTarget;
		return Vector3.ClampMagnitude(velocityToTarget - rbComp.velocity, maxVelocity);
	}
}
