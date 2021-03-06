using UnityEngine;

/// <summary>
/// Ein Objekt, dem diese Klasse hinzugefügt wird verfolgt ein
/// Zielobjekt mit Hilfe von Starrkörper-Physik.
/// </summary>
public class SeekTarget : MonoBehaviour
{
    /// <summary>
    /// Welches Objekt verfolgen wir?
    /// </summary>
    [Tooltip("Das verfolgte Objekt")]
    public GameObject player;
    /// <summary>
    /// Wenn dieser Wert auf true gesetzt wird werden
    /// die beiden Geschwindigkeitsvektoren angezeigt,
    /// die die Bewegung definieren.
    /// </summary>
    [Tooltip("Anzeige der beiden Geschwindigkeitsvektoren")]
    public bool showVectors = false;

    private const float MAX_MOVE_DISTANCE = 500.0f;
    /// <summary>
    /// Rigidbody-Component des Objekts, das wir beeinflussen.
    /// </summary>
    private Rigidbody m_rbComp;

    /// <summary>
    /// Abfragen der Rigidbody-Komponente.
    /// 
    /// Wir überprüfen nicht, ob eine solche Komponente vorhanden ist!
    /// </summary>
    private void Awake()
    {
        m_rbComp = GetComponent(typeof(Rigidbody)) as Rigidbody;
    }

    /// <summary>
    /// Wir führen die Bewegung in FixedUpdate aus.
    /// </summary>
    private void FixedUpdate ()
    {
		float moveDistance = MAX_MOVE_DISTANCE * Time.deltaTime;
		Vector3 source = transform.position;
		Vector3 target = player.transform.position;

		Vector3 seekVelocity = Seek(source, target, moveDistance);
        m_rbComp.AddForce( seekVelocity, ForceMode.VelocityChange );

        if (showVectors)
        {
            Debug.DrawRay(transform.position, seekVelocity, Color.blue);
            Debug.DrawRay(transform.position, m_rbComp.velocity, Color.yellow);
        }
	}

    /// <summary>
    /// Berechnen des Geschwindigkeitsvektors für das Verfolgen
    /// </summary>
    /// <param name="source">Ausgangsposition</param>
    /// <param name="target">Position des verfolgten Objekts</param>
    /// <param name="distance">Maximaler Betrag der Geschwindigkeit, die wir verwenden möchten</param>
    /// <returns></returns>
	private Vector3 Seek(Vector3 source, Vector3 target, float distance)
    {		
		Vector3 directionToTarget = Vector3.Normalize( target - source );
		Vector3 velocityToTarget = distance * directionToTarget;
		transform.LookAt( player.transform );

		return Vector3.ClampMagnitude(velocityToTarget - m_rbComp.velocity, distance);
	}
}
